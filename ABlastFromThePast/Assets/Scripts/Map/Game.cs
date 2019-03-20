using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public int[] numPlayer; //int para de la sección de los personajes
    public Stats[] playerStats;
    public GameObject[] GameObjectsPlayers;
    //BattleChoose battleInfo;

    private void Awake()
    {
        numPlayer = new int[2];
        numPlayer[0] = 1;
        numPlayer[1] = 2;

        playerStats = new Stats[2]; 
        InitPlayers();
        //battleInfo 
    }

    // Use this for initialization
    void Start () {
        //Debug.Log("Personaje: " + BattleChoose.charactersChoice[0]);
        //Debug.Log("Personaje: " + BattleChoose.charactersChoice[1]);
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
            //switch (numPlayer[i])

            //GameObjectsPlayers[i].name = BattleChoose.namePlayer[i];
            Debug.Log("Player " + i + " " + BattleChoose.charactersChoice[i]);

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

            //playerStats[i].name = BattleChoose.namePlayer[i];

            /* --- Asignamos valores enum(Player) para vada Script --- */
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
