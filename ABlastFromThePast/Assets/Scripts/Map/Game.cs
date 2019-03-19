using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public int[] numPlayer; //int para de la sección de los personajes
    public Stats[] playerStats;
    GameObject[] GameObjectsPlayers;

    private void Awake()
    {
        numPlayer = new int[2];
        numPlayer[0] = 1;
        numPlayer[1] = 2;

        playerStats = new Stats[2]; 
        InitPlayers();
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            //Debug.Log("Player" + (i + 1) + " Damage: " + playerStats[i].GetDamageBasicAttack());
            //Debug.Log("Player" + (i + 1) + " Health: " + playerStats[i].GetHealth());
        }
    }

    //void InitColliders()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        GameObjectsPlayers[i] = GameObject.Find("Player" + (i + 1));
    //        Debug.Log("Player" + (i + 1));
    //    }
    //}
	
    void InitPlayers()
    {
        for (int i = 0; i < 2; i++)
        {
            //playerStats[i].enumPlayer = (Stats.EnumPlayer)i;
            switch (numPlayer[i])
            {
                case 0:
                    //playerStats[i] = GameObject.Find("Player" + (i + 1)).GetComponent<NewPlayer>();
                    //player[i] = new NameStats();
                    break;

                case 1:
                    playerStats[i] = GameObject.Find("Player" + (i + 1)).GetComponent<SwipperStats>();
                    //playerStats[i] = new SwipperStats();
                    break;

                case 2:
                    playerStats[i] = GameObject.Find("Player" + (i + 1)).GetComponent<BrayanStats>();
                    //playerStats[i] = new BrayanStats();
                    //playerStats[i].idPlayer = i;
                    break;

                case 3:
                    //player[i] = new NameStats();
                    break;
            }
            /* --- Asignamos valores enum(Player) para vada Script --- */
            #region Assign Num Player
            playerStats[i].whichIsThisPlayer = i;
            GameObject.Find("Player" + (i + 1)).GetComponent<PlayerMovement>().whichIs = i;
            
            // Class Stats
            playerStats[i].enumPlayer = (Stats.EnumPlayer)i; 

            // Class Input Attack
            GameObject.Find("Player" + (i + 1)).GetComponent<PlayerAttackInput>().enumPlayer = (PlayerAttackInput.EnumPlayer)i;

            // Class Input Movement
            GameObject.Find("Player" + (i + 1)).GetComponent<PlayerInput>().enumPlayer = (PlayerInput.EnumPlayer)i;
            #endregion
        }
    }
}
