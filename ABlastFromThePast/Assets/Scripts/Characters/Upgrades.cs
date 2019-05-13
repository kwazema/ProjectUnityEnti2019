using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades {
    // -------------------------------------- //

    protected int health_upgrade;
    protected int new_health;

    // -------------------------------------- //

    protected int shield_upgrade;
    protected int new_shield;

    // -------------------------------------- //

    protected int shield_recovery_upgrade;
    protected int new_shield_recovery;

    // -------------------------------------- //

    protected int base_damage_upgrade;
    protected int new_base_damage;

    // -------------------------------------- //

    protected float fireRate_time_upgrade;
    protected float new_fireRate;

    // -------------------------------------- //

    protected int skill_damage_upgrade;
    protected int new_skill_damage;

    // -------------------------------------- //

    protected int skill_time_upgrade;
    protected int new_skill_time;

    // -------------------------------------- //

    protected int ultimate_damage_upgrade;
    protected int new_ultimate_damage;

    // -------------------------------------- //

    protected int ultimate_time_upgrade;
    protected int new_ultimate_time;

    // -------------------------------------- //

    protected string[] descriptions;

    // -------------------------------------- //

    public virtual void Upgrade1()
    {
        descriptions[0] = "Hola mejoras vida";
    }

    public virtual void Upgrade2()
    {
        descriptions[1] = "Hola mejoras daño";
    }

    public virtual void Upgrade3()
    {
        descriptions[2] = "Hola mejoras ulti";
    }
}

[System.Serializable]
public class USanta : Upgrades
{
    Santa santa; 

    void Start() {
        santa = GameObject.Find("Santa").GetComponent<Santa>();
    }

    public override void Upgrade1()
    {
        new_health = santa.GetHealthMax() + health_upgrade;
        new_shield = santa.GetShieldMax() + shield_upgrade;

        santa.SetHealth(new_health);
        santa.SetShield(new_shield);
    }

    public override void Upgrade2()
    {
        new_base_damage = santa.GetDamageBasicAttack() + base_damage_upgrade;
        new_skill_damage = santa.GetDamageSkill() + skill_damage_upgrade;

        santa.SetDamageBasicAttack(new_base_damage);
        santa.SetDamageSkill(new_skill_damage);
    }

    public override void Upgrade3()
    {
        new_ultimate_damage = santa.GetDamageUltimate() + ultimate_damage_upgrade;

        santa.SetDamageUltimate(new_ultimate_damage);
    }
}

[System.Serializable]
public class UMinos : Upgrades
{
    Minos minos;

    void Start()
    {
        minos = GameObject.Find("Santa").GetComponent<Minos>();
    }

    public override void Upgrade1()
    {
        new_health = minos.GetHealthMax() + health_upgrade;
        new_shield = minos.GetShieldMax() + shield_upgrade;

        minos.SetHealth(new_health);
        minos.SetShield(new_shield);
    }

    public override void Upgrade2()
    {
        new_base_damage = minos.GetDamageBasicAttack() + base_damage_upgrade;
        new_skill_damage = minos.GetDamageSkill() + skill_damage_upgrade;

        minos.SetDamageBasicAttack(new_base_damage);
        minos.SetDamageSkill(new_skill_damage);
    }

    public override void Upgrade3()
    {
        new_ultimate_damage = minos.GetDamageUltimate() + ultimate_damage_upgrade;

        minos.SetDamageUltimate(new_ultimate_damage);
    }
}

[System.Serializable]
public class UPolyphemus : Upgrades
{
    Polyphemus poly;

    void Start()
    {
        poly = GameObject.Find("Santa").GetComponent<Polyphemus>();
    }

    public override void Upgrade1()
    {
        new_health = poly.GetHealthMax() + health_upgrade;
        new_shield = poly.GetShieldMax() + shield_upgrade;

        poly.SetHealth(new_health);
        poly.SetShield(new_shield);
    }

    public override void Upgrade2()
    {
        new_base_damage = poly.GetDamageBasicAttack() + base_damage_upgrade;
        new_skill_time = (int)poly.GetSkillCD() - skill_time_upgrade;

        poly.SetDamageBasicAttack(new_base_damage);
        poly.SetSkillCD(new_skill_time);
    }

    public override void Upgrade3()
    {
        new_ultimate_damage = poly.GetDamageUltimate() + ultimate_damage_upgrade;

        poly.SetDamageUltimate(new_ultimate_damage);
    }
}

[System.Serializable]
public class UAdventurer : Upgrades
{
    Adventurer adventurer;

    void Start()
    {
        adventurer = GameObject.Find("Santa").GetComponent<Adventurer>();
    }

    public override void Upgrade1()
    {
        new_health = adventurer.GetHealthMax() + health_upgrade;
        new_shield = adventurer.GetShieldMax() + shield_upgrade;

        adventurer.SetHealth(new_health);
        adventurer.SetShield(new_shield);
    }

    public override void Upgrade2()
    {
        new_base_damage = adventurer.GetDamageBasicAttack() + base_damage_upgrade;
        new_skill_damage = adventurer.GetDamageSkill() - skill_damage_upgrade;

        adventurer.SetDamageBasicAttack(new_base_damage);
        adventurer.SetDamageSkill(new_skill_damage);
    }

    public override void Upgrade3()
    {
        new_ultimate_time = (int)adventurer.GetUltimateCD() - ultimate_time_upgrade;

        adventurer.SetUltimateCD(new_ultimate_time);
    }
}