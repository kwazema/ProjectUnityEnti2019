using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour {

    //public Slider volumeBar;
    //public GameObject gameObject;

    //enum language
    //{
    //    catalan,
    //    spanish, 
    //    english
    //};

    //language idiom;
    public Slider volume;
    public Slider volumeMusic;
    public Slider volumeEffects;
    public AudioMixer audioMixer;


    private void Start()
    {
        volume.value = GameManager.instance.volume;
        volumeMusic.value = GameManager.instance.volumeMusic;
        volumeEffects.value = GameManager.instance.volumeEffects;
    }

    public void MasterVolume ( float volume)
    {
        audioMixer.SetFloat("volume", volume);
        GameManager.instance.volume = volume;
    }

    public void Music(float volume)
    {
        audioMixer.SetFloat("volumeMusic", volume);
        GameManager.instance.volumeMusic = volume;
    }

    public void Effects(float volume)
    {
        audioMixer.SetFloat("volumeEffects", volume);
        GameManager.instance.volumeEffects = volume;
    }

    public void Quality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    //public void SetLanguageCatalan()
    //{        
    //    idiom = language.catalan;
    //}

    //public void SetLanguageSpanish()
    //{
    //    idiom = language.spanish;
    //}

    //public void SetLanguageEnglish()
    //{
    //    idiom = language.english;
    //}

    //public void ShowContent()
    //{
    //    GameObject.Find("OptionsMenuCat").SetActive(false);
    //    GameObject.Find("OptionsMenuEsp").SetActive(false);
    //    GameObject.Find("OptionsMenuEng").SetActive(false);
    //    switch (idiom)
    //    {
    //        case language.catalan:
    //            GameObject.Find("OptionsMenuCat").SetActive(true);
    //            Debug.Log("catalan");
    //            break;
    //        case language.spanish:
    //            GameObject.Find("OptionsMenuEsp").SetActive(true);
    //            Debug.Log("spanish");
    //            break;     
    //        case language.english:
    //            GameObject.Find("OptionsMenuEng").SetActive(true);
    //            Debug.Log("english");
    //            break;
    //    }
    //}
}
