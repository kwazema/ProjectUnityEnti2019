using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polyphemus : PlayerManager
{
    #region Internal Variables
    //public Transform distance_attack;
    
    int pos_x;
    int pos_y;

    int last_block;
    int direction;
    #endregion

    [SerializeField]
    int upgradeHral;

    protected override void Awake()
    {
        index = 2; // TODO: Estoy dudando que sea manual o lo haga GameManager
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

        //#region Basic Stats

        //health = health_max;
        //shield = shield_max;

        //damageBasicAttack = 2;
        //damageSkill = 10;
        //damageUltimate = 50;

        //skillCD = 2;
        //ultimateCD = 6;

        //fireRate = 0.1f;
        //recoveryShieldTime = 2;
        //#endregion

        // -------------------------------------------------- //

        SelectedZonaPlayer();

        // -------------------------------------------------- //

        //distance_attack.position = map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position;

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

        Debug.Log("DMG: " + damageBasicAttack);

        // -------------------------------------------------- //
        // Color block = white 
        if (can_color_white)
        {
            for (int i = 0; i < map.columnLenth; i++) {
                map.ColorBlocks(i, pos_y, Color.white);
            }

            can_color_white = false;
        }

        // -------------------------------------------------- //

        if (is_ultimateOn && cast_ended)
            StartCoroutine(LaserBeam(0.2f));
    }

    public override void Skill()
    {
        if (cur_skillCD >= skillCD && !playerMovement.GetIsMoving())
        {
            player_att_input.enabled = false;
            // -------------------------------------------------- //

            anim.SetBool("attack", false);

            anim.SetTrigger("skill");
            DeployParticles(Particles.Skill);
            AudioManager.instance.Play("PolyphemusSkill");
            
            // -------------------------------------------------- //

            is_skill_ready = false;

            // -------------------------------------------------- //

            playerInput.enabled = false;

            // -------------------------------------------------- //

            StartCoroutine(Rage(3.5f, damageSkill));
        }
    }

    // Skill que consiste en mejorar durante X tiempo el daño y la velocidad de ataque
    // Le pasamos por parámetro el timepo que queremos que dure y el nuevo valor de daño
    private IEnumerator Rage(float time, int dmg) {

        player_att_input.enabled = true;

        player_att_input.is_skillOn = false;

        //Retornamos el control del personaje al jugador.
        playerInput.enabled = true;

        // Almacenamos el valor base del ataque para que cuando cambie no se pierda su valor y poder volver a asignarlo una vez acabada la skill;
        int oldDamage = damageBasicAttack;

        // Cambiamos el daño al nuevo valor;
        damageBasicAttack *= dmg;

        float time_cur = 0;
        while (time_cur < time)
        {
            // Activar particulas para que el jugador sepa que tiene un bonus de daño.
            time_cur += Time.deltaTime;
            yield return null;
        }

        cur_skillCD = 0;
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
            anim.SetBool("attack", false);

            anim.SetTrigger("ultimate");

            is_ultimate_ready = false;
            is_ultimateOn = true;

            DeployParticles(Particles.UltimateCast);

            StartCoroutine(CastingTime(1.5f, false));
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

    private IEnumerator LaserBeam(float time)
    {
        cast_ended = false;

        // Primero comprobamos los bloques que tendra que recorrer y la dirección en que va el rayo.
        if (player_to_attack == 1)  // El rayo irá hacia la derecha.
        {
            last_block = map.columnLenth;
            direction = 1;
        }
        else // El rayo irá hacia la izquierda.
        {
            last_block = -1;
            direction = -1;
        }

        // Segundo cogemos la posicion del ciclope
        pos_x = playerMovement.playerColumn + direction;
        pos_y = playerMovement.playerRow;

        Vector3 position;

        AudioManager.instance.Play("PolyphemusUlti");

        while (pos_x != last_block)
        {
            map.ColorBlocks(pos_x, pos_y, Color.red);

            position = map.blocks[pos_x, pos_y].transform.position;
            Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Ultimate], position, Quaternion.identity);


            if (map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());

            yield return new WaitForSeconds(time);
            pos_x += (1 * direction);
        }

        cur_ultimateCD = 0;
        is_ultimateOn = false;

        can_color_white = true;

        playerInput.enabled = true;
        player_att_input.enabled = true;

        player_att_input.is_ultOn = false;

        

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