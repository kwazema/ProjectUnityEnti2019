using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public PlayerManager[] playerManager;
    public GameObject[] objectPlayer;
    public Sprite[] logoPlayer;

    public static GameManager instance = null;
    public int[] playerChoise;
    public GameObject pause_menu;

    string filePath;
    string jsonString;

    // NOW: Añadir el systema de bloques con vida y los bloques rotos.
    // NOW: Y cargar los datos del fichero en el Awake del PlayerManager.

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        filePath = Application.dataPath + "/GameData/FileCharactersData.json";
    }

    public ListCharacters LoadFileToString()
    {
        // Parseamos el fichero a un string
        string jsonString = File.ReadAllText(filePath);

        // Parseamos el string a la clase
        ListCharacters listCharacters = JsonUtility.FromJson<ListCharacters>(jsonString);

        return listCharacters;
    }

    public void SaveStringToFile(ListCharacters listCharacters)
    {
        // Parseamos la clase a un fichero string
        string jsonString = JsonUtility.ToJson(listCharacters);

        // Parseamos el string a json
        File.WriteAllText(filePath, jsonString);
    }

    #region Como utilizar funciones FILE GameManager

    void ExampleUsingFile()
    {
        ListCharacters listCharacters = LoadFileToString();

        // Get varible
        int damage = listCharacters.characterStats[0].damageBasicAttack;

        // Set varible
        listCharacters.characterStats[0].damageBasicAttack = damage;
    }

    #endregion

    public void InitPlayers()
    {
        playerManager = new PlayerManager[2];

        for (int i = 0; i < playerManager.Length; i++)
        {
           // Debug.Log("Player " + i + " " + playerChoise[i]);

            switch (playerChoise[i])
            {
                case 0: playerManager[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<Santa>();  break;
                case 1: playerManager[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<Minos>(); break;
                case 2: playerManager[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<Polyphemus>(); break;
                case 3: playerManager[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<Adventurer>(); break;
            }

            playerManager[i].name = (playerManager[i].namePlayer + (i + 1));
            playerManager[i].whichIsThisPlayer = i;

            GameObject.Find(playerManager[i].name).GetComponent<PlayerMovement>().whichIs = i;
            
            // Class Manager
            playerManager[i].thisPlayerIs = (PlayerManager.ThisPlayerIs)i;

            if (playerManager[i].thisPlayerIs == PlayerManager.ThisPlayerIs.Player1)
            {
                playerManager[0].transform.rotation = new Quaternion(0, 0, 0, 0);
                playerManager[0].gameObject.layer = 11;
            }
            else
            {
                playerManager[1].transform.rotation = new Quaternion(0, 180, 0, 0);
                playerManager[1].gameObject.layer = 12;
            }


            // Class Input Attack
            GameObject.Find(playerManager[i].name).GetComponent<PlayerAttackInput>().enumPlayer = (PlayerAttackInput.EnumPlayer)i;

            // Class Input Movement
            GameObject.Find(playerManager[i].name).GetComponent<PlayerInput>().enumPlayer = (PlayerInput.EnumPlayer)i;
        }
    }

    void Fade()
    {
        FindObjectOfType<FadeImage>().FadeToBlack();
    }


}
