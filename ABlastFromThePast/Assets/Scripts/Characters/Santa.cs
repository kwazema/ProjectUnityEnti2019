using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : PlayerManager {

    #region Internal Variables
        int blocks_width;

        Vector2 enemy_position;

    #endregion

    GameObject BurnedEffect;

    protected override void Awake()
    {
        index = 0; 
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
        // Comprueba si se ha activado el ultimate para empezar la coroutine.
        if (is_ultimateOn && cast_ended)
            StartCoroutine(Leech(2));

            // -------------------------------------------------- //
    }

    public override void Skill()
    {
        if (cur_skillCD >= skillCD)
        {
            player_att_input.enabled = false;

            // -------------------------------------------------- //

            anim.SetBool("attack", false);

            anim.SetTrigger("skill");

            // -------------------------------------------------- //

            is_skill_ready = false;

            // -------------------------------------------------- //

            oldPos = (Vector2)transform.position;
            moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, transform.position.y);
            AudioManager.instance.Play("SantaSkill");

            // -------------------------------------------------- //

            if (!playerMovement.GetIsMoving())
            {
                playerInput.enabled = false;
                moveToPosition = true;
            }
        }
    }

    // Le pasas por parámetro el numero de bloques que quieres que se desplace    
    private void PushPlayer(int blocks_pushed) {
        int enemy_pos = game_manager.playerManager[player_to_attack].playerMovement.playerColumn + (blocks_pushed * dirSkillZone);

        if (enemy_pos < map.columnLenth && enemy_pos >= 0)
        {
            game_manager.playerManager[player_to_attack].playerMovement.playerColumn += (blocks_pushed * dirSkillZone);
        }
        else
        {
            if (player_to_attack == 1)
                game_manager.playerManager[player_to_attack].playerMovement.playerColumn = map.columnLenth - 1;
            else
                game_manager.playerManager[player_to_attack].playerMovement.playerColumn = 0;
        }
    }

    protected override IEnumerator LookForBlocks(int rangeEffectColumn, float time) {

        base.LookForBlocks(rangeEffectColumn, time);

        int cur_block = 0;

        bool already_hit = false;

        int pos_x;
        int pos_y = playerMovement.playerRow;
        Vector2 position;

        while (cur_block < rangeEffectColumn) {
            // Almacenamos la posicion horizontal del ataque.
            pos_x = ((playerMovement.playerColumn + graphicMove) + (cur_block * dirSkillZone));

            // Comprobamos que la posicion del ataque no se pase del tamaño del array de map.blocks[,].
            if (
                pos_x < map.columnLenth &&
                pos_x >= 0
                )
            {
                map.ColorBlocks(pos_x, pos_y, Color.red);

                // Almacenamos en un vector2 la posicion en que se va instanciar la partícula.
                position = map.blocks[pos_x, pos_y].transform.position;

                // Instanciamos el prefab de la partícula.
                Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Skill], position, Quaternion.identity);

                // Ahora le pedimos al bloque que nos diga si hay alguien  en la posición de bloques afectados. 
                // En caso afirmativo le pedimos al bloque que haga daño.
                if (map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs) != null && !already_hit)
                {
                    map.blocks[pos_x, pos_y].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                    PushPlayer(1);

                    already_hit = true;
                }
                
                yield return new WaitForSeconds(time);
                
                // Pintamos de nuevo el color base.
                map.ColorBlocks(pos_x, pos_y, Color.white);
            }
            cur_block++;
        }
        cur_skillCD = 0;

        // -------------------------------------------------- //

        player_att_input.enabled = true;

        // -------------------------------------------------- //

        player_att_input.is_skillOn = false;
    }


    public override void Ultimate()
    {
        if (cur_ultimateCD >= ultimateCD) {
            anim.SetBool("attack", false);

            anim.SetTrigger("ultimate");
            DeployParticles(Particles.UltimateCast);
            AudioManager.instance.Play("SantaUlti");

            // -------------------------------------------------------------- //

            is_ultimate_ready = false;
            is_ultimateOn = true;

            // -------------------------------------------------------------- //
            enemy_position = game_manager.playerManager[player_to_attack].transform.position;
            enemy_position = new Vector2(enemy_position.x, enemy_position.y + 1);

            BurnedEffect = Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Ultimate2], enemy_position, Quaternion.identity);
            BurnedEffect.transform.SetParent(game_manager.playerManager[player_to_attack].transform);
            BurnedEffect.SetActive(true);
            // -------------------------------------------------------------- //

            StartCoroutine(CastingTime(2, false));
        }
    }
    
    IEnumerator Leech(int dmgXsecond)
    {
        is_ultimateOn = false;
        cast_ended = false;
        float time = 0;

        GameObject LeechEffect;

        BurnedEffect = new GameObject();       

        while (time < GetDamageUltimate())
        {
            game_manager.playerManager[player_to_attack].TakeDamage(dmgXsecond);

            health += dmgXsecond / 2;
            if (health > health_max)
                health = health_max;

            // -------------------------------------------------------------- //
            enemy_position = game_manager.playerManager[player_to_attack].transform.position;
            enemy_position = new Vector2(enemy_position.x, enemy_position.y + 1);

            LeechEffect = Instantiate(ParticlesToInstantiate[(int)ParticlesSkills.Ultimate], enemy_position, Quaternion.identity);
            LeechEffect.SetActive(true);
            // -------------------------------------------------------------- //
             
            yield return new WaitForSeconds(0.05f);
            time ++;
        }

        Destroy(BurnedEffect);
        cur_ultimateCD = 0;

        // -------------------------------------------------------------- //

        playerInput.enabled = true;
        player_att_input.enabled = true;

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