using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minos : PlayerManager
{

    #region Internal Variables
    //public Transform distance_attack;
    int pos_column;
    Vector2Int[] blocks_affected;
    int max_blocks;
    int upgrade_maxBlocks;
    #endregion

    protected override void Awake()
    {
        index = 1; 
        base.Awake();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        // -------------------------------------------------- //

        //#region Basic Stats

        //health = health_max;
        //shield = shield_max;

        //damageBasicAttack = 2;
        //damageSkill = 10;
        //damageUltimate = 20;

        //skillCD = 2;
        //ultimateCD = 6;

        //fireRate = 0.1f;
        //recoveryShieldTime = 2;
        //#endregion

        // -------------------------------------------------- //

        SelectedZonaPlayer();

        // -------------------------------------------------- //

        distance_attack.position = map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position;

        // -------------------------------------------------- //

        casting_skill = 1f;
        casting_ult= 1f;

        max_blocks = 8;

        // -------------------------------------------------- //

        upgrade_castingSkill = -.5f;
        upgrade_damageSkill = 10;

        upgrade_maxBlocks = 4;

        // -------------------------------------------------- //

        upgrade_description[1] = "Your skill gets " + upgrade_castingSkill +
                                 " seconds less of casting time and " + upgrade_damageSkill + " extra damage.";
        upgrade_description[2] = "You hit " + upgrade_maxBlocks + " blocks with the ultimate.";
    }

    protected override void Update()
    {
        base.Update();

        // -------------------------------------------------- //

        if (moveToPosition && cast_ended)
            MovingToPosition(65f);

        // -------------------------------------------------- //
        // Color block = white 
        if (can_color_white)
        {
            for (int i = -1; i < 2; i++)
            { // horizontal
                for (int j = -1; j < 2; j++)
                { // vertical

                    if (
                        (pos_column + i) >= 0 &&
                        (pos_column + i) < map.columnLenth &&
                        (playerMovement.playerRow + j) >= 0 &&
                        (playerMovement.playerRow + j) < map.rowLenth
                      )
                    {
                        map.ColorBlocks((pos_column + i), (playerMovement.playerRow + j), Color.white);
                    }
                }
            }

            can_color_white = false;
        }

        // -------------------------------------------------- //

        if (is_ultimateOn && cast_ended)
            StartCoroutine(StellarRain());
    }

    public override void Skill()
    {
        if (cur_skillCD >= skillCD)
        {
            anim.SetBool("attack", false);

            StartCoroutine(base.CastingTime(casting_skill, false));

            // -------------------------------------------------- //

            is_skill_ready = false;

            // -------------------------------------------------- //

            oldPos = (Vector2)transform.position;

            pos_column = game_manager.playerManager[player_to_attack].playerMovement.playerColumn;
            moveToBlock = new Vector2(map.blocks[pos_column, playerMovement.playerRow].transform.position.x, transform.position.y);
            // -------------------------------------------------- //

            for (int i = -1; i < 2; i++)
            { // horizontal
                for (int j = -1; j < 2; j++)
                { // vertical

                    if (
                        (pos_column + i) >= 0 &&
                        (pos_column + i) < map.columnLenth &&
                        (playerMovement.playerRow + j) >= 0 &&
                        (playerMovement.playerRow + j) < map.rowLenth
                      )
                    {
                        map.SetAlert((pos_column + i), (playerMovement.playerRow + j), true);
                    }
                }

                // -------------------------------------------------- //

                if (!playerMovement.GetIsMoving())
                {
                    playerInput.enabled = false;
                    player_att_input.enabled = false;
                    moveToPosition = true;
                }
            }
        }
    }

    protected override IEnumerator LookForBlocks(int rangeEffectColumn, float time)
    {
        base.LookForBlocks(rangeEffectColumn, time);

        anim.SetTrigger("skill");
        AudioManager.instance.Play("MinosSkill");

        DeployParticles(Particles.Skill);
        for (int i = -1; i < 2; i++)
        { // horizontal
            for (int j = -1; j < 2; j++)
            { // vertical

                if (
                    (pos_column + i) >= 0 &&
                    (pos_column + i) < map.columnLenth &&
                    (playerMovement.playerRow + j) >= 0 &&
                    (playerMovement.playerRow + j) < map.rowLenth
                  )
                {
                    map.SetAlert((pos_column + i), (playerMovement.playerRow + j), false);
                    map.ColorBlocks((pos_column + i), (playerMovement.playerRow + j), Color.red);

                    if (map.blocks[(pos_column + i), (playerMovement.playerRow + j)].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                    {
                        map.blocks[(pos_column + i), (playerMovement.playerRow + j)].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.5f);

        cur_skillCD = 0;

        can_color_white = true;

        player_att_input.is_skillOn = false;
    }


    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD)
        {
            anim.SetBool("attack", false);

            anim.SetTrigger("ultimate");
            AudioManager.instance.Play("MinosUltiFirst");
            is_ultimate_ready = false;
            is_ultimateOn = true;

            StartCoroutine(CastingTime(casting_ult, false));
        }
    }

    protected override void SelectedZonaPlayer()
    {
        if (whichIsThisPlayer == 0)
        {
            graphicMove = 1;
            dirSkillZone = 1;
        }
        else
        {
            graphicMove = -1;
            dirSkillZone = -1;
        }
    }

    private IEnumerator StellarRain()
    {
        cast_ended = false;

        int i = 0;
        float time_waiting = 0.35f;

        while (i < max_blocks)
        {
            map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.red);
            map.SetAlert(blocks_affected[i].x, blocks_affected[i].y, false);

            GameObject HitStellarRain;
            Vector2 block_pos = map.blocks[blocks_affected[i].x, blocks_affected[i].y].transform.position;

            HitStellarRain = Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Ultimate], block_pos, Quaternion.identity);
            AudioManager.instance.Play("MinosUltiImpact");

            if (map.blocks[blocks_affected[i].x, blocks_affected[i].y].GetPlayerStatsBlock((int)thisPlayerIs) != null)
            {
                map.blocks[blocks_affected[i].x, blocks_affected[i].y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
            }

            yield return new WaitForSeconds(time_waiting);
            map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.white);
            i++;
        }

        if (i == max_blocks)
        {
            cur_ultimateCD = 0;
            is_ultimateOn = false;

            playerInput.enabled = true;
            player_att_input.enabled = true;
        }

        player_att_input.is_ultOn = false;

    }

    protected override IEnumerator CastingTime(float time_cast, bool value)
    {
        DeployParticles(Particles.UltimateCast);
        blocks_affected = GetRandomBlocks(max_blocks); 
        
        // Aqui se pintan antes/durante el casteo
        for (int i = 0; i < max_blocks; i++)
        {
            map.SetAlert(blocks_affected[i].x, blocks_affected[i].y, true);
        }
        // ------------------------------------------------------------------------- //
        return base.CastingTime(time_cast, value);
    }

    public override void Upgrade2()
    {
        casting_skill += upgrade_castingSkill;
        damageSkill += upgrade_damageSkill;
    }

    public override void Upgrade3()
    {
        max_blocks += upgrade_maxBlocks;
    }
}