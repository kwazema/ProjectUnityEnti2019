using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] public enum ThisPlayerIs { Player1, Player2 }
    [SerializeField] public ThisPlayerIs thisPlayerIs;

    [SerializeField]
    public enum Particles
    {
        Die,
        Attack,
        Skill,
        Ultimate,
        Hit,
        Move,
        UltimateCast
    }

    [SerializeField]
    public enum ParticlesSkills
    {
        Ultimate,
        Skill,
        Ultimate2
    }

    // Añadir las imagenes en el prefab
    public Sprite icon;
    public Sprite[] upgrade;
    public int numUpgrade;
    public Animator animShield;

    //public GameObject[] particles_list;
    //public Transform[] particles_pos;

    [Header("<-- Array Particles -->")]
    public ParticleSystem[] particleSystem;

    [Header("<-- Exclusive Particles -->")]
    public GameObject[] ParticlesToInstantiate;

    //public Transform particles;
    [Header("<-- Colliders -->")]
    public BoxCollider2D bodyCollider;
    public BoxCollider2D minibodyCollider;

    [Header("<-- Hitmarker -->")]
    public Transform distance_attack;
    public SpriteRenderer hitmarker_color;


    #region Classes
    public PlayerMovement playerMovement;
    protected Map map;
    protected PlayerInput playerInput;
    protected PlayerAttackInput player_att_input;
    protected GameObject player;
    protected Vector2 oldPos;
    protected GameManager game_manager;
    //protected Animator anim;
    public Animator anim;

    protected SpriteRenderer sprite;


    #endregion

    #region Private Variables

    // SORPRESA 

    #endregion

    #region Protected Variables

    protected int index;

    protected Vector2 moveToBlock;
    protected float timeInPosition;
    
    [SerializeField]
    protected int damageBasicAttack;
    [SerializeField]
    protected int damageSkill;
    protected int damageUltimate;

    [SerializeField]
    protected int health_max = 350;
    protected int health;

    [SerializeField]
    protected int shield_max = 50;
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
    protected bool can_color_white = false;

    protected int player_to_attack;

    #endregion

    #region Public Variables
    public string namePlayer;
    public string[] upgrade_text;
    #endregion

    #region Get Functions
    virtual public int GetDamageBasicAttack() { return damageBasicAttack; }
    virtual public int GetDamageSkill() { return damageSkill; }
    virtual public int GetDamageUltimate() { return damageUltimate; }

    virtual public int GetHealth() { return health; }
    virtual public int GetHealthMax() { return health_max; }
    virtual public int GetShield() { return shield; }
    virtual public int GetShieldMax() { return shield_max; }

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

    virtual public void SetRecoveryShieldTime(int value) { recoveryShieldTime = value; }

    virtual public void SetThisPlayer(int value) { whichIsThisPlayer = value; }

    virtual public void SetIsShootting(bool value) { is_shootting = value; }
    #endregion

    protected virtual void Awake()
    {
        map = GameObject.Find("Map").GetComponent<Map>();

        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        player_att_input = GetComponent<PlayerAttackInput>();

        //bodyCollider = GameObject.Find(name + "/BodyCollider").GetComponent<Collider2D>();

        game_manager = FindObjectOfType<GameManager>();
        anim = GameObject.Find(name + "/GraficCharacter").GetComponent<Animator>();

        sprite = GameObject.Find(name + "/GraficCharacter").GetComponent<SpriteRenderer>();

        //particles_GO = GameObject.Find(name + "/DieParticle");

        //particle = GameObject.Find(name + "/DieParticle").GetComponent<ParticleSystem>();
        upgrade_text = new string[3];

        LoadStatsFile();
    }

    protected virtual void Start()
    {
        cur_skillCD = 0;
        cur_ultimateCD = 0;

        is_skill_ready = false;
        is_ultimate_ready = false;

        isShieldActive = false;
        moveToPosition = false;
        returnOldPosition = false;
        noHaAtacado = true;
        cast_ended = false;
        is_ultimateOn = false;
        is_shield_broken = false;

        is_shootting = false;
        can_color_white = false;

        if (thisPlayerIs == ThisPlayerIs.Player1)
        {
            GameObject.Find(name + "/BodyCollider").layer = 11;

            hitmarker_color.color = Color.blue;
        }
        else {
            GameObject.Find(name + "/BodyCollider").layer = 12;

            hitmarker_color.color = Color.red;
        }
    }

    private void LoadStatsFile()
    {
        ListCharacters listCharacters = GameManager.instance.LoadFileToString();

        //------------------ Set Varibles Max ------------------//

        namePlayer = listCharacters.characterStats[index].name;
        //listCharacters.characterStats[index].description = desc
        //listCharacters.characterStats[index].history

        health_max = listCharacters.characterStats[index].healthMax;
        shield_max = listCharacters.characterStats[index].shieldMax;

        damageBasicAttack = listCharacters.characterStats[index].damageBasicAttack;
        damageSkill = listCharacters.characterStats[index].damageSkill;
        damageUltimate = listCharacters.characterStats[index].damageUltimate;

        skillCD = listCharacters.characterStats[index].skillCD;
        ultimateCD = listCharacters.characterStats[index].ultimateCD;

        fireRate = listCharacters.characterStats[index].fireRate;
        recoveryShieldTime = (int)listCharacters.characterStats[index].recoveryShieldTime;

        //------------------ Set Varibles Cur ------------------//

        health = health_max;
        shield = shield_max;
    }


    protected virtual void Update()
    {
        if (shield <= 0)
        {
            StartCoroutine(ShieldRecovery());
            is_shield_broken = true;
        }

        // ----------------------------- //

        if (cur_skillCD == 0)
        {
            Debug.Log("cur_skillCD 1: " + cur_skillCD);
            StartCoroutine(SkillRecovery());
        }

        // ----------------------------- //

        if (cur_ultimateCD == 0 && !is_ultimate_ready)
            StartCoroutine(UltimateRecovery());

        // ----------------------------- //

        //AnimReflectShield();
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
            //cur_skillCD++;
            //yield return new WaitForSeconds(1);
            cur_skillCD += Time.deltaTime;
            yield return null;

        }
        is_skill_ready = true;
    }

    protected IEnumerator UltimateRecovery()
    {
        while (cur_ultimateCD < ultimateCD)
        {
            //cur_ultimateCD++;
            //yield return new WaitForSeconds(1);
            cur_ultimateCD += Time.deltaTime;
            yield return null;
        }
        is_ultimate_ready = true;
    }


    // Le pasas por parametro el tiempo de casteo y si quieres que una vez acabado se reactiven los inputs.
    protected virtual IEnumerator CastingTime(float time_cast, bool value)
    {
        player_att_input.enabled = false;
        playerInput.enabled = false;

        float cast = 0;
        while (cast < time_cast)
        {
            cast++;
            yield return new WaitForSeconds(1);
        }

        cast_ended = true;

        // Esto se encarga de devolverle los inputs al player una vez acabado el casteo. Si quieres que cuando acabe el casteo se pueda mover
        // escribe true en el parametro, si quieres que no se pueda mover escribe false;
        if (value)
        {
            playerInput.enabled = true;
            player_att_input.enabled = true;
        }
    }

    virtual public IEnumerator Die2()
    {
        anim.SetTrigger("die");

        // ----------------------- //

        DeployParticles(Particles.Die);

        // ----------------------- //

        bodyCollider.enabled = false;
        minibodyCollider.enabled = false;

        // ----------------------- //

        playerInput.enabled = false;
        player_att_input.enabled = false;

        // ----------------------- //

        Color transparency = Color.white;
        transparency.a = .5f;

        // ----------------------- //

        sprite.color = transparency;

        // ----------------------- //

        yield return new WaitForSeconds(3.5f);
        //SceneManager.LoadScene("Menu");
        //FindObjectOfType<FadeImage>()
    }

    //void Die()
    //{
    //    SceneManager.LoadScene("Modojuego");
    //}

    bool activeReflect = true; float timeCur = 1;
    public void AnimReflectShield()
    {
        Debug.Log("Shield Up");
        Debug.Log("Shield Bool: " + activeReflect);

        if (activeReflect)
        {
            float randomTime = Random.Range(1, 5);
            timeCur = randomTime;
            activeReflect = false;
        }
        else
        {
            timeCur -= Time.deltaTime;
            Debug.Log("Time: " + timeCur);
            if (timeCur < 0)
            {
                animShield.SetTrigger("Reflect");
                activeReflect = true;
            }
        }
    }

    virtual public void TakeDamage(int enemyDamage)
    {
        if (isShieldActive)
        {
            shield -= enemyDamage;
            animShield.SetTrigger("Impact");
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
            StartCoroutine(Die2());
        }
    }

    public virtual void Skill() { }

    protected virtual void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0) { }

    protected virtual void SelectedZonaPlayer() { }

    public virtual void Ultimate() { }


    public virtual void Upgrade1()
    {
    }

    public virtual void Upgrade2()
    {
    }

    public virtual void Upgrade3()
    {
    }

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
                //LookForwardBlocks(blocks_width);

                StartCoroutine(LookForBlocks(blocks_width, 0.1f));

                noHaAtacado = false;
            }
        }

        if (returnOldPosition)
        {
            if (Time.time > timeInPosition)
            {
                can_color_white = true;
                transform.position = Vector2.MoveTowards(transform.position, oldPos, step);

                if ((Vector2)transform.position == oldPos)
                {
                    moveToPosition = false;
                    returnOldPosition = false;
                    noHaAtacado = true;
                    playerInput.enabled = true;
                    playerMovement.enabled = true;

                    player_att_input.enabled = true;
                    cast_ended = false;

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

    public virtual void DeployParticles(Particles value)
    {
        //particleSystem[(int)value].Play();
        particleSystem[(int)value].gameObject.SetActive(true); //<-- Esta es la valida
    }

    public void ResetCharacter()
    {
        anim.SetTrigger("iddle");

        // ----------------------- //

        health = health_max;
        shield = shield_max;

        // ----------------------- //

        cur_skillCD = 0;
        cur_ultimateCD = 0;

        // ----------------------- //

        if (thisPlayerIs == ThisPlayerIs.Player1)
            playerMovement.playerColumn = 0;
        else
            playerMovement.playerColumn = map.columnLenth - 1;

        playerMovement.playerRow = 0;

        // ----------------------- //

        Color transparency = Color.white;
        transparency.a = 1f;

        sprite.color = transparency;
        // ----------------------- //

        bodyCollider.enabled = true;
        minibodyCollider.enabled = true;

        // ----------------------- //

        playerInput.enabled = true;
        player_att_input.enabled = true;

        // ----------------------- //

        is_skill_ready = false;
        is_ultimate_ready = false;

        // ----------------------- //

        isShieldActive = false;
        moveToPosition = false;
        returnOldPosition = false;
        noHaAtacado = true;
        cast_ended = false;
        is_ultimateOn = false;
        is_shield_broken = false;
        is_shootting = false;
        can_color_white = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DeployParticles(Particles.Hit);

        if (thisPlayerIs == ThisPlayerIs.Player1)
        {
            game_manager.playerManager[0].TakeDamage(GetDamageBasicAttack());
        }
        else
        {
            game_manager.playerManager[1].TakeDamage(GetDamageBasicAttack());
        }
    }

    public void SetPlayerInputs(bool value)
    {
        playerInput.enabled = value;
        player_att_input.enabled = value;
    }

    protected virtual IEnumerator LookForBlocks(int rangeEffectColumn, float time)
    {
        yield return null;
    }

    protected Vector2Int[] GetRandomBlocks(int blocks)
    {
        Vector2Int[] blocks_affected;
        int i = 0;
        int blocks_created = 0;
        blocks_affected = new Vector2Int[blocks];

        int init_pos_x;
        int max_pos_x;
        if (player_to_attack == 0)
        {
            init_pos_x = 0;
            max_pos_x = init_pos_x + (map.columnLenth) / 2;
        }
        else
        {
            init_pos_x = map.columnLenth - 1;
            max_pos_x = init_pos_x - (map.columnLenth) / 2;
        }

        while (i < blocks)
        {
            Vector2Int cpy = new Vector2Int(Random.Range(init_pos_x, max_pos_x), Random.Range(0, map.rowLenth));

            bool is_finded = false;
            for (int j = 0; j < blocks_created && !is_finded; j++)
            {
                if (blocks_affected[j] == cpy)
                {
                    is_finded = true;
                }
            }

            if (!is_finded)
            {
                blocks_affected[i] = cpy;
                i++;
                blocks_created++;
            }
        }

        return blocks_affected;
    }

}