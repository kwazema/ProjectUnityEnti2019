﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public int[] numPlayer; //int para de la sección de los personajes
    public Stats[] playerStats;
    public GameObject[] GameObjectsPlayers;

    private void Awake()
    {
        numPlayer = new int[2];
        numPlayer[0] = 1;
        numPlayer[1] = 2;

        playerStats = new Stats[2]; 

        InitPlayers();
    }

    //void Start () { }

    //void Update() { }

    void InitPlayers()
    {
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("Player " + i + " " + BattleChoose.charactersChoice[i]);
            //GameObjectsPlayers[i].name = BattleChoose.namePlayer[i];
            //Instantiate(GameObjectsPlayers[BattleChoose.charactersChoice[i]]);
            //GameObjectsPlayers[i].name = BattleChoose.namePlayer[i];

            switch (BattleChoose.charactersChoice[i])
            {
                case 0:
                    playerStats[i] = Instantiate(GameObjectsPlayers[BattleChoose.charactersChoice[i]]).GetComponent<BrayanStats>();
                    break;

                case 1:
                    playerStats[i] = Instantiate(GameObjectsPlayers[BattleChoose.charactersChoice[i]]).GetComponent<SwipperStats>();
                    break;

                case 2:
                    playerStats[i] = Instantiate(GameObjectsPlayers[BattleChoose.charactersChoice[i]]).GetComponent<NorthStarStats>();
                    break;
                case 3:
                    playerStats[i] = Instantiate(GameObjectsPlayers[BattleChoose.charactersChoice[i]]).GetComponent<BrayanStats>();
                    break;
            }
            playerStats[i].name = BattleChoose.namePlayer[i];

            #region Assign Num Player

            playerStats[i].whichIsThisPlayer = i;

            if (playerStats[i].whichIsThisPlayer == 0)
            {
                playerStats[0].transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                playerStats[1].transform.rotation = new Quaternion(0, 180, 0, 0);
            }

            GameObject.Find(BattleChoose.namePlayer[i]).GetComponent<PlayerMovement>().whichIs = i;
            
            // Class Stats
            playerStats[i].enumPlayer = (Stats.EnumPlayer)i; 

            // Class Input Attack
            GameObject.Find(BattleChoose.namePlayer[i]).GetComponent<PlayerAttackInput>().enumPlayer = (PlayerAttackInput.EnumPlayer)i;

            // Class Input Movement
            GameObject.Find(BattleChoose.namePlayer[i]).GetComponent<PlayerInput>().enumPlayer = (PlayerInput.EnumPlayer)i;

            #endregion
        }
    }
}