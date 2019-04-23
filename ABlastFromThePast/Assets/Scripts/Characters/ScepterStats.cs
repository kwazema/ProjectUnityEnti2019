using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScepterStats : PlayerManager {

    public Transform distance_attack;
    int pos_x;




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

    protected override void Update()
    {
        base.Update();
        if (moveToPosition)
            MovingToPosition(95f, 2);
    }

    public override void SkillMoveTo(float cooldown = 0, float timeToRetorn = 0)
    {
        int player_to_attack;
        GameManager game_manager = FindObjectOfType<GameManager>();

        if (thisPlayerIs == ThisPlayerIs.Player1)
            player_to_attack = 1;
        else
            player_to_attack = 0;
        
        oldPos = (Vector2)transform.position;

        pos_x = game_manager.playerStats[player_to_attack].playerMovement.playerColumn;
        moveToBlock = new Vector2(map.blocks[pos_x, playerMovement.playerRow].transform.position.x, transform.position.y);

        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
        }
    }

    protected override void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0)
    {
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if (
                    (playerMovement.playerColumn + i) >= 0 &&
                    (playerMovement.playerColumn + i) < map.columnLenth &&
                    (playerMovement.playerRow + j) >= 0 &&
                    (playerMovement.playerRow + j) < map.rowLenth
                  )
                {
                    map.ColorBlocks((pos_x + i), (playerMovement.playerRow + j), Color.red);
                }
            }
        }
    }


    protected override void SelectedZonaPlayer()
    {
        if (whichIsThisPlayer == 0)
        {
            graphicMove = 1;
            dirSkillZone = 1;
        }
        else
        {
            graphicMove = -1;
            dirSkillZone = -1;
        }
    }
}