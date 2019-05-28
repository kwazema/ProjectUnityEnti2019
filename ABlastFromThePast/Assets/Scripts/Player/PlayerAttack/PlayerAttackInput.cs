using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class PlayerAttackInput : MonoBehaviour
{
    public enum EnumPlayer { Player1, Player2 }
    public EnumPlayer enumPlayer;

    public GameManager gameManager;
    PlayerManager[] playerManager;

    PlayerMovement playerMove;

    public Transform basicShotSpawn;
    public GameObject basicAttack;
    public SpriteRenderer shieldRender;

    private GamePadState state;
    private GamePadState prevState;

    int numPlayer;
    public bool is_ultOn = false;
    public bool is_skillOn = false;

    #region  Variables
    private float nextFire = 0.0f;
    //private bool isShieldActive = false;
    private bool isShooting = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();

        playerManager = new PlayerManager[2];
        playerManager[0] = gameManager.playerManager[0];
        playerManager[1] = gameManager.playerManager[1];

        numPlayer = (int)enumPlayer;

        if (numPlayer == 1)
        {
            playerManager[numPlayer].floatingText.GetComponent<Transform>().transform.Rotate(0, 180, 0);
            playerManager[numPlayer].floatingText.GetComponent<Transform>().transform.position = new Vector3(playerManager[numPlayer].floatingText.GetComponent<Transform>().transform.position.x, playerManager[numPlayer].floatingText.GetComponent<Transform>().transform.position.y, -2f);
        }
        state = GamePad.GetState((PlayerIndex)enumPlayer);

        Debug.Log("State: " + state.PacketNumber);
        Debug.Log("Enum Player: " + enumPlayer);
    }

    void Update()
    {
        //prevState = state;

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
        if (Input.GetButton("Attack0") && !playerManager[0].GetIsShieldActive() && !is_skillOn && !is_ultOn)
            playerManager[0].anim.SetBool("attack", true);
        else
            playerManager[0].anim.SetBool("attack", false);

        // ----------------------- // 

        if (Input.GetButton("Attack0") && !playerMove.GetIsMoving() && Time.time > nextFire && !playerManager[0].GetIsShieldActive())
        {
            BasicAttack();
        }
        else {
            playerManager[0].SetIsShootting(false);
        }

        // ----------------------- //

        if (!playerManager[0].GetIsShootting() && !playerManager[0].GetIsShieldActive() && !is_ultOn)
            if (Input.GetButtonDown("Skill0") && playerManager[0].GetIsSkillReady() && !playerMove.GetIsMoving())
                SkillAttack();

        // ----------------------- //

        if (!playerManager[0].GetIsShootting() && !playerManager[0].GetIsShieldActive() && !is_skillOn)
        {
            if (Input.GetButton("Ultimate0") && playerManager[0].is_ultimate_ready && !playerMove.GetIsMoving()) 
                UltimateAttack();

            //if (Input.GetAxisRaw("UltimateAxis0") > 0 && playerManager[0].is_ultimate_ready && !playerMove.GetIsMoving())
            //    UltimateAttack();
        }

        // ----------------------- //

        if (!is_skillOn && !is_ultOn) {
            if ((state.Triggers.Right > 0.25f || Input.GetButton("Shield0")) && !playerManager[0].GetShieldState() && !playerManager[0].GetIsShootting())
                ActiveShield();
            else
                DeactivateShield();
        }

        if ((state.Triggers.Right > 0.25f || Input.GetButton("Shield0")) && !playerManager[0].GetShieldState() && !playerManager[0].GetIsShootting())
        {
            playerManager[numPlayer].AnimReflectShield();
        }

    }

    private void GetInputPlayer2()
    {
        if (Input.GetButton("Attack1") && !playerManager[1].GetIsShieldActive())
            playerManager[1].anim.SetBool("attack", true);
        else
            playerManager[1].anim.SetBool("attack", false);

        // ----------------------- // 

        if (Input.GetButton("Attack1") && !playerMove.GetIsMoving() && Time.time > nextFire && !playerManager[1].GetIsShieldActive())
            BasicAttack();
        else
            playerManager[1].SetIsShootting(false);

        // ----------------------- //

        if (!playerManager[1].GetIsShootting() && !playerManager[1].GetIsShieldActive())
            if (Input.GetButtonDown("Skill1") && playerManager[1].GetIsSkillReady() && !playerMove.GetIsMoving())
                SkillAttack();

        // ----------------------- //

        if (!playerManager[1].GetIsShootting() && !playerManager[1].GetIsShieldActive() && !is_skillOn && !is_ultOn)
            if (Input.GetButton("Ultimate1") && playerManager[1].is_ultimate_ready && !playerMove.GetIsMoving())
                UltimateAttack();

           //if (Input.GetAxisRaw("UltimateAxis1") > 0 && playerManager[0].is_ultimate_ready && !playerMove.GetIsMoving())
           //     UltimateAttack();

        // ----------------------- //

        if (!is_skillOn && !is_ultOn)
        {
            if ((state.Triggers.Right > 0.25f || Input.GetButton("Shield1")) && !playerManager[1].GetShieldState() && !playerManager[1].GetIsShootting())
                ActiveShield();
            else
                DeactivateShield();
        }

        if ((state.Triggers.Right > 0.25f || Input.GetButton("Shield1")) && !playerManager[1].GetShieldState() && !playerManager[1].GetIsShootting())
        {
            playerManager[numPlayer].AnimReflectShield();
        }
    }

    void BasicAttack()
    {
        
        playerManager[(int)enumPlayer].SetIsShootting(true);
        
        // Cada vez que disparas te iguala el time.time y despues le sumas el fireRate 
        // sino hasta que el nextFire no sea mayor a Time.Time actual no dejara de disparar
        nextFire = Time.time;
        for (int i = 0; i < 2; i++)
        {
            nextFire += Time.deltaTime + playerManager[i].GetFireRate();
        }

        GameObject basicAttackClone = (GameObject)Instantiate(basicAttack, basicShotSpawn.position, basicShotSpawn.rotation);
        basicAttackClone.transform.rotation = transform.rotation;

        // ----------------------------------------- //

        if (EnumPlayer.Player1 == enumPlayer)
        {
            // layer attack 1
            basicAttackClone.layer = 15;
        }
        else
        {
            // layer attack 2
            basicAttackClone.layer = 16;
        }
    }

    private void ActiveShield()
    {
        playerManager[numPlayer].floatingText.SetActive(true);
        playerManager[numPlayer].SetIsShieldActive(true);
        shieldRender.enabled = true;
        playerManager[numPlayer].AnimReflectShield();
    }

    private void DeactivateShield()
    {
        playerManager[numPlayer].floatingText.SetActive(false);
        playerManager[numPlayer].SetIsShieldActive(false);
        shieldRender.enabled = false;
    }

    private void SkillAttack()
    {
        is_skillOn = true;

        playerManager[numPlayer].Skill();
    }

    private void UltimateAttack()
    {
        is_ultOn = true;

        playerManager[1].anim.SetBool("attack", false);

        playerManager[numPlayer].Ultimate();
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{

    //    if (EnumPlayer.Player1 == enumPlayer)
    //    {
    //        playerManager[0].TakeDamage(playerManager[1].GetDamageBasicAttack());
    //    }
    //    else
    //    {
    //        playerManager[1].TakeDamage(playerManager[0].GetDamageBasicAttack());
    //    }
    //}
}