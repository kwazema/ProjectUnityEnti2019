using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : PlayerManager {

    #region Internal Variables
    public Transform distance_attack;

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

        if (moveToPosition)
            MovingToPosition(95f, 1);

    }

    public override void Skill()
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

    protected override IEnumerator LookForBlocks(int rangeEffectColumn, float time)
    {

        base.LookForBlocks(rangeEffectColumn, time);
        yield return null;
        cur_skillCD = 0;
    }

    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD)
        {
            anim.SetTrigger("ultimate");

            // -------------------------------------------------------------- //

            is_ultimate_ready = false;
            is_ultimateOn = true;

            // -------------------------------------------------------------- //
        }
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