using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleChoose : MonoBehaviour {
    public Text textPlayer;

    bool foxy;
    bool skull;
    bool mage;

    public static int[] charactersChoice;
    public static string[] namePlayer = new string[2];

    private void Awake()
    {
        charactersChoice = new int[2];
        namePlayer[0] = "NorthStart";
        namePlayer[1] = "BrayanAI";
        charactersChoice[0] = 2;
        charactersChoice[1] = 3;
    }
    // Use this for initialization
    void Start () {
        foxy = false;
        skull= false;
        mage = false;

        textPlayer.text = "Choose for Player " + (selectedPLayers+1);

    }

    // Update is called once per frame
    void Update () {
		
	}

    int selectedPLayers = 0;

    public void Play()
    {
        if (namePlayer[0] == namePlayer[1])
        {
            namePlayer[1] += "1";
        }

        if (selectedPLayers == 0)
        {
            textPlayer.text = "Choose for Player " + (selectedPLayers + 2);
        }
        
        if (selectedPLayers == 1)
        {
            SceneManager.LoadScene("BattleScene");
        }
        selectedPLayers++;
    }

    public void PlayAI()
    {
        SceneManager.LoadScene("SceneAI");
    }


    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setFoxy()
    {
        foxy = true;
        Debug.Log("Swiper no robes");
        charactersChoice[selectedPLayers] = 1;
        namePlayer[selectedPLayers] = "Swipper";

        Debug.Log(charactersChoice[selectedPLayers]);
    }

    public void setSkull()
    {
        skull = true;
        Debug.Log("Eres el portador de la muerte");
        charactersChoice[selectedPLayers] = 0;
        namePlayer[selectedPLayers] = "Brayan";

        Debug.Log(charactersChoice[selectedPLayers]);
    }

    public void setMage()
    {
        mage = true;
        Debug.Log("Freeze you fool");
        charactersChoice[selectedPLayers] = 2;
        namePlayer[selectedPLayers] = "NorthStart";

        Debug.Log(charactersChoice[selectedPLayers]);
    }
}
