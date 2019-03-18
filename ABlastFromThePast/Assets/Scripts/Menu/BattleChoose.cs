using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleChoose : MonoBehaviour {

    bool foxy;
    bool skull;

    static int charactersChoice;

    // Use this for initialization
    void Start () {
        foxy = false;
        skull= false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene("MovementSoncan");
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setFoxy()
    {
        foxy = true;
        Debug.Log("Swiper no robes");
        charactersChoice = 2;
        Debug.Log(charactersChoice);
    }

    public void setSkull()
    {
        skull = true;
        Debug.Log("Eres el portador de la muerte");
        charactersChoice = 1;
        Debug.Log(charactersChoice);
    }
}
