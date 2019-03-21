using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Stats : MonoBehaviour {

    //Map map;
    protected bool isShieldActive = false;

    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer enumPlayer;

    protected Map map;
    protected PlayerMovement playerMovement;
    protected Transform playerGraphic;
    protected PlayerInput playerInput;

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
    protected int dirSkillArea;

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

    // Use this for initialization
    protected virtual void Start () {
        map = GameObject.Find("Map").GetComponent<Map>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        //playerGraphic = GameObject.Find("Player" + (whichIsThisPlayer + 1) + "/GraficCharacter").GetComponent<Transform>();

        playerGraphic = GameObject.Find(BattleChoose.namePlayer[whichIsThisPlayer] + "/GraficCharacter").GetComponent<Transform>();

        if (whichIsThisPlayer == 0)
        {
            graphicMove = 4;
            dirSkillArea = 1;
        }
        else
        {
            graphicMove = -4;
            dirSkillArea = -1;
        }
        Debug.Log("Grafic: " + graphicMove);
    }

    // Update is called once per frame
    protected virtual void Update () {
        //Debug.Log("Shield: " + shield);
        //MoveToPosition();
        //Debug.Log(playerGraphic.transform.position);

        //Debug.Log(playerGraphic.transform.position);
        if (moveToPosition)
        {
            MoveToPosition(60f);
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

    void Die()
    {
        SceneManager.LoadScene("Modojuego");
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


    protected bool moveToPosition = false;
    Vector2 moveTo;
    public void SkillMoveTo(float cooldown, float timeToRetorn) {

        moveTo = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, playerGraphic.transform.position.y);
        //Debug.Log(moveTo);

        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
        }
    }

    bool moveToOldPosition = false;
    float waitPosition = 0f;
    bool noHaAtacado = true;
    public void MoveToPosition(float velocity)
    {
        float step = velocity * Time.deltaTime;

        if ((Vector2)playerGraphic.transform.position == moveTo)
        {
            moveToOldPosition = true;
            
            if (noHaAtacado)
            {
                Debug.Log("Ya atacado");
                LookForwardBlocks(/*EnumPlayer player*/);

                noHaAtacado = false;
            }
        }

        if (moveToOldPosition)
        {
            if (Time.time > waitPosition) // Time to Retorn
            {
                playerGraphic.transform.position = Vector2.MoveTowards(playerGraphic.transform.position, transform.position, step);

                if (playerGraphic.transform.position == transform.position)
                {
                    moveToPosition = false;
                    moveToOldPosition = false;
                    noHaAtacado = true;
                    playerInput.enabled = true;
                }
            }
        }
        else
        {
            playerGraphic.transform.position = Vector2.MoveTowards(playerGraphic.transform.position, moveTo, step);

            waitPosition = Time.time;
            waitPosition += 0.3f;
        }
    }


    private void MoveToXPos() {}

    virtual public void LookForwardBlocks(/*EnumPlayer player*/) {
        //for (int i = 0; i < 3; i++)
        for (int i = 0; i < 3 ; i++)
        {
            //Debug.Log("Column: " + map.columnLenth + 3);
            Debug.Log("Column: " + ((playerMovement.playerColumn + graphicMove) + (i * dirSkillArea)));

            if (
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillArea))  < map.columnLenth &&
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillArea))  >= 0
                )
            {
                Debug.Log("000000");
                if (map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillArea), playerMovement.playerRow].PlayerInThisBlock())
                {
                    //if (map.blocks[(playerMove.playerColumn - graphicMove) - i, playerMove.playerRow] == map.blocks[playerMove.playerColumn , playerMove.playerRow] && transform.rotation.y > 0)
                    Debug.Log("Bloque --> Column: " + ((playerMovement.playerColumn + graphicMove) + i) + " Row: " + playerMovement.playerRow);
                    map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillArea), playerMovement.playerRow].GetPlayerStats().TakeDamage(GetDamageSkill());
                }
            }

            //if (map.blocks[(playerMove.playerColumn - graphicMove)-skillArea , playerMove.playerRow] == map.blocks[playerMove.playerColumn, playerMove.playerRow])
            //{
            //    Debug.Log("HEREEEEEEEEEEEEEEEEEEEEEEEEE::");
            //}

        }

    }
}
