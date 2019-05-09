using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public PlayerManager[] playerStats;
    public GameObject[] objectPlayer;
    public Sprite[] logoPlayer;

    public static GameManager instance = null;
    public int[] playerChoise;
    public GameObject pause_menu;

    string filePath;
    string jsonString;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        filePath = Application.dataPath + "/GameData/FileCharactersData.json";
    }

    public void InitPlayers()
    {
        playerStats = new PlayerManager[2];

        for (int i = 0; i < playerStats.Length; i++)
        {
            Debug.Log("Player " + i + " " + playerChoise[i]);

            switch (playerChoise[i])
            {
                case 0: playerStats[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<SantaStats>();  break;
                case 1: playerStats[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<ScepterStats>(); break;
            }
            playerStats[i].name = (playerStats[i].namePlayer + (i + 1));

            playerStats[i].whichIsThisPlayer = i;


            GameObject.Find(playerStats[i].name).GetComponent<PlayerMovement>().whichIs = i;
            
            // Class Manager
            playerStats[i].thisPlayerIs = (PlayerManager.ThisPlayerIs)i;

            if (playerStats[i].thisPlayerIs == PlayerManager.ThisPlayerIs.Player1)
            {
                playerStats[0].transform.rotation = new Quaternion(0, 0, 0, 0);
                playerStats[0].gameObject.layer = 11;
            }
            else
            {
                playerStats[1].transform.rotation = new Quaternion(0, 180, 0, 0);
                playerStats[1].gameObject.layer = 12;
            }


            // Class Input Attack
            GameObject.Find(playerStats[i].name).GetComponent<PlayerAttackInput>().enumPlayer = (PlayerAttackInput.EnumPlayer)i;

            // Class Input Movement
            GameObject.Find(playerStats[i].name).GetComponent<PlayerInput>().enumPlayer = (PlayerInput.EnumPlayer)i;
        }
    }

    void Fade()
    {
        FindObjectOfType<FadeImage>().FadeToBlack();
    }


}
