using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoToMenu : MonoBehaviour {

    public GameObject GO_Canvas;
    public Canvas myCanvas;
    bool active = false;

	public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            // if (Time.timeScale == 1)
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
