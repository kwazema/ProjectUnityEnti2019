using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : PlayerManager {

    #region Internal Variables
    int max_blocks;
    Vector2Int[] blocks_affected;
    #endregion

    protected override void Awake()
    {
        index = 3; // TODO: Estoy dudando que sea manual o lo haga GameManager
        base.Awake();

        upgrade_text[0] = "You gain " + 30 + " more of maximum health and " + 15 + " of maximum shield.";
        upgrade_text[1] = "You gain " + 2 + " more of basic damage and " + 5 + " of skill damage.";
        upgrade_text[2] = "You gain " + 15 + " more of ultimate damage.";
        //namePlayer = "Brayan"; // Nombre añadido desde el inspector
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        max_blocks = map.blocks.Length / 4;
        // -------------------------------------------------- //

        //#region Basic 

        //health_max = 1;
        //health = health_max;
        //shield = shield_max;

        //damageBasicAttack = 2;
        //damageSkill = 15;
        //damageUltimate = 20;

        //skillCD = 2;
        //ultimateCD = 7;

        //fireRate = 0.1f;
        //recoveryShieldTime = 2;
        //#endregion

        // -------------------------------------------------- //

        SelectedZonaPlayer();

        // -------------------------------------------------- //

        distance_attack.position = map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position;

        // -------------------------------------------------- //

        if (thisPlayerIs == ThisPlayerIs.Player1)
            player_to_attack = 1;
        else
            player_to_attack = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // -------------------------------------------------- //

        if (moveToPosition)
            MovingToPosition(95f, 1);

        // -------------------------------------------------- //

        if (is_ultimateOn && cast_ended)
            StartCoroutine(CryingBlock());

        // -------------------------------------------------- //

        if (can_color_white)
        {
            for (int i = 0; i < max_blocks; i++)
            {
                map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.white);
            }

            can_color_white = false;
        }
    }

    public override void Skill()
    {
        if (cur_skillCD >= skillCD)
        {
            is_skill_ready = false;

            // -------------------------------------------------- //

            oldPos = (Vector2)transform.position;
            moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, transform.position.y);

            // -------------------------------------------------- //

            if (!playerMovement.GetIsMoving())
            {
                playerInput.enabled = false;
                moveToPosition = true;
            }
        }
    }

    protected override IEnumerator LookForBlocks(int rangeEffectColumn, float time)
    {
        can_color_white = false;

        // -------------------------------------------------- //

        base.LookForBlocks(rangeEffectColumn, time);

        // -------------------------------------------------- //

        anim.SetTrigger("skill");

        // -------------------------------------------------- //

        DeployParticles(Particles.Skill);

        // -------------------------------------------------- //

        int pos_x = playerMovement.playerColumn + graphicMove;
        int pos_y = playerMovement.playerRow;

        if (map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs) != null)
        {
            map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
        }

        // -------------------------------------------------- //

        yield return null;
        cur_skillCD = 0;
    }

    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD)
        {
            anim.SetTrigger("ultimate");

            // -------------------------------------------------------------- //
            Vector2 middleScreen = new Vector2(0, 10);

            GameObject rain =  Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Ultimate], middleScreen, Quaternion.identity);
            rain.SetActive(true);

            // -------------------------------------------------------------- //

            is_ultimate_ready = false;
            is_ultimateOn = true;

            // -------------------------------------------------------------- //

            blocks_affected = GetRandomBlocks(max_blocks);

            for (int i = 0; i < max_blocks; i++)
            {
                map.SetAlert(blocks_affected[i].x, blocks_affected[i].y, true);
            }

            // -------------------------------------------------------------- //

            StartCoroutine(CastingTime(2, false));
        }
    }

    private IEnumerator CryingBlock()
    {
        is_ultimateOn = false;
        cast_ended = false;

        Vector2 position;


        // -------------------------------------------------------------- //

        for (int i = 0; i < max_blocks; i++)
        {
            position = map.blocks[blocks_affected[i].x, blocks_affected[i].y].transform.position;

            Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Skill], position, Quaternion.identity);

            // -------------------- //

            map.SetAlert(blocks_affected[i].x, blocks_affected[i].y, false);
            map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.red);

            // -------------------- //

            map.DestroyBlock(blocks_affected[i].x, blocks_affected[i].y);
        }

        // -------------------------------------------------------------- //

        yield return new WaitForSeconds(0.2f);

        // -------------------------------------------------------------- //

        cur_ultimateCD = 0;

        playerInput.enabled = true;
        player_att_input.enabled = true;

        can_color_white = true;
    }

    protected override void SelectedZonaPlayer()
    {
        if (whichIsThisPlayer == 0)
        {
            graphicMove = 4;
            dirSkillZone = 1;
        }
        else
        {
            graphicMove = -4;
            dirSkillZone = -1;
        }
    }

    public override void Upgrade1()
    {
        health_max += 30;
        shield_max += 20;

        health = health_max;
        shield = shield_max;
    }

    public override void Upgrade2()
    {
        damageBasicAttack += 2;
        damageSkill += 5;
    }

    public override void Upgrade3()
    {
        damageUltimate += 15;
    }
}