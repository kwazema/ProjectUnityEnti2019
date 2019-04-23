using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleChoose : MonoBehaviour {
    public Text textPlayer;
    public GameManager gameManager;
    public int players;

    private int numSelected = 0;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Use this for initialization
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
            SceneManager.LoadScene("BattleScene");
        }
        numSelected++;
    }

    public void PlayAI()
    {
        SceneManager.LoadScene("SceneAI");
    }


    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setSkull()
    {
        gameManager.playerChoise[numSelected] = 0;
        Debug.Log("Eres el portador de la muerte");
    }

    public void setScepter()
    {
        gameManager.playerChoise[numSelected] = 1;
        Debug.Log("La prueba va OK");
    }
}
