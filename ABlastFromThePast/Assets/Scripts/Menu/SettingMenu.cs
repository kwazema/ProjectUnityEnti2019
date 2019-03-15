using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour {

    public GameObject gameObject;

    enum language
    {
        catalan,
        spanish, 
        english
    };

    language idiom;

    public AudioMixer audioMixer;
  
    public void Volume ( float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetLanguageCatalan()
    {        
        idiom = language.catalan;
    }

    public void SetLanguageSpanish()
    {
        idiom = language.spanish;
    }

    public void SetLanguageEnglish()
    {
        idiom = language.english;
    }

    public void ShowContent()
    {
        GameObject.Find("OptionsMenuCat").SetActive(false);
        GameObject.Find("OptionsMenuEsp").SetActive(false);
        GameObject.Find("OptionsMenuEng").SetActive(false);
        switch (idiom)
        {
            case language.catalan:
                GameObject.Find("OptionsMenuCat").SetActive(true);
                Debug.Log("catalan");
                break;
            case language.spanish:
                GameObject.Find("OptionsMenuEsp").SetActive(true);
                Debug.Log("spanish");
                break;     
            case language.english:
                GameObject.Find("OptionsMenuEng").SetActive(true);
                Debug.Log("english");
                break;
        }
    }
}
