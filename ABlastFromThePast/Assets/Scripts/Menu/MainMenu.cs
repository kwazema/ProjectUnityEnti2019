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

    //eventSystem.firstSelectedGameObject = GameObject.Find("ChoosePlayerEng/Brayan");
    public void Play() 
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("MargenPersonaje(Clone)")); // Selecciona un nuevo boton
    }

    public void Library()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("LibraryButtonSanta")); // Selecciona un nuevo boton
    }

    public void BackToPlay()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("PlayButton")); // Selecciona un nuevo boton
    }

    public void BackToLibrary()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("LibraryButtonSanta")); // Selecciona un nuevo boton
    }

    public void Stats()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("Button")); // Selecciona un nuevo boton
    }


    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
