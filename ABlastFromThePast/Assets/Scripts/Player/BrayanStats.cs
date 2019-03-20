using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrayanStats : Stats {


    // Use this for initialization
    protected override void Start () {
        base.Start();

        health = 100;
        shield = 10;
        damageBasicAttack = 8;
        damageSkill = 12;
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
