using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleChoose : MonoBehaviour
{
    public Text textPlayer;
    public GameManager gameManager;
    public int players;
    private FadeImage fade;

    public GameObject p1Skull;
    public GameObject p2Skull;
    public GameObject p1Scepter;
    public GameObject p2Scepter;

    private int numSelected = 0;

    public GameObject[] test;
    public GameObject prefab;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        fade = FindObjectOfType<FadeImage>();
        test = InstantiateBlocks(5, new Vector2Int(-2, -2), 2, 1, 1);
    }

    void Start () {
        gameManager.playerChoise = new int[players];
        textPlayer.text = "Choose for Player " + (numSelected+1);
    }

    public void Play()
    {
        if (numSelected == 0)
        {
            textPlayer.text = "Choose for Player " + (numSelected + 2);
        }
        
        if (numSelected == 1)
        {
            Invoke("FadeToImage", 1);
            Invoke("LoadSceneBattleScene", 3);
        }
        numSelected++;
    }

    void FadeToImage()
    {
        fade.FadeToBlack();
    }

    void LoadSceneBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }

    //public void PlayAI()
    //{
    //    SceneManager.LoadScene("SceneAI");
    //}

    //public void Return()
    //{
    //    SceneManager.LoadScene("Menu");
    //}

    public void setSkull()
    {
        gameManager.playerChoise[numSelected] = 0;
        //if (numSelected == 0)
        //{
        //    p1Skull.SetActive(true);
        //}

        //if (numSelected == 1)
        //{
        //    p2Skull.SetActive(true);
        //}
    }

    public void setScepter()
    {
        gameManager.playerChoise[numSelected] = 1;
        //if (numSelected == 0)
        //{
        //    p1Scepter.SetActive(true);
        //}

        //if (numSelected == 1)
        //{
        //    p2Scepter.SetActive(true);
        //}
    }

    GameObject[] InstantiateBlocks(int column, Vector2Int offset, float width, float height, float margin)
    {
        GameObject[] test = new GameObject[5];

        for (int i = 0; i < column; i++) // Horizontal
        {
            test[i] = Instantiate(prefab);
            test[i].transform.SetParent(GameObject.Find("Canvas_16_9").transform);
            test[i].transform.position = offset + new Vector2(i * (width + margin), 0 * -(height + margin));
        }
        return test;
    }

}
