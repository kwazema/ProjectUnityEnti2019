using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthStarStats : Stats {

    // Use this for initialization
    protected override void Start () {
        base.Start();

        #region Basic Stats

        health = 100;
        shield = 0;

        damageBasicAttack = 2;
        damageSkill = 25;
        damageUltimate = 50;

        #endregion

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
