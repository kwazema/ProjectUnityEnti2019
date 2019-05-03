using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

   // private FadeImage fade;

    private void Awake()
    {
        //fade = FindObjectOfType<FadeImage>();
    }

    public void Play() 
    {
        //fade.FadeToBlack();
        //Invoke("LoadSceneModoJuego", 4); // Tambien se puede usar un star corountine si se hicieran muchas llamanadas
        //StartCoroutine(Test());
    }

    //private void LoadSceneModoJuego()
    //{
    //    SceneManager.LoadScene("Modojuego");
    //}

    //IEnumerator Test()
    //{

    //    yield return new WaitForSeconds(3f);
    //    SceneManager.LoadScene("Modojuego");

    //}

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
