using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopsStats : PlayerManager
{
    #region Internal Variables
    public Transform distance_attack;
    #endregion

    [SerializeField]
    int upgradeHral;

    protected override void Awake()
    {
        base.Awake();

        upgrade_text[0] = "You gain " + upgradeHral + " more of maximum health and " + 15 + " of maximum shield.";
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

    }

    protected override void Update()
    {
        base.Update();
        
        // -------------------------------------------------- //
        // Color block = white 
        if (can_color_white)
        {
            can_color_white = false;
        }

        // -------------------------------------------------- //

        if (is_ultimateOn && cast_ended)
        {
            // Aqui va la coroutine del ultimate
        }
    }

    public override void Skill(float cooldown = 0, float timeToRetorn = 0)
    {
        if (cur_skillCD >= skillCD && !playerMovement.GetIsMoving())
        {
            anim.SetTrigger("skill");

            // -------------------------------------------------- //

            is_skill_ready = false;

            // -------------------------------------------------- //

            playerInput.enabled = false;

            // -------------------------------------------------- //

            StartCoroutine(Rage(3.5f, 4));
        }
    }

    // Skill que consiste en mejorar durante X tiempo el daño y la velocidad de ataque
    // Le pasamos por parámetro el timepo que queremos que dure y el nuevo valor de daño
    private IEnumerator Rage(float time, int dmg) {
        
        //Retornamos el control del personaje al jugador.
        playerInput.enabled = true;

        // Almacenamos el valor base del ataque para que cuando cambie no se pierda su valor y poder volver a asignarlo una vez acabada la skill;
        int oldDamage = damageBasicAttack;

        // Cambiamos el daño al nuevo valor;
        damageBasicAttack = dmg;

        float time_cur = 0;
        while (time_cur < time)
        {
            // Activar particulas para que el jugador sepa que tiene un bonus de daño.
            time_cur += Time.deltaTime;
            yield return null;
        }

        damageBasicAttack = oldDamage;
    }

            // Revisar !!!
    protected override void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0)
    {
        //for (int i = 0; i < map.columnLenth; i++)
        //{ // horizontal
        //    if (
        //        (pos_column + i) >= 0 &&
        //        (pos_column + i) < map.columnLenth
        //      )
        //    {

        //        map.ColorBlocks((pos_column + i), (playerMovement.playerRow), Color.red);

        //        if (map.blocks[(pos_column + i), (playerMovement.playerRow)].GetPlayerStatsBlock((int)thisPlayerIs) != null)
        //        {
        //            map.blocks[(pos_column + i), (playerMovement.playerRow)].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
        //        }
        //    }
        //    cur_skillCD = 0;
        //}
    }
            // Revisar !!!


    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD)
        {
            is_ultimateOn = true;
            StartCoroutine(CastingTime(1.5f));
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

    // Funciones para los upgrades al acabar ronda 
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