using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrayanStats : PlayerManager {
    #region Internal Variables
        public Transform distance_attack;
        int blocks_width;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        upgrade_text[0] = "You gain " + 30 + " more of maximum health and " + 15 + " of maximum shield.";
        upgrade_text[1] = "You gain " + 2 + " more of basic damage and " + 5 + " of skill damage.";
        upgrade_text[2] = "You gain " + 15 + " more of ultimate damage.";
        //namePlayer = "Brayan"; // Nombre añadido desde el inspector
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        // -------------------------------------------------- //

        #region Basic Stats
        health_max = 1;
        health = health_max;
        
        shield = shield_max;

        damageBasicAttack = 2;
        damageSkill = 15;
        damageUltimate = 12;

        skillCD = 2;
        ultimateCD = 7;

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

        blocks_width = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        

        if (moveToPosition) 
            MovingToPosition(95f, blocks_width);

        // -------------------------------------------------- //
        // Color block = white 
        if (can_color_white)
        {
            for (int i = 0; i < blocks_width; i++)
            {
                if (
                    (playerMovement.playerColumn + graphicMove) + (i * dirSkillZone) < map.columnLenth &&
                    (playerMovement.playerColumn + graphicMove) + (i * dirSkillZone) >= 0
                   )
                {
                    map.ColorBlocks((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow, Color.white);
                }
            }
            can_color_white = false;
        }

        // -------------------------------------------------- //
        // Comprueba si se ha activado el ultimate para empezar la coroutine.
        if (is_ultimateOn && cast_ended)
            StartCoroutine(Leech(3));

        // -------------------------------------------------- //
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
            moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, transform.position.y);

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
        for (int i = 0; i < rangeEffectColumn; i++)
        {
            if (
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone)) < map.columnLenth &&
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone)) >= 0
                )
            {
                map.ColorBlocks((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow, Color.red);

                if (map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                {
                    map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                }
            }
        }
        cur_skillCD = 0;
    }

    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD) {
            anim.SetTrigger("ultimate");
            //DeployParticles(Particles.Ultimate);
            StartCoroutine(CastingTime(2));
        }
    }
    
    IEnumerator Leech(float use_time)
    {
     
        is_ultimateOn = false;
        cast_ended = false;
        float time = 0;

        while (time < use_time)
        {
            game_manager.playerStats[player_to_attack].TakeDamage(GetDamageUltimate());
            health += (GetDamageUltimate() / 2);

            if (health > health_max)
                health = health_max;

            yield return new WaitForSeconds(1);
            time++;
        }
        cur_ultimateCD = 0;
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
