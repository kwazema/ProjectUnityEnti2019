using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour {

    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer enumPlayer;

    protected Map map;
    protected PlayerMovement playerMovement;
    protected Collider2D bodyCollider;
    protected PlayerInput playerInput;

    protected GameObject player;
    Vector2 oldPos;

    #region Variables Private

    private bool isShieldActive = false;
    private bool moveToPosition = false;
    private bool returnOldPosition = false;
    private bool noHaAtacado = true;

    #endregion

    #region Variables Protected

    protected Vector2 moveToBlock;
    protected float timeInPosition;

    [SerializeField]
    protected int damageBasicAttack;
    [SerializeField]
    protected int damageSkill;
    protected int damageUltimate;

    [SerializeField]
    protected int health;

    [SerializeField]
    protected int shield;
    protected int recoveryShieldTime;

    protected float fireRate;
    //protectedected float nextFire;

    protected float skillCD;
    protected float ultimateCD;

    protected int skillDistance;
    protected int ultimateDistance;


    public int whichIsThisPlayer;
    protected int graphicMove;
    protected int dirSkillZone;

    #endregion

    #region Get Functions
    virtual public int GetDamageBasicAttack() { return damageBasicAttack; }
    virtual public int GetDamageSkill() { return damageSkill; }
    virtual public int GetDamageUltimate() { return damageUltimate; }

    virtual public int GetHealth() { return health; }
    virtual public int GetShield() { return shield; }
    virtual public float GetFireRate() { return fireRate; }
    virtual public bool GetIsShieldActive() { return isShieldActive; }

    virtual public float GetSkillCD() { return skillCD; }
    virtual public float GetUltimateCD() { return ultimateCD; }

    virtual public int GetSkillDistance() { return skillDistance; }
    virtual public int GetUltimateDistance() { return ultimateDistance; }

    virtual public int GetRecoveryShieldTime() { return recoveryShieldTime; }

    virtual public int WhichIs() { return whichIsThisPlayer; }
    #endregion

    #region Set Functions
    virtual public void SetDamageBasicAttack(int value) { damageBasicAttack = value; }
    virtual public void SetDamageSkill(int value) { damageSkill = value; }
    virtual public void SetDamageUltimate(int value) { damageUltimate = value; }

    virtual public void SetHealth(int value) { health = value; }
    virtual public void SetShield(int value) { shield = value; }
    virtual public void SetFireRate(int value) { fireRate = value; }
    virtual public void SetIsShieldActive(bool value) { isShieldActive = value; }

    virtual public void SetSkillCD(int value) { skillCD = value; }
    virtual public void SetUltimateCD(int value) { ultimateCD = value; }

    virtual public void SetSkillDistance(int value) { skillDistance = value; }
    virtual public void SetUltimateDistance(int value) { ultimateDistance = value; }

    virtual public void SetRecoveryShieldTime(int value) {  recoveryShieldTime = value; }

    virtual public void SetThisPlayer(int value) { whichIsThisPlayer = value; }
    #endregion

    protected virtual void Start () {
        map = GameObject.Find("Map").GetComponent<Map>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();

        bodyCollider = GameObject.Find(BattleChoose.namePlayer[whichIsThisPlayer] + "/BodyCollider").GetComponent<Collider2D>();

        SelectedZonaPlayer();
    }

    protected virtual void Update () {

        if (moveToPosition)
        {
            MovingToPosition(60f);
        }
    }

    virtual public IEnumerator ShieldRecovery()
    {
        while (true)
        { // loops forever...
            if (
                shield < 20 &&
                !isShieldActive
                )
            {
                shield += recoveryShieldTime;
                if (shield > 20)
                    shield = 20;
                //playerStats.SetShield(RecoveryShield()); 
                // increase health and wait the specified time
                yield return new WaitForSeconds(1);
            }
            else
            { // if shieldHealth >= 100, just yield 
                yield return null;
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Modojuego");
    }

    virtual public void TakeDamage(int enemyDamage)
    {
        if (isShieldActive)
        {
            shield -= enemyDamage;
            if (shield < 0)
            {
                shield = 0;
                health += shield;
            }
        }
        else
        {
            health -= enemyDamage;
        }

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    #region Hability Skills

    public void SkillMoveTo(float cooldown = 0, float timeToRetorn = 0)
    {
        oldPos = (Vector2)transform.position;
        moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, transform.position.y);
      
        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
        }
    }

    private void MovingToPosition(float velocity)
    {
        float step = velocity * Time.deltaTime;

        if ((Vector2)transform.position == moveToBlock)
        {
            // Collider
            bodyCollider.enabled = true;
            returnOldPosition = true;

            if (noHaAtacado)
            {
                LookForwardBlocks(3);

                noHaAtacado = false;
            }
        }

        if (returnOldPosition)
        {
            if (Time.time > timeInPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, oldPos, step);
                //Collider 
                bodyCollider.enabled = false;

                if ((Vector2)transform.position == oldPos)
                {
                    moveToPosition = false;
                    returnOldPosition = false;
                    noHaAtacado = true;
                    playerInput.enabled = true;
                    playerMovement.enabled = true;

                    //Collider 
                    bodyCollider.enabled = true;
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, moveToBlock, step);
            //Collider 
            bodyCollider.enabled = false;            

            playerMovement.enabled = false;

            timeInPosition = Time.time;
            timeInPosition += 0.3f;
        }
    }

    private void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0) {
        for (int i = 0; i < rangeEffectColumn; i++)
        {
            if (
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone))  < map.columnLenth &&
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone))  >= 0
                )
            {
                map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].spriteBlock.color = Color.red;

                if (map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].IsPlayerInThisBlock())
                {
                    
                    map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].GetPlayerStatsBlock().TakeDamage(GetDamageSkill());

                }
            }
        }
    }

    #endregion

    #region Hability Ultimate

    #endregion

    private void SelectedZonaPlayer()
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
        Debug.Log("Grafic: " + graphicMove);
    }
}
