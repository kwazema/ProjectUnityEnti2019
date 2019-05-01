using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    [SerializeField] public enum ThisPlayerIs { Player1, Player2 }
    [SerializeField] public ThisPlayerIs thisPlayerIs;

    // Añadir las imagenes en el prefab
    public Sprite icon;
    public Sprite[] upgrade;
    public int numUpgrade;


    #region Classes
    public PlayerMovement playerMovement;
    protected Map map;
    protected Collider2D bodyCollider;
    protected PlayerInput playerInput;
    protected PlayerAttackInput player_att_input;
    protected GameObject player;
    protected Vector2 oldPos;
    protected GameManager game_manager;
    protected Animator anim;
    #endregion

    #region Private Variables

    // SORPRESA 

    #endregion

    #region Protected Variables

    protected Vector2 moveToBlock;
    protected float timeInPosition;

    [Header("Test Header")]

    [SerializeField]
    protected int damageBasicAttack;
    [SerializeField]
    protected int damageSkill;
    protected int damageUltimate;

    [SerializeField]
    protected const int health_max = 200;
    protected int health;

    [SerializeField]
    protected const int shield_max = 50;
    protected int shield;
    protected int recoveryShieldTime;
    
    protected float fireRate;
    //protectedected float nextFire;

    protected float skillCD;
    protected float cur_skillCD;
    public bool is_skill_ready = false;
    protected float ultimateCD;
    protected float cur_ultimateCD;
    public bool is_ultimate_ready = false;

    protected int skillDistance;
    protected int ultimateDistance;

    public int whichIsThisPlayer;
    protected int graphicMove;
    protected int dirSkillZone;

    protected bool isShieldActive = false;
    protected bool moveToPosition = false;
    protected bool returnOldPosition = false;
    protected bool noHaAtacado = true;
    protected bool cast_ended = false;
    protected bool is_ultimateOn = false;
    protected bool is_shield_broken = false;
    
    protected bool is_shootting = false;

    protected int player_to_attack;
    #endregion

    #region Public Variables
    public string namePlayer;
    #endregion

    #region Get Functions
    virtual public int GetDamageBasicAttack() { return damageBasicAttack; }
    virtual public int GetDamageSkill() { return damageSkill; }
    virtual public int GetDamageUltimate() { return damageUltimate; }

    virtual public int GetHealth() { return health; }
    virtual public int GetHealthMax() { return health_max;  }
    virtual public int GetShield() { return shield; }
    virtual public bool GetShieldState() { return is_shield_broken; }
    virtual public float GetFireRate() { return fireRate; }
    virtual public bool GetIsShieldActive() { return isShieldActive; }

    virtual public float GetSkillCD() { return skillCD; }
    virtual public float GetUltimateCD() { return ultimateCD; }

    virtual public int GetSkillDistance() { return skillDistance; }
    virtual public int GetUltimateDistance() { return ultimateDistance; }

    virtual public int GetRecoveryShieldTime() { return recoveryShieldTime; }

    virtual public int WhichIs() { return whichIsThisPlayer; }

    virtual public bool GetIsUltimateReady() { return is_ultimate_ready; }
    virtual public float GetCurUltimateCD() { return cur_ultimateCD; }

    virtual public bool GetIsSkillReady() { return is_skill_ready; }
    virtual public float GetCurSkillCD() { return cur_skillCD; }

    virtual public bool GetIsShootting() { return is_shootting; }

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

    virtual public void SetIsShootting(bool value) { is_shootting = value; }
    #endregion

    protected virtual void Awake() {
        map = GameObject.Find("Map").GetComponent<Map>();

        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        player_att_input = GetComponent<PlayerAttackInput>();

        bodyCollider = GameObject.Find(name + "/BodyCollider").GetComponent<Collider2D>();

        game_manager = FindObjectOfType<GameManager>();
        anim = GameObject.Find(name + "/GraficCharacter").GetComponent<Animator>();

        if (thisPlayerIs == ThisPlayerIs.Player2)
            GameObject.Find(name + "/BodyCollider").layer = 12;
    }

    protected virtual void Start() {
        cur_skillCD = 0;
        cur_ultimateCD = 0;
    }

    protected virtual void Update()
    {
        // ----------------------------- //

        if (shield <= 0) {
            StartCoroutine(ShieldRecovery());
            is_shield_broken = true;
        }

        // ----------------------------- //

        if (cur_skillCD == 0) {
            Debug.Log("cur_skillCD 1: " + cur_skillCD);
            StartCoroutine(SkillRecovery());
        }

        // ----------------------------- //

        if (cur_ultimateCD == 0)
            StartCoroutine(UltimateRecovery());

        // ----------------------------- //

        if (is_shootting)
            anim.SetBool("is_shooting", true);
        else
            anim.SetBool("is_shooting", false);
    }

    virtual public IEnumerator ShieldRecovery()
    {
        while (shield < shield_max)
        {
            shield += recoveryShieldTime;
            if (shield > shield_max)
                shield = shield_max;
            yield return new WaitForSeconds(1);
        }
        is_shield_broken = false;
    }

    protected IEnumerator SkillRecovery()
    {
        while (cur_skillCD < skillCD)
        {
            cur_skillCD++;
            yield return new WaitForSeconds(1);
        }
        is_skill_ready = true;
    }

    protected IEnumerator UltimateRecovery()
    {
        while (cur_ultimateCD < ultimateCD)
        {
            cur_ultimateCD++;
            yield return new WaitForSeconds(1);
        }
        is_ultimate_ready = true;
    }

    protected virtual IEnumerator CastingTime(float time_cast)
    {
        is_ultimate_ready = false;

        player_att_input.enabled = false;
        playerInput.enabled = false;
        playerMovement.enabled = false;

        float cast = 0;
        while (cast < time_cast)
        {
            cast++;
            yield return new WaitForSeconds(1);
        }

        cast_ended = true;
        is_ultimateOn = true;
        playerMovement.enabled = true;
        playerInput.enabled = true;
        player_att_input.enabled = true;
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

    public virtual void Skill(float cooldown = 0, float timeToRetorn = 0) { }

    protected virtual void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0) { }

    protected virtual void SelectedZonaPlayer() { }

    public virtual void Ultimate() { }

    protected void MovingToPosition(float velocity, int blocks_width = 0, int blocks_height = 0)
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

                if ((Vector2)transform.position == oldPos)
                {
                    moveToPosition = false;
                    returnOldPosition = false;
                    noHaAtacado = true;
                    playerInput.enabled = true;
                    playerMovement.enabled = true;
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
