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
        index = 3;
        base.Awake();

        upgrade_castingUlt = -2f;

        upgrade_description[1] = "Your skill affects in a cross area.";
        upgrade_description[2] = "Your ultimate CD (cold down) gets " + upgrade_ultCD.ToString() + " seconds less.";
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

        float r = hitmarker_color.color.r;
        float g = hitmarker_color.color.g;
        float b = hitmarker_color.color.b;

        sprite_distanceAttack.color = new Color(r, g, b, 0);

        // -------------------------------------------------------------- //

        casting_ult = 2f;
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
    }

    public override void Skill()
    {
        if (cur_skillCD >= skillCD)
        {
            anim.SetBool("attack", false);
        
            anim.SetTrigger("skill");

            // -------------------------------------------------- //

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
        base.LookForBlocks(rangeEffectColumn, time);

        // -------------------------------------------------- //

        DeployParticles(Particles.Skill);
        AudioManager.instance.Play("AdventurerSkill");

        // -------------------------------------------------- //

        int pos_x = playerMovement.playerColumn + graphicMove;
        int pos_y = playerMovement.playerRow;

        if (!isUpgraded_skill)
        {
            if (map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs) != null)
            {
                map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
            }
        }
        else
        {
            // comprueba el eje de las X // horizontal
            for (int i = -1; i < 2; i++) {
                if (pos_x + i < map.columnLenth && pos_x + i >= 0) {
                    map.ColorBlocks(pos_x + i, pos_y, Color.red);

                    if (map.blocks[pos_x + i, pos_y].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                        map.blocks[pos_x + i, pos_y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                }
            }

            // comprueba el eje de las Y // Vertical
            for (int i = -1; i < 2; i++)
            {
                if ((pos_y + i) >= 0 && (pos_y + i) < map.rowLenth) {
                    map.ColorBlocks(pos_x, pos_y + i, Color.red);

                    if (map.blocks[pos_x, pos_y + i].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                        map.blocks[pos_x, pos_y + i].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                }
            }

            yield return new WaitForSeconds(0.2f);

            for (int i = -1; i < 2; i++)
            {
                if (pos_x + i < map.columnLenth && pos_x + i >= 0)
                    map.ColorBlocks(pos_x + i, pos_y, Color.white);
            }

            for (int i = -1; i < 2; i++)
            {
                if ((pos_y + i) >= 0 && (pos_y + i) < map.rowLenth)
                    map.ColorBlocks(pos_x, pos_y + i, Color.white);
            }
        }

        // -------------------------------------------------- //

        yield return null;
        cur_skillCD = 0;

        player_att_input.is_skillOn = false;
    }

    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD)
        {
            anim.SetBool("attack", false);

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
                AudioManager.instance.Play("AdventurerUlti");
            }

            // -------------------------------------------------------------- //

            StartCoroutine(CastingTime(casting_ult, true));
            AudioManager.instance.Stop("AdventurerUlti");
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

        // -------------------------------------------------------------- //

        for (int i = 0; i < max_blocks; i++)
        {
            map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.white);
        }

        // -------------------------------------------------------------- //

        player_att_input.is_ultOn = false;

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

    public override void Upgrade2()
    {
        isUpgraded_skill = true;
    }

    public override void Upgrade3()
    {
        ultimateCD += upgrade_ultCD;
    }
}