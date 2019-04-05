using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorthStartStats : Stats {

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

        SelectedZonaPlayer();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (moveToPosition)
        {
            MovingToPosition(75f, 3);
        }

    }

    public override void SkillMoveTo(float cooldown = 0, float timeToRetorn = 0) 
    {
        oldPos = (Vector2) transform.position;
        moveToBlock = new Vector2(map.blocks[playerMovement.playerColumn + graphicMove, playerMovement.playerRow].transform.position.x, transform.position.y);

        if (!playerMovement.GetIsMoving())
        {
            playerInput.enabled = false;
            moveToPosition = true;
        }
    }

    //private void MovingToPosition(float velocity)
    //{
    //    float step = velocity * Time.deltaTime;

    //    if ((Vector2)transform.position == moveToBlock)
    //    {
    //        // Collider
    //        bodyCollider.enabled = true;
    //        returnOldPosition = true;

    //        if (noHaAtacado)
    //        {
    //            LookForwardBlocks(3);

    //            noHaAtacado = false;
    //        }
    //    }

    //    if (returnOldPosition)
    //    {
    //        if (Time.time > timeInPosition)
    //        {
    //            transform.position = Vector2.MoveTowards(transform.position, oldPos, step);
    //            //Collider 
    //            bodyCollider.enabled = false;

    //            if ((Vector2)transform.position == oldPos)
    //            {
    //                moveToPosition = false;
    //                returnOldPosition = false;
    //                noHaAtacado = true;
    //                playerInput.enabled = true;
    //                playerMovement.enabled = true;

    //                //Collider 
    //                bodyCollider.enabled = true;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, moveToBlock, step);
    //        //Collider 
    //        bodyCollider.enabled = false;

    //        playerMovement.enabled = false;

    //        timeInPosition = Time.time;
    //        timeInPosition += 0.3f;
    //    }
    //}

    public override void LookForwardBlocks(int rangeEffectColumn, int rangeEfectRow = 0)
    {
        for (int i = 0; i < rangeEffectColumn; i++)
        {
            if (
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone)) < map.columnLenth &&
                ((playerMovement.playerColumn + graphicMove) + (i * dirSkillZone)) >= 0
                )
            {
                map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].spriteBlock.color = Color.red;

                if (map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].IsPlayerInThisBlock())
                {

                    map.blocks[(playerMovement.playerColumn + graphicMove) + (i * dirSkillZone), playerMovement.playerRow].GetPlayerStatsBlock().TakeDamage(GetDamageSkill());

                }
            }
        }
    }

    private void SelectedZonaPlayer()
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
        Debug.Log("Grafic: " + graphicMove);
    }

}
