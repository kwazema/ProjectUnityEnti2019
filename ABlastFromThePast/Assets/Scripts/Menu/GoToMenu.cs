using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class GoToMenu : MonoBehaviour {

    public GameObject GO_Canvas;
    public Canvas myCanvas;
    bool active = false;
    private FadeImage fade;
    public EventSystem eventSystem;
    private void Awake()
    {
       // fade = FindObjectOfType<FadeImage>();
    }

    private void Start()
    {
    }

    public void GoBack()
    {
        Time.timeScale = 1;

        fade = FindObjectOfType<FadeImage>();
        fade.FadeToBlack();
        Invoke("LoadSceneMenu", 2);
    }

    private void LoadSceneMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
        
    private void Update()
    {



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
                ClosePauseMenu();

            //if (Time.timeScale == 0)
            //    OpenPauseMenu();
        }


        //// Desactivar el menu de pause 
        //if (Input.GetKeyDown(KeyCode.Escape) && myCanvas.enabled)
        //{
        //    //myCanvas.enabled = false;
        //    Time.timeScale = 1;
        //    GO_Canvas.SetActive(false);
        //    //active = false;
        //}
    }

    private void OpenPauseMenu() {
        Time.timeScale = 0;
        GO_Canvas.SetActive(true);
    }

    private void ClosePauseMenu() {
        Time.timeScale = 1;
        GO_Canvas.SetActive(false);
    }

}
