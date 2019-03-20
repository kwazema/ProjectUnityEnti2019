using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStats : Stats
{
	// Use this for initialization
	void Start ()
    {
        damageBasicAttack = 30;
        damageSkill = 100;
        damageUltimate = 200;

        fireRate = 2.5f;
	}

    // Update is called once per frame
    protected override void Update () {
        base.Update();

    }
}
