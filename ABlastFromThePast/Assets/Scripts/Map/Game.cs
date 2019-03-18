using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    int[] numPlayer;
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
            Debug.Log("Player" + (i + 1) + " Damage: " + playerStats[i].GetDamageBasicAttack());
            Debug.Log("Player" + (i + 1) + " Health: " + playerStats[i].GetHealth());
        }
    }

    void InitColliders()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObjectsPlayers[i] = GameObject.Find("Player" + (i + 1));
            Debug.Log("Player" + (i + 1));
        }
    }
	
    void InitPlayers()
    {
        for (int i = 0; i < 2; i++)
        {

            switch (numPlayer[i])
            {
                case 0:
                    //player[i] = new NameStats();
                    break;

                case 1:
                    playerStats[i] = GameObject.Find("Player" + (i + 1)).GetComponent<SwipperStats>();
                    //playerStats[i] = new SwipperStats();
                    break;

                case 2:
                    playerStats[i] = GameObject.Find("Player" + (i + 1)).GetComponent<BrayanStats>();
                    //playerStats[i] = new BrayanStats();
                    break;

                case 3:
                    //player[i] = new NameStats();
                    break;
            }
        }
    }




}
