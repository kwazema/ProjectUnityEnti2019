using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    int numPlayer;

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
        playerManager[0] = gameManager.playerStats[0];
        playerManager[1] = gameManager.playerStats[1];

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
        if (Input.GetButton("Attack0") && !playerMove.GetIsMoving() && Time.time > nextFire)
            BasicAttack();
        else
            isShooting = false;

        // ----------------------- //

        if (Input.GetButton("Skill0"))
            SkillAttack();

        // ----------------------- //

        if (Input.GetButton("Ultimate0"))
            UltimateAttack();

        // ----------------------- //

        if (Input.GetButton("Shield0") && playerManager[0].GetShield() > 0 && !isShooting)
            ActiveShield();
        else
            DeactivateShield();
    }

    private void GetInputPlayer2()
    {
        if (Input.GetButton("Attack1") && !playerMove.GetIsMoving() && Time.time > nextFire)
            BasicAttack();
        else
            isShooting = false;

        // ----------------------- //

        if (Input.GetButtonDown("Skill1"))
            SkillAttack();

        // ----------------------- //

        if (Input.GetButtonDown("Ultimate1"))
            UltimateAttack();

        // ----------------------- //

        if (Input.GetButton("Shield1") && playerManager[1].GetShield() > 0 && !isShooting)
            ActiveShield();
        else
            DeactivateShield();
    }

    void BasicAttack()
    {
        // Cada vez que disparas te iguala el time.time y despues le sumas el fireRate 
        // sino hasta el nextFire no sea mayor a Time.Time actual no dejara de disparar
        nextFire = Time.time;
        for (int i = 0; i < 2; i++)
        {
            nextFire += Time.deltaTime + playerManager[i].GetFireRate();
        }

        GameObject basicAttackClone = (GameObject)Instantiate(basicAttack, basicShotSpawn.position, basicShotSpawn.rotation);
        basicAttackClone.transform.rotation = transform.rotation;

        isShooting = true;
    }

    private void ActiveShield()
    {
        playerManager[numPlayer].SetIsShieldActive(true);
        shieldRender.enabled = true;
    }

    private void DeactivateShield()
    {
        playerManager[numPlayer].SetIsShieldActive(false);
        shieldRender.enabled = false;
    }

    private void SkillAttack()
    {
        playerManager[numPlayer].SkillMoveTo(0f, 0f);
    }

    private void UltimateAttack()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (EnumPlayer.Player1 == enumPlayer)
        {
            playerManager[0].TakeDamage(playerManager[1].GetDamageBasicAttack());
        }
        else
        {
            playerManager[1].TakeDamage(playerManager[0].GetDamageBasicAttack());
        }
    }
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
