using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void Play() 
    {
        SceneManager.LoadScene("Modojuego");
    }

    public void Practice()
    {
        Debug.Log("Practica moffo!");
        //SceneManager.LoadScene("SampleScene");
    }

    public void Online()
    {
        Debug.Log("About to get rekt");
        //SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
