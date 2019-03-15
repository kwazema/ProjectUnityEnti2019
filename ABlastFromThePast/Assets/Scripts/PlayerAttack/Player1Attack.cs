using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Attack : MonoBehaviour
{

    Player1Movement playerMove;

    public Transform basicShotSpawn;
    public GameObject basicAttack;
    public SpriteRenderer shieldRender;

    #region  Variables
    private const float fireRate = 0.2f;
    private float nextFire = 0.0f;
    private int shieldHealth;
    private const int maxShieldHealth = 20;
    private const int recoveryShieldTime = 1;


    private bool isShieldActive = false;
    private bool isShooting = false;

    #endregion

    // Use this for initialization
    void Start()
    {
        playerMove = GetComponent<Player1Movement>();
        
        //shieldHealth = maxShieldHealth;
        shieldHealth = 10;

        /* <-- Funcion que siempre comprueba y añade vida al escudo por cada segundo --> */
        StartCoroutine(ShieldRecovery()); 
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        //Debug.Log("Shield: " + shieldHealth);
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
        if (Input.GetKey(KeyCode.M))
        {
            if (shieldHealth > 0 && !isShooting)
                ActiveShield(); 
        }

        //if (Input.GetKeyUp(KeyCode.M) || (shieldHealth < 0) || isShooting)
        if (Input.GetKeyUp(KeyCode.M) || (shieldHealth < 0))
        {
            DeactivateShield();
        }
    }

    void BasicAttack()
    {
        Debug.Log("Basic Attack");

        // Cada vez que disparas te iguala el time.time y despues le sumas el fireRate 
        // sino hasta el nextFire no sea mayor a Time.Time actual no dejara de disparar
        nextFire = Time.time; 
        nextFire += Time.deltaTime + fireRate;
       
        Debug.Log("Next Fire: " + nextFire);
        Instantiate(basicAttack, basicShotSpawn.position, basicShotSpawn.rotation);
        isShooting = true;
    }

    private void ActiveShield()
    {
        Debug.Log("Activando Protocolo Ruedines.");
        isShieldActive = true;
        shieldRender.enabled = true;
    }

    private void DeactivateShield()
    {
        Debug.Log("Descactivando Protocolo Ruedines.");
        isShieldActive = false;
        shieldRender.enabled = false;
    }

    IEnumerator ShieldRecovery()
    {
        while (true)
        { // loops forever...
            if (
                shieldHealth < 20 &&
                !isShieldActive
                )
            {
                shieldHealth += recoveryShieldTime; // increase health and wait the specified time
                yield return new WaitForSeconds(1);
            }
            else
            { // if shieldHealth >= 100, just yield 
                yield return null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isShieldActive)
        {
            Debug.Log("Hitted shield");
            shieldHealth -= 5;
            if (shieldHealth <= 0)
            {
                // <-- Player pierde vida --> //
            }
        }
        else // <-- Si no esta activado el escudo directamente pierde vida --> //
        {
            Debug.Log("Hitted");
            //if (player.isAlive())
            //{
            //    // <-- Player pierde vida --> //
            //}
        }
    }


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
