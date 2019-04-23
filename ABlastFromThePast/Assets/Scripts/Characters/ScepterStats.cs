using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScepterStats : PlayerManager {

    public Transform distance_attack;

    protected override void Awake()
    {
        base.Awake();
        //namePlayer = "Scepter"; // Nombre añadido desde el inspector
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        #region Basic Stats

        health = 100;
        shield = 10;

        damageBasicAttack = 8;
        damageSkill = 12;
        damageUltimate = 50;

        #endregion

        fireRate = 0.2f;
        recoveryShieldTime = 2;
        StartCoroutine(ShieldRecovery());

        SelectedZonaPlayer();
        distance_attack.position = map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position;
    }

    protected override void SelectedZonaPlayer()
    {
        if (whichIsThisPlayer == 0)
        {
            graphicMove = 5;
            dirSkillZone = 1;
        }
        else
        {
            graphicMove = -5;
            dirSkillZone = -1;
        }
    }
}