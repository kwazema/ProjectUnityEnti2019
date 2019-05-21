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

    protected override void Awake()
    {
        index = 2;
        base.Awake();

        upgrade_description[1] = "Your skill gets " + upgrade_durationSkill + " seconds time longer.";
        upgrade_description[2] = "Your ultimate affects 2 blocks rows, but you don't know what rows will be.";
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

        casting_ult = 1.5f;
        duration_skill = 4.5f;

        // -------------------------------------------------- //

        upgrade_durationSkill = 2f;

        // -------------------------------------------------- //

        // Comprobaciones que se hacen para saber en que dirección y hasta que posición 
        // debe ir el laser beam.
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
            for (int i = 0; i < map.columnLenth; i++)
            {
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

            StartCoroutine(Rage(duration_skill, damageSkill));
        }
    }

    // Skill que consiste en mejorar durante X tiempo el daño y la velocidad de ataque
    // Le pasamos por parámetro el timepo que queremos que dure y el nuevo valor de daño
    private IEnumerator Rage(float time, int dmg)
    {

        player_att_input.enabled = true;

        player_att_input.is_skillOn = false;

        //Retornamos el control del personaje al jugador.
        playerInput.enabled = true;

        // Almacenamos el valor base del ataque para que cuando cambie no se pierda su valor y poder volver a asignarlo una vez acabada la skill;
        int oldDamage = damageBasicAttack;

        // Cambiamos el daño al nuevo valor;
        damageBasicAttack += dmg;

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

    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD)
        {
            anim.SetBool("attack", false);

            anim.SetTrigger("ultimate");

            is_ultimate_ready = false;
            is_ultimateOn = true;

            DeployParticles(Particles.UltimateCast);

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

    private IEnumerator LaserBeam(float time)
    {
        cast_ended = false;
        int pos_y2 = pos_y;

        // Cogemos la posicion del cíclope
        pos_x = playerMovement.playerColumn + direction;
        pos_y = playerMovement.playerRow;

        Vector3 position;

        AudioManager.instance.Play("PolyphemusUlti");

        while (pos_x != last_block)
        {
            position = map.blocks[pos_x, pos_y].transform.position;
            Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Ultimate], position, Quaternion.identity);

            map.ColorBlocks(pos_x, pos_y, Color.red);

            if (isUpgraded_ult)
            {
                int direction_y = 0;

                if (Random.Range(0, 2) > 0)
                    direction_y = 1;
                else
                    direction_y = -1;

                if (
                    playerMovement.playerRow + direction_y >= 0 &&
                    playerMovement.playerRow + direction_y < map.rowLenth
                    )
                {
                    pos_y2 = playerMovement.playerRow + direction_y;

                    map.ColorBlocks(pos_x, pos_y2, Color.red);

                    position = map.blocks[pos_x, pos_y2].transform.position;
                    Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Ultimate], position, Quaternion.identity);


                    if (map.blocks[pos_x, pos_y2].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                        map.blocks[pos_x, pos_y2].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageUltimate());
                }
            }

            if (map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageUltimate());

            yield return new WaitForSeconds(time);
            map.ColorBlocks(pos_x, pos_y, Color.white);

            if (isUpgraded_ult)
                map.ColorBlocks(pos_x, pos_y2, Color.white);

            pos_x += (1 * direction);
        }

        cur_ultimateCD = 0;
        is_ultimateOn = false;

        can_color_white = true;

        playerInput.enabled = true;
        player_att_input.enabled = true;

        player_att_input.is_ultOn = false;
    }

    public override void Upgrade2()
    {
        duration_skill += upgrade_durationSkill;
    }

    public override void Upgrade3()
    {
        isUpgraded_ult = true;
    }
}