using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrayanStats : Stats {


    // Use this for initialization
    protected override void Start () {
        base.Start();

        health = 100;
        shield = 20;
        damageBasicAttack = 5;
        damageSkill = 20;
        damageUltimate = 50;
        fireRate = 0.2f;
        recoveryShieldTime = 2;

        StartCoroutine(ShieldRecovery());

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }
}
