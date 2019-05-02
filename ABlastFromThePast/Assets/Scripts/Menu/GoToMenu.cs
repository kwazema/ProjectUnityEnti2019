using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoToMenu : MonoBehaviour {

    public Canvas myCanvas;
    bool active = false;

	public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && active == false)
        {
            myCanvas.enabled = true;
            active = true;
        } else if (Input.GetKey(KeyCode.Escape) && active == true)
        {
            myCanvas.enabled = false;
            active = false;
        }
    }
}
