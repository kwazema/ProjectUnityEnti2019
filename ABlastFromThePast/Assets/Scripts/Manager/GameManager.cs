using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerManager[] playerStats;
    public GameObject[] objectPlayer;
    public static GameManager instance = null;

    [SerializeField]
    public PlayerManager[] playerManager;

    public int[] playerChoise;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    //void Start () { }

    //void Update() { }

    public void InitPlayers()
    {
        playerStats = new PlayerManager[2];

        for (int i = 0; i < playerStats.Length; i++)
        {
            Debug.Log("Player " + i + " " + playerChoise[i]);

            switch (playerChoise[i])
            {
                case 0: playerStats[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<BrayanStats>();  break;
                case 1: playerStats[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<ScepterStats>(); break;
            }
            playerStats[i].name = (playerStats[i].namePlayer + (i + 1));


            playerStats[i].whichIsThisPlayer = i;

            if (playerStats[i].whichIsThisPlayer == 0)
                playerStats[0].transform.rotation = new Quaternion(0, 0, 0, 0);
            else
                playerStats[1].transform.rotation = new Quaternion(0, 180, 0, 0);

            GameObject.Find(playerStats[i].name).GetComponent<PlayerMovement>().whichIs = i;
            
            // Class Stats
            playerStats[i].thisPlayerIs = (PlayerManager.ThisPlayerIs)i; 

            // Class Input Attack
            GameObject.Find(playerStats[i].name).GetComponent<PlayerAttackInput>().enumPlayer = (PlayerAttackInput.EnumPlayer)i;

            // Class Input Movement
            GameObject.Find(playerStats[i].name).GetComponent<PlayerInput>().enumPlayer = (PlayerInput.EnumPlayer)i;

        }
    }
}
