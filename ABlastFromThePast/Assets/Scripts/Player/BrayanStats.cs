using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrayanStats : Stats {

    PlayerMovement playerMove;
    Transform playerGraphic;
    Map map;

    Vector2 oldPos;

    const float timeAnimation = 0.5f;
    float timeToGoBack;
    bool isMovingGraph = false;

    int graphicMove = -4;
    int skillArea = -3;

    // Use this for initialization
    void Start () {
        health = 100;
        shield = 2;
        damageBasicAttack = 1;
        damageSkill = 20;
        damageUltimate = 50;
        fireRate = 0.2f;
        recoveryShieldTime = 2;

        StartCoroutine(ShieldRecovery());


        playerMove = GetComponent<PlayerMovement>();
        playerGraphic = GameObject.Find("GraficCharacter").GetComponent<Transform>();
        map = GameObject.Find("Map").GetComponent<Map>();
        timeToGoBack = 0f;

        if (transform.rotation.y > 0)
            graphicMove = 4; skillArea = 3;

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
 

        isMovingGraph = true;

        if (!playerMove.GetIsMoving())
        {
            playerMove.enabled = false;

            Vector2 moveTo = new Vector2(map.blocks[(playerMove.playerColumn - graphicMove), playerMove.playerRow].transform.position.x * transform.rotation.y, playerGraphic.transform.position.y);
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

    public override void LookForwardBlocks(/*EnumPlayer player*/)
    {
        Game game = GameObject.Find("Map").GetComponent<Game>();
        PlayerInput playerInput = GetComponent<PlayerInput>();
        Stats[] player;
        player = new Stats[2];
        player[0] = game.playerStats[0];
        player[1] = game.playerStats[1];
        
        for (int i = 0; i < skillArea; i++)
        {
            if (map.blocks[(playerMove.playerColumn - graphicMove) - i, playerMove.playerRow] != null)
            {
                //if (map.blocks[(playerMove.playerColumn - graphicMove) - i, playerMove.playerRow] == map.blocks[playerMove.playerColumn , playerMove.playerRow] && transform.rotation.y > 0)
                if (map.blocks[(playerMove.playerColumn - graphicMove) - i, playerMove.playerRow].transform.position == player[0].transform.position)
                 Debug.Log("HEREEEEEEEEEEEEEEEEEEEEEEEEE::" + map.blocks[(playerMove.playerColumn - graphicMove) - i, playerMove.playerRow].transform.position);
            }

            //if (map.blocks[(playerMove.playerColumn - graphicMove)-skillArea , playerMove.playerRow] == map.blocks[playerMove.playerColumn, playerMove.playerRow])
            //{
            //    Debug.Log("HEREEEEEEEEEEEEEEEEEEEEEEEEE::");
            //}
            
        }

        


    }

}
