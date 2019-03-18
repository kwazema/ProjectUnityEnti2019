using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
{
    public Game game;
    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer player;

    //private AnotherScript anotherScript;
    PlayerMovement playerMove;

    public Transform basicShotSpawn;
    public GameObject basicAttack;
    public SpriteRenderer shieldRender;

    #region  Variables
    private float nextFire = 0.0f;
    private bool isShieldActive = false;
    private bool isShooting = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        //playerStats = GetComponent<BrayanStats>();
        game = GameObject.Find("Map").GetComponent<Game>();

        /* <-- Funcion que siempre comprueba y añade vida al escudo por cada segundo --> */
        //StartCoroutine(ShieldRecovery()); 
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        //Debug.Log("Escudo: " + playerStats.shield);
        //Debug.Log("Vida: " + playerStats.health);
    }

    void InitPlayer()
    {

    }

    void GetInput()
    {
        if (
           Input.GetKey(KeyCode.V) &&
           !playerMove.GetIsMoving() &&
           Time.time > nextFire
           )
        {
            BasicAttack();
        }
        else
        {
            isShooting = false;
        }

        if (Input.GetKeyDown(KeyCode.B))
            SkillAttack();

        if (Input.GetKeyDown(KeyCode.N))
            UltimateAttack();

        /*
         Hay problemas a la hora de seleccionar que tiene prioridad:
         si el disparo o el escudo. 
         De cualquier manera, si mantienes pulsado el escudo y disparas, este se desactiva intermitentemente 
        */
        for (int i = 0; i < 2; i++)
        {
            if (Input.GetKey(KeyCode.M))
            {
                if (game.playerStats[i].GetShield() > 0 && !isShooting)
                    ActiveShield();
            }

            //if (Input.GetKeyUp(KeyCode.M) || (shieldHealth < 0) || isShooting)
            if (Input.GetKeyUp(KeyCode.M) || (game.playerStats[i].GetShield() < 0))
            {
                DeactivateShield();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //GetDamage();
        //Debug.Log("Hit " + col.gameObject.name);

        if (EnumPlayer.Player1 == player)
        {
            game.playerStats[0].TakeDamage(game.playerStats[1].GetDamageBasicAttack());
        }
        else
        {
            game.playerStats[1].TakeDamage(game.playerStats[0].GetDamageBasicAttack());
        }
        //playerStats.TakeDamage(20);
    }

    void BasicAttack()
    {
        // Cada vez que disparas te iguala el time.time y despues le sumas el fireRate 
        // sino hasta el nextFire no sea mayor a Time.Time actual no dejara de disparar
        nextFire = Time.time;
        for (int i = 0; i < 2; i++)
        {
            nextFire += Time.deltaTime + game.playerStats[i].GetFireRate();
        }
       
        GameObject basicAttackClone = (GameObject)Instantiate(basicAttack, basicShotSpawn.position, basicShotSpawn.rotation);
        basicAttackClone.transform.rotation = transform.rotation;
        
        isShooting = true;
    }

    private void ActiveShield()
    {
        isShieldActive = true;
        shieldRender.enabled = true;
    }

    private void DeactivateShield()
    {
       //Debug.Log("Descactivando Protocolo Ruedines.");
        isShieldActive = false;
        shieldRender.enabled = false;
    }

    IEnumerator ShieldRecovery()
    {
        while (true)
        { // loops forever...
            //if (
            //    playerStats.shield < 20 &&
            //    !isShieldActive
            //    )
            //{
            //    playerStats.shield += playerStats.recoveryShieldTime;
            //    //playerStats.SetShield(RecoveryShield()); 
            //    // increase health and wait the specified time
            //    yield return new WaitForSeconds(1);
            //}
            //else
            //{ // if shieldHealth >= 100, just yield 
            //    yield return null;
            //}
        }
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    //GetDamage();
    //    playerStats.TakeDamage(1);
    //}

    //private int RecoveryShield()
    //{
    //    int shield = (int)playerStats.GetShield();
    //    shield += playerStats.GetRecoveryShieldTime();
    //    return shield;
    //}

    // *************************** //
    void SkillAttack() {}

    void UltimateAttack() {}
    // *************************** //
}

/*
 Asi es como se hace un timer y pienso que puede estar 
 curioso apuntarselo para el tema del cd de las skills y tal
 
    float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        int seconds = timer % 60;
    }

 */
