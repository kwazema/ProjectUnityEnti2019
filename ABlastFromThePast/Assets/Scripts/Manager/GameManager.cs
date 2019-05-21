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

    //string filePath;
    string filePathPersistent;
    string jsonString;

    string jsonData;
    //public TextAsset fileCharactersData;
    public bool build;


    public float volume = 1;
    public float volumeMusic = 1;
    public float volumeEffects = 1;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        //if (build)
        //   filePath = "./Mono/FileCharactersData.json";
        //else
        //   filePath = Application.dataPath + "/FileCharactersData.json";

        

        //TextAsset /*jsonData2*/ = (text)Resources.Load("FileCharactersData");


        //ListCharacters lc = LoadFileToString();

        //filePath = Path.Combine(Application.streamingAssetsPath, jsonString);
        //SaveStringToFile(lc);
    }

    public ListCharacters LoadFileToString()
    {

        //try
        //{
        //    filePathPersistent = Application.persistentDataPath + "/FileCharactersData.json";
        //}
        //catch (System.Exception)
        //{
        //    jsonData = Resources.Load<TextAsset>("FileCharactersData").text;
        //    //Debug.Log(")
        //    throw;
        //}

        filePathPersistent = Application.persistentDataPath + "/FileCharactersData.json";

        if (filePathPersistent != null)
        {
            //Parseamos el fichero a un string
            jsonData = File.ReadAllText(filePathPersistent);
        }
        else
        {
            jsonData = Resources.Load<TextAsset>("FileCharactersData").text;
        }

        // Parseamos el string a la clase
        ListCharacters listCharacters = JsonUtility.FromJson<ListCharacters>(jsonData);

        return listCharacters;
    }

    public void SaveStringToFile(ListCharacters listCharacters)
    {
        // Parseamos la clase a un fichero string
        string jsonString = JsonUtility.ToJson(listCharacters);

        // Parseamos el string a json
        File.WriteAllText(filePathPersistent, jsonString);
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
