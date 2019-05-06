using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour {

    private EventSystem eventSystem;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void PlayEng() 
    {
        //eventSystem.firstSelectedGameObject = GameObject.Find("ChoosePlayerEng/Brayan");
        eventSystem.SetSelectedGameObject(GameObject.Find("ChoosePlayerEng/Brayan")); // Selecciona un nuevo boton
    }

    public void PlayEsp() 
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("ChoosePlayerEsp/Brayan")); // Selecciona un nuevo boton
    }

    public void PlayCat()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("ChoosePlayerCat/Brayan")); // Selecciona un nuevo boton
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
