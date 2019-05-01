using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrayanStats : PlayerManager {

    public Transform distance_attack;

    protected override void Awake()
    {
        base.Awake();
        //namePlayer = "Brayan"; // Nombre añadido desde el inspector
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();

        #region Basic Stats

        health = health_max;
        shield = shield_max;

        damageBasicAttack = 2;
        damageSkill = 15;
        damageUltimate = 20;

        ultimateCD = 5;
        #endregion

        fireRate = 0.1f;
        recoveryShieldTime = 2;

        SelectedZonaPlayer();

        distance_attack.position = map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position;

        if (thisPlayerIs == ThisPlayerIs.Player1)
            player_to_attack = 1;
        else
            player_to_attack = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        Debug.Log("Brayan: " + health);

        base.Update();
        int blocks_width = 3;

        if (moveToPosition) {
            MovingToPosition(95f, blocks_width);
            anim.SetTrigger("skill");
        }

        // -------------------------------------------------- //
        // Color block = green 
        if (!returnOldPosition)
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
        }

        // -------------------------------------------------- //
        // Comprueba si se ha activado el ultimate para empezar la coroutine.
        if (is_ultimateOn && cast_ended)
            StartCoroutine(Leech(3));

        // -------------------------------------------------- //

        if (is_shootting)
        {
            Debug.Log("SHOOOOOOTING");
            anim.SetTrigger("shootting");
        }

    }

    public override void Skill(float cooldown = 0, float timeToRetorn = 0)
    {
        oldPos = (Vector2)transform.position;
        moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, transform.position.y);

        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
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
    }

    public override void Ultimate()
    {
        if (cur_ultimateCD == ultimateCD) {
            StartCoroutine(CastingTime(2));
        }
    }

    // NO FUNCIONA EL INPUT DEL TECLADO DEL SEGUNDO PLAYER
    // TECLADO NUMERICO NO FUNCIONA CON ESTA HABILIDAD
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
}
