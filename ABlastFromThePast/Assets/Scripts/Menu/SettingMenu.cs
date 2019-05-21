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
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        GameManager.instance.volume = Mathf.Log10(volume) * 20;
    }

    public void Music(float volume)
    {
        audioMixer.SetFloat("volumeMusic", Mathf.Log10(volume) * 20);
        GameManager.instance.volumeMusic = Mathf.Log10(volume) * 20;
    }

    public void Effects(float volume)
    {
        audioMixer.SetFloat("volumeEffects", Mathf.Log10(volume) * 20);
        GameManager.instance.volumeEffects = Mathf.Log10(volume) * 20;
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
