using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScepterStats : PlayerManager {

    #region Internal Variables
        public Transform distance_attack;
        int pos_column;
        Vector2Int[] blocks_affected;
        int max_blocks;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        upgrade_text[0] = "You gain " + 50 + " more of maximum health and " + 15 + " of maximum shield.";
        upgrade_text[1] = "You gain " + 4 + " more of basic damage and " + 10 + " of skill damage.";
        upgrade_text[2] = "You gain " + 35 + " more of ultimate damage.";
        //namePlayer = "Scepter"; // Nombre añadido desde el inspector
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        // -------------------------------------------------- //

        #region Basic Stats

        health = health_max;
        shield = shield_max;

        damageBasicAttack = 2;
        damageSkill = 10;
        damageUltimate = 20;

        skillCD = 2;
        ultimateCD = 6;

        fireRate = 0.1f;
        recoveryShieldTime = 2;
        #endregion

        // -------------------------------------------------- //

        SelectedZonaPlayer();

        // -------------------------------------------------- //

        distance_attack.position = map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position;

        // -------------------------------------------------- //

        if (thisPlayerIs == ThisPlayerIs.Player1)
            player_to_attack = 1;
        else
            player_to_attack = 0;

        // -------------------------------------------------- //

        max_blocks = 8;
    }

    protected override void Update()
    {
        base.Update();

        // -------------------------------------------------- //

        if (moveToPosition) 
            MovingToPosition(65f);
       
        // -------------------------------------------------- //
         // Color block = white 
        if (can_color_white) {
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

    public override void Skill(float cooldown = 0, float timeToRetorn = 0)
    {
        if (cur_skillCD >= skillCD)
        {
            anim.SetTrigger("skill");

            // -------------------------------------------------- //

            is_skill_ready = false;

            // -------------------------------------------------- //

            oldPos = (Vector2)transform.position;

            pos_column = game_manager.playerStats[player_to_attack].playerMovement.playerColumn;
            moveToBlock = new Vector2(map.blocks[pos_column, playerMovement.playerRow].transform.position.x, transform.position.y);

            // -------------------------------------------------- //

            if (!playerMovement.GetIsMoving())
            {
                playerInput.enabled = false;
                moveToPosition = true;
            }
        }
    }

    protected override void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0)
    {
        for (int i = -1; i < 2; i++) { // horizontal
            for (int j = -1; j < 2; j++) { // vertical

                if (
                    (pos_column + i) >= 0 &&
                    (pos_column + i) < map.columnLenth &&
                    (playerMovement.playerRow + j) >= 0 &&
                    (playerMovement.playerRow + j) < map.rowLenth 
                  )
                {
                    
                    map.ColorBlocks((pos_column + i), (playerMovement.playerRow + j), Color.red);

                    if (map.blocks[(pos_column + i), (playerMovement.playerRow + j)].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                    {
                        map.blocks[(pos_column + i), (playerMovement.playerRow + j)].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                    }
                }
            }
            cur_skillCD = 0;
        }
    }

    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD)
        {
            is_ultimateOn = true;
            StartCoroutine(CastingTime(3));
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

    void GetRandomBlocks()
    {
        int i = 0;
        int blocks_created = 0;
        blocks_affected = new Vector2Int[max_blocks];

        int init_pos_x;
        int max_pos_x;
        if (player_to_attack == 0)
        {
            init_pos_x = 0;
            max_pos_x = init_pos_x + (map.columnLenth) / 2;
        }
        else
        {
            init_pos_x = map.columnLenth - 1;
            max_pos_x = init_pos_x - (map.columnLenth) / 2;
        }

        while (i < max_blocks)
        {
            Vector2Int cpy = new Vector2Int(Random.Range(init_pos_x, max_pos_x), Random.Range(0, map.rowLenth));

            bool is_finded = false;
            for (int j = 0; j < blocks_created && !is_finded; j++)
            {
                if (blocks_affected[j] == cpy)
                {
                    is_finded = true;
                }
            }

            if (!is_finded) {
                blocks_affected[i] = cpy;
                i++;
                blocks_created++;
            }
        }
    }

    private IEnumerator StellarRain() {
        cast_ended = false;
        is_ultimateOn = false;

        int i = 0;
        float time_waiting = 0.5f;

        while (i < max_blocks) {
            map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.red);
            map.SetAlert(blocks_affected[i].x, blocks_affected[i].y, false);

            if (map.blocks[blocks_affected[i].x, blocks_affected[i].y].GetPlayerStatsBlock((int)thisPlayerIs) != null)
            {
                map.blocks[blocks_affected[i].x, blocks_affected[i].y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
            }
            yield return new WaitForSeconds(time_waiting);
            map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.white);
            i++;
        }

        if (i == max_blocks) {
            cur_ultimateCD = 0;
        }
        
    }

    protected override IEnumerator CastingTime(float time_cast)
    {
        DeployParticles(Particles.Ultimate);
        GetRandomBlocks();

        // Aqui se pintan antes/durante el casteo
        //
        for (int i = 0; i < max_blocks; i++)
        {
            //map.ColorBlocks(blocks_affected[i].x, blocks_affected[i].y, Color.yellow);
            map.SetAlert(blocks_affected[i].x, blocks_affected[i].y, true);
        }
        // ------------------------------------------------------------------------- //
        return base.CastingTime(time_cast);
    }

    public override void Upgrade1()
    {
        health_max += 50;
        shield_max += 15;

        health = health_max;
        shield = shield_max;
    }

    public override void Upgrade2()
    {
        damageBasicAttack += 4;
        damageSkill += 10;
    }

    public override void Upgrade3()
    {
        damageUltimate += 35;
    }
}