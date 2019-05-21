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


    public float volume;
    public float volumeMusic;
    public float volumeEffects;
    ListCharacters lc;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        filePathPersistent = Application.persistentDataPath + "/FileCharactersData.json";

        //if (build)
        //   filePath = "./Mono/FileCharactersData.json";
        //else
        //   filePath = Application.dataPath + "/FileCharactersData.json";



        //TextAsset /*jsonData2*/ = (text)Resources.Load("FileCharactersData");


        //lc = LoadFileToString();

        //filePath = Path.Combine(Application.streamingAssetsPath, jsonString);

       volume = 1;
       volumeMusic = 1;
       volumeEffects = 1;
    }

    private void Start()
    {

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

        if (File.Exists(filePathPersistent))
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

    IEnumerator SaveFile(string jsonString)
    {
        FileInfo fileInfo = new FileInfo(filePathPersistent);

        while (IsFileLocked(fileInfo))
        {
            Debug.Log("File Bloqued");
            yield return new WaitForSeconds(0.05f);
        }

        File.WriteAllText(filePathPersistent, jsonString);
    }

    bool IsFileLocked(FileInfo file)
    {
        FileStream stream = null;
        try
        {
            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
        }
        catch (IOException)
        {
            //the file is unavailable because it is:
            //still being written to
            //or being processed by another thread
            //or does not exist (has already been processed)
            return true;
        }
        finally
        {
            if (stream != null)
                stream.Close();
        }

        //file is not locked
        return false;
    }

    public void SaveStringToFile(ListCharacters listCharacters)
    {
        // Parseamos la clase a un fichero string
        string jsonString = JsonUtility.ToJson(listCharacters);

        if (File.Exists(filePathPersistent))
        {
            // Parseamos el string a json
            //File.WriteAllText(filePathPersistent, jsonString);
            File.WriteAllText(filePathPersistent, jsonString);
        }
        else
        {
            File.CreateText(filePathPersistent);
            StartCoroutine(SaveFile(jsonString));
        }
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
