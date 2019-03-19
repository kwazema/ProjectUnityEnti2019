using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
{
    public enum EnumPlayer { Player1, Player2 }
    public EnumPlayer enumPlayer;

    public Game game;
    Stats[] player;

    PlayerMovement playerMove;

    public Transform basicShotSpawn;
    public GameObject basicAttack;
    public SpriteRenderer shieldRender;

    int numPlayer;

    #region  Variables
    private float nextFire = 0.0f;
    private bool isShieldActive = false;
    private bool isShooting = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        game = GameObject.Find("Map").GetComponent<Game>();

        player = new Stats[2];
        player[0] = game.playerStats[0];
        player[1] = game.playerStats[1];

        numPlayer = (int)enumPlayer;
    }
     
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (EnumPlayer.Player1 == enumPlayer)
            GetInputPlayer1();
        else
            GetInputPlayer2();
    }

    private void GetInputPlayer1()
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

        if (Input.GetKey(KeyCode.M))
        {
            if (player[0].GetShield() > 0 && !isShooting)
                ActiveShield();
        }

        if (Input.GetKeyUp(KeyCode.M) || (player[0].GetShield() < 0))
        {
            DeactivateShield();
        }
    }

    private void GetInputPlayer2()
    {
        if (
          Input.GetKey(KeyCode.Keypad1) &&
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

        if (Input.GetKeyDown(KeyCode.Keypad2))
            SkillAttack();

        if (Input.GetKeyDown(KeyCode.Keypad3))
            UltimateAttack();

        if (Input.GetKey(KeyCode.Keypad0))
        {
            if (player[1].GetShield() > 0 && !isShooting)
                ActiveShield();
        }

        if (Input.GetKeyUp(KeyCode.Keypad0) || (player[1].GetShield() < 0))
        {
            DeactivateShield();
        }
    }

    void BasicAttack()
    {
        // Cada vez que disparas te iguala el time.time y despues le sumas el fireRate 
        // sino hasta el nextFire no sea mayor a Time.Time actual no dejara de disparar
        nextFire = Time.time;
        for (int i = 0; i < 2; i++)
        {
            nextFire += Time.deltaTime + player[i].GetFireRate();
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

    //void SkillAttack()
    //{
    //    if (EnumPlayer.Player1 == enumPlayer)
    //        SkillAttackPlayer1();
    //    else
    //        SkillAttackPlayer2();
    //}
    //void UltimateAttack() {}


    private void SkillAttack()
    {
        Debug.Log("NumPlayer: " + numPlayer);
        player[numPlayer].SkillMoveTo();
        player[1].LookForwardBlocks();
    }

    private void UltimateAttack()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (EnumPlayer.Player1 == enumPlayer)
        {
            player[0].TakeDamage(player[1].GetDamageBasicAttack());
        }
        else
        {
            player[1].TakeDamage(player[0].GetDamageBasicAttack());
        }
    }


    //private void LookForwardBlocks()
    //{
        
    //}

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
