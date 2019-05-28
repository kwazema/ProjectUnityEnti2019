using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using XInputDotNetPure; // Required in C#


public class MainMenu : MonoBehaviour {

    private EventSystem eventSystem;

    private string lastSelectect;

    private GamePadState state;
    private GamePadState prevState;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Start()
    {

    }

    public void Vibration()
    {
        StartCoroutine(ControllerManager.ControllerVibrationSpam(0, 0.8f, 0.8f, 0.15f, 0.1f, 6));
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

                if (Input.GetAxisRaw("HorizontalP1") != 0)
                    StartCoroutine(ControllerManager.ControllerVibration(0, 0.1f, 0.1f, 0.1f));
                else if (Input.GetAxisRaw("HorizontalP2") != 0)
                    StartCoroutine(ControllerManager.ControllerVibration(1, 0.1f, 0.1f, 0.1f));
                else if (Input.GetAxisRaw("VerticalP1") != 0)
                    StartCoroutine(ControllerManager.ControllerVibration(0, 0.1f, 0f, 0.1f));
                else if (Input.GetAxisRaw("VerticalP2") != 0)
                    StartCoroutine(ControllerManager.ControllerVibration(1, 0.1f, 0.1f, 0.1f));
            }
        }

        //Si usas las teclas y no tienes los focus te hace auto focus al ultimo boton 
        if (
            Input.GetButtonDown("HorizontalP1") || Input.GetButtonDown("VerticalP1") || 
            Input.GetButtonDown("HorizontalP2") || Input.GetButtonDown("VerticalP2") || 
            Input.GetAxisRaw("Joy0Y") != 0 || Input.GetAxisRaw("Joy0X") != 0 || 
            Input.GetAxisRaw("Joy1Y") != 0 || Input.GetAxisRaw("Joy1X") != 0
            )
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

        prevState = state;
        state = GamePad.GetState((PlayerIndex)0);

        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            StartCoroutine(ControllerManager.ControllerVibration(0, 0.4f, 0.4f, 0.15f));
        }
    }

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

    public void Stadistics()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("LibraryButtonSanta")); // Selecciona un nuevo boton
    }

    public void BackToQuit()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("QuitButton")); // Selecciona un nuevo boton
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
