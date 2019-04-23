using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScepterStats : PlayerManager {

    public Transform distance_attack;
    int pos_column;




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

        fireRate = 0.1f;
        recoveryShieldTime = 2;
        StartCoroutine(ShieldRecovery());

        SelectedZonaPlayer();
        
        distance_attack.position = map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (moveToPosition)
            MovingToPosition(65f);


        if (!returnOldPosition) {
            for (int i = -1; i < 2; i++)
            { // horizontal
                for (int j = -1; j < 2; j++)
                { // vertical

                    if (
                        (pos_column + i) >= 0 &&
                        (pos_column + i) < map.columnLenth &&
                        (playerMovement.playerRow + j) >= 0 &&
                        (playerMovement.playerRow + j) < map.rowLenth
                      )
                    {
                        map.ColorBlocks((pos_column + i), (playerMovement.playerRow + j), Color.white);
                    }
                }
            }
        }


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

        pos_column = game_manager.playerStats[player_to_attack].playerMovement.playerColumn;
        moveToBlock = new Vector2(map.blocks[pos_column, playerMovement.playerRow].transform.position.x, transform.position.y);

        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
        }
    }

    protected override void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0)
    {
        for (int i = -1; i < 2; i++) { // horizontal
            for (int j = -1; j < 2; j++) { // vertical

                if (
                    (pos_column + i) >= 0 &&
                    (pos_column + i) < map.columnLenth &&
                    (playerMovement.playerRow + j) >= 0 &&
                    (playerMovement.playerRow + j) < map.rowLenth 
                  )
                {
                    map.ColorBlocks((pos_column + i), (playerMovement.playerRow + j), Color.red);

                    if (map.blocks[(pos_column + i), (playerMovement.playerRow + j)].GetPlayerStatsBlock((int)thisPlayerIs) != null)
                    {
                        map.blocks[(pos_column + i), (playerMovement.playerRow + j)].GetPlayerStatsBlock((int)thisPlayerIs).TakeDamage(GetDamageSkill());
                    }
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