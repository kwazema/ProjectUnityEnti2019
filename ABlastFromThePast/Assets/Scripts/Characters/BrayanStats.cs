using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrayanStats : PlayerManager {

    public Transform distance_attack;

    protected override void Awake()
    {
        base.Awake();
        //namePlayer = "Brayan"; // Nombre añadido desde el inspector
    }

    // Use this for initialization
    protected override void Start ()
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

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (moveToPosition)
            MovingToPosition(75f, 2);
    }

    public override void SkillMoveTo(float cooldown = 0, float timeToRetorn = 0)
    {
        oldPos = (Vector2)transform.position;
        moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, transform.position.y);

        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
        }
    }

    protected override void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0)
    {
        for (int i = 0; i < rangeEffectColumn; i++)
        {
            if (
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone)) < map.columnLenth &&
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone)) >= 0
                )
            {
                map.ColorBlocks((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow, Color.red);

                if (map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                {
                    map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                }
            }
        }
    }

    protected override void SelectedZonaPlayer()
    {
        if (whichIsThisPlayer == 0)
        {
            graphicMove = 4;
            dirSkillZone = 1;
        }
        else
        {
            graphicMove = -4;
            dirSkillZone = -1;
        }
    }
}
