using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipperStats : Stats {

    PlayerMovement playerMove;
    Transform playerGraphic;
    Map map;

    Vector2 oldPos;

    const float timeAnimation = 0.5f;
    float timeToGoBack;
    bool isMovingGraph = false;

    int graphicMove;
    int dirSkillArea;
    const int skillArea = 3;


    // Use this for initialization
    void Start () {
        health = 100;
        shield = 20;
        damageBasicAttack = 5;
        damageSkill = 20;
        damageUltimate = 50;
        fireRate = 0.2f;
        recoveryShieldTime = 2;

        StartCoroutine(ShieldRecovery());


        playerMove = GetComponent<PlayerMovement>();
        playerGraphic = GameObject.Find("Player1/GraficCharacter").GetComponent<Transform>();
        //playerGraphic = playerGraphic.transform.Find

        
        map = GameObject.Find("Map").GetComponent<Map>();
        timeToGoBack = 0f;


        if (whichIsThisPlayer == 0)
        {
            graphicMove = -4;
            dirSkillArea = -1;
        }
        else
        {
            graphicMove = 4;
            dirSkillArea = 1;
        }


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MoveToXPos();

    }

    public override void SkillMoveTo()
    {
        Map map = GameObject.Find("Map").GetComponent<Map>();
        graphicMove = -4;

        if (transform.rotation.y > 0)
            graphicMove = 4;

        isMovingGraph = true;

        if (!playerMove.GetIsMoving())
        {
            Debug.Log("HEREEEEEEE --> 2 : " + whichIsThisPlayer);
            playerMove.enabled = false;
            Vector2 moveTo = new Vector2(map.blocks[(playerMove.playerColumn + graphicMove), playerMove.playerRow].transform.position.x, playerGraphic.transform.position.y);
            Debug.Log("HEREEEEEEE --> 3 --> " + moveTo);
            playerGraphic.transform.position = moveTo;
        }
        //playerGraphic.position = Vector2.MoveTowards(transform.position, moveTo, 1);
        //playerGraphic.transform.position = map.blocks[(playerMove.playerColumn - graphicMove), playerMove.playerRow].transform.position;
        //playerGraphic.transform.position = new Vector2(playerGraphic.transform.position.x, playerGraphic.transform.position.y + 0.096f);
        //playerGraphic.Translate(moveTo); 
    }

    private void MoveToXPos()
    {
        if (isMovingGraph)
        {
            timeToGoBack += Time.deltaTime;
            if (timeToGoBack >= timeAnimation)
            {
                // Esta funcion se ejecuta en el update y por eso el oldpos se guarda la y del ultimo
                // momento en que nos estabamos moviendo a tiempo real.
                oldPos = new Vector2(transform.position.x, playerGraphic.transform.position.y);
                playerGraphic.transform.position = oldPos;
                isMovingGraph = false;
                timeToGoBack = 0f;
                playerMove.enabled = true;
            }
        }
    }
}
