using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    #region Variables
    protected int damageBasicAttack;
    protected int damageSkill;
    protected int damageUltimate;
    protected int health;
    protected int shield;

    protected float fireRate;
    protected float nextFire;

    protected float skillCD;
    protected float ultimateCD;

    protected int skillDistance;
    protected int ultimateDistance;
    #endregion

    #region Get Functions
    virtual public int GetDamageBasicAttack() { return damageBasicAttack; }
    virtual public int GetDamageSkill() { return damageSkill; }
    virtual public int GetDamageUltimate() { return damageUltimate; }
    virtual public int GetHealth() { return health; }
    virtual public float GetShield() { return shield; }
    virtual public float GetFireRate() { return fireRate; }
    virtual public float GetNextFire() { return nextFire; }
    virtual public float GetSkillCD() { return skillCD; }
    virtual public float GetUltimateCD() { return ultimateCD; }
    virtual public int GetSkillDistance() { return skillDistance; }
    virtual public int GetUltimateDistance() { return ultimateDistance; }
    #endregion

    #region Set Functions
    virtual public void SetDamageBasicAttack(int value) { damageBasicAttack = value; }
    virtual public void SetDamageSkill(int value) { damageSkill = value; }
    virtual public void SetDamageUltimate(int value) { damageUltimate = value; }
    virtual public void SetHealth(int value) { health = value; }
    virtual public void SetShield(int value) { shield = value; }
    virtual public void SetFireRate(int value) { fireRate = value; }
    virtual public void SetNextFire(int value) { nextFire = value; }
    virtual public void SetSkillCD(int value) { skillCD = value; }
    virtual public void SetUltimateCD(int value) { ultimateCD = value; }
    virtual public void SetSkillDistance(int value) { skillDistance = value; }
    virtual public void SetUltimateDistance(int value) { ultimateDistance = value; }
    #endregion

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    protected virtual void Update () {
		
	}
}
