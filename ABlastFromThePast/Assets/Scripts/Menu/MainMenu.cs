using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour {

    private EventSystem eventSystem;
    private string lastSelectect;

    private void Start()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Si mueves el raton pierdes el focus del boton
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0)
        {
            eventSystem.SetSelectedGameObject(null); // Desseleccionar boton

            if (Cursor.visible == false)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        //Muentras no sea null y se haya cambiado el boton te guarda el string
        if (eventSystem.currentSelectedGameObject != null)
        {
            if (lastSelectect != eventSystem.currentSelectedGameObject.name)
            {
                lastSelectect = eventSystem.currentSelectedGameObject.name;
            }
        }

        //dirHorizontal = Input.GetAxisRaw("Joy0X");

        //if (Input.GetKeyDown(KeyCode.A))
        //    dirHorizontal = -1f;

        //if (Input.GetKeyDown(KeyCode.D))
        //    dirHorizontal = 1f;

        ////--------------- Direction Vertical --------------- //
        //if (dirHorizontal == 0)
        //{
        //    dirVertical = Input.GetAxisRaw("Joy0Y");

        //    if (Input.GetKeyDown(KeyCode.W))
        //        dirVertical = 1f;

        //    if (Input.GetKeyDown(KeyCode.S))
        //        dirVertical = -1f;
        //}

        //Input.get

        if (Input.GetButtonDown("VerticalJ"))
        {
            Debug.Log("CLICK");

        }

        //Si usas las teclas y no tienes los focus te hace auto focus al ultimo boton 
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical") || Input.GetAxisRaw("Joy0Y") != 0 || Input.GetAxisRaw("Joy0X") != 0 || Input.GetAxisRaw("Joy1Y") != 0 || Input.GetAxisRaw("Joy1X") != 0)
        {
            if (Cursor.visible == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(GameObject.Find(lastSelectect)); // Selecciona un nuevo boton
            }
        }
    }

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    //eventSystem.firstSelectedGameObject = GameObject.Find("ChoosePlayerEng/Brayan");
    

    public void Play() 
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("MargenPersonaje(Clone)")); // Selecciona un nuevo boton
        //eventSystem.SetSelectedGameObject(null); // Desseleccionar boton
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
