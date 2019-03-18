using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipperStats : Stats {

	// Use this for initialization
	void Start () {
        health = 120;
        shield = 5;
        damageBasicAttack = 20;
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
