using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipperStats : Stats {

    // Use this for initialization
    protected override void Start () {
        base.Start();

        health = 100;
        shield = 20;
        damageBasicAttack = 3;
        damageSkill = 10;
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
