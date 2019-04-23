using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    [SerializeField] public enum ThisPlayerIs { Player1, Player2 }
    [SerializeField] public ThisPlayerIs thisPlayerIs;

    protected Map map;
    protected PlayerMovement playerMovement;
    protected Collider2D bodyCollider;
    protected PlayerInput playerInput;

    protected GameObject player;
    protected Vector2 oldPos;

    #region Private Variables

    // SORPRESA 

    #endregion

    #region Protected Variables

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

    protected bool isShieldActive = false;
    protected bool moveToPosition = false;
    protected bool returnOldPosition = false;
    protected bool noHaAtacado = true;

    #endregion

    #region Public Variables
    public string namePlayer;

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

    protected virtual void Awake() {
        map = GameObject.Find("Map").GetComponent<Map>();

        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();

        bodyCollider = GameObject.Find(name + "/BodyCollider").GetComponent<Collider2D>();
    }

    protected virtual void Start() {
        //SelectedZonaPlayer();
    }

    protected virtual void Update() {

        if (moveToPosition)
        {
            //MovingToPosition(60f);
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

    //#region Hability Skills

    public virtual void SkillMoveTo(float cooldown = 0, float timeToRetorn = 0) { }

    protected virtual void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0) { }

    protected virtual void SelectedZonaPlayer() { }

    protected void MovingToPosition(float velocity, int blocks_width = 0)
    {
        float step = velocity * Time.deltaTime;

        if ((Vector2)transform.position == moveToBlock)
        {
            // Collider
            bodyCollider.enabled = true;
            returnOldPosition = true;

            if (noHaAtacado)
            {
                LookForwardBlocks(blocks_width);

                noHaAtacado = false;
            }
        }

        if (returnOldPosition)
        {
            if (Time.time > timeInPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, oldPos, step);
                // Collider 
                bodyCollider.enabled = false;

                if ((Vector2)transform.position == oldPos)
                {
                    moveToPosition = false;
                    returnOldPosition = false;
                    noHaAtacado = true;
                    playerInput.enabled = true;
                    playerMovement.enabled = true;

                    // Collider 
                    bodyCollider.enabled = true;

                    // Block Color
                    for (int i = 0; i < blocks_width; i++)
                    {
                        map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].spriteBlock.color = Color.green;
                    }
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
}
