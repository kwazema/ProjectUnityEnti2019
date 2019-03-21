using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stats : MonoBehaviour {

    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer enumPlayer;

    protected Map map;
    protected PlayerMovement playerMovement;
    protected Transform playerGraphic;
    protected PlayerInput playerInput;

    protected bool isShieldActive = false;
    protected bool moveToPosition = false;
    Vector2 moveToBlock;
    private bool returnOldPosition = false;
    private float timeInPosition = 0f;
    private bool noHaAtacado = true;
    
    #region Variables Protected

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
    //virtual public float GetNextFire() { return nextFire; }
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
    virtual public void SetIsShieldActive(bool value) { isShieldActive= value; }
    virtual public void SetFireRate(int value) { fireRate = value; }
    //virtual public void SetNextFire(int value) { nextFire = value; }
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

        playerGraphic = GameObject.Find(BattleChoose.namePlayer[whichIsThisPlayer] + "/GraficCharacter").GetComponent<Transform>();

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

    protected virtual void Update () {

        //Debug.Log("Shield: " + shield);
        //Debug.Log(playerGraphic.transform.position);
        //Debug.Log(playerGraphic.transform.position);

        if (moveToPosition)
        {
            MovingToPosition(60f);
        }
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

    #region Hability Skills

    public void SkillMoveTo(float cooldown, float timeToRetorn)
    {
        moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, playerGraphic.transform.position.y);
        //Debug.Log(moveTo);

        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
        }
    }

    private void MovingToPosition(float velocity)
    {
        float step = velocity * Time.deltaTime;

        if ((Vector2)playerGraphic.transform.position == moveToBlock)
        {
            returnOldPosition = true;
            
            if (noHaAtacado)
            {
                Debug.Log("Ya atacado");
                LookForwardBlocks(3/*EnumPlayer player*/);

                noHaAtacado = false;
            }
        }

        if (returnOldPosition)
        {
            if (Time.time > timeInPosition) // Time to Retorn
            {
                playerGraphic.transform.position = Vector2.MoveTowards(playerGraphic.transform.position, transform.position, step);

                if (playerGraphic.transform.position == transform.position)
                {
                    moveToPosition = false;
                    returnOldPosition = false;
                    noHaAtacado = true;
                    playerInput.enabled = true;
                }
            }
        }
        else
        {
            playerGraphic.transform.position = Vector2.MoveTowards(playerGraphic.transform.position, moveToBlock, step);

            timeInPosition = Time.time;
            timeInPosition += 0.3f;
        }
    }

    private void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0) {
        for (int i = 0; i < rangeEffectColumn; i++)
        {
            //Debug.Log("Column: " + map.columnLenth + 3);
            //Debug.Log("Column: " + ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone)));

            if (
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone))  < map.columnLenth &&
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone))  >= 0
                )
            {
                //Debug.Log("000000");
                if (map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].PlayerInThisBlock())
                {
                    //Debug.Log("Bloque --> Column: " + ((playerMovement.playerColumn + graphicMove) + i) + " Row: " + playerMovement.playerRow);
                    map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].GetPlayerStats().TakeDamage(GetDamageSkill());
                }
            }
        }
    }

    #endregion
}
