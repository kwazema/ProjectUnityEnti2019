using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {


    protected bool isShieldActive = false;

    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer enumPlayer;

    #region Variables
    [SerializeField]
    protected int damageBasicAttack;
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


    //public int idPlayer;
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

    #endregion

    #region Set Functions
    virtual public void SetDamageBasicAttack(int value) { damageBasicAttack = value; }
    virtual public void SetDamageSkill(int value) { damageSkill = value; }
    virtual public void SetDamageUltimate(int value) { damageUltimate = value; }
    virtual public void SetHealth(int value) { health = value; }
    virtual public void SetShield(int value) { shield = value; }
    virtual public void SetFireRate(int value) { fireRate = value; }
    //virtual public void SetNextFire(int value) { nextFire = value; }
    virtual public void SetSkillCD(int value) { skillCD = value; }
    virtual public void SetUltimateCD(int value) { ultimateCD = value; }
    virtual public void SetSkillDistance(int value) { skillDistance = value; }
    virtual public void SetUltimateDistance(int value) { ultimateDistance = value; }
    virtual public void SetRecoveryShieldTime(int value) {  recoveryShieldTime = value; }
    #endregion

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    protected virtual void Update () {
        //Debug.Log("Shield: " + shield);
    }

    virtual public void TakeDamage(int enemyDamage)
    {
        if (isShieldActive)
        {
            shield -= enemyDamage;

            if (shield < 0)
            {
                health += shield;
                shield = 0;
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
        Destroy(gameObject);
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

    virtual public void SkillMoveTo() {}

    private void MoveToXPos() {}

    virtual public void LookForwardBlocks(/*EnumPlayer player*/) {}
}
