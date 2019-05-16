using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Library : MonoBehaviour {

    public GameObject info;
    public GameObject characters;

    //public VideoController videoController;
    //public VideoData[] videoData;

    public ListCharacters lc;
    public VideoController vc;

    public Text nameCharacter;
    public Text description;

    public Text health;
    public Text shield;

    public Text damageAttack;
    public Text noSe;

    public Text nameSkill;
    public Text skillDamage;
    public Text skillCD;
    public Text skillDesc;

    public Text nameUltimate;
    public Text ultiDamage;
    public Text ultiCD;
    public Text ultiDesc;


    public Button skillPlay;
    public Button ultiPlay;

    //public RawImage rawImage;
    //public RenderTexture render;

    public int indexVideo;

       

    void Start ()
    {
        //rawImage.texture = render;

        lc = GameManager.instance.LoadFileToString();
    }

    public void ShowInfoCharacter(int index)
    {
        nameCharacter.text = lc.characterStats[index].name;
        description.text = lc.characterStats[index].description;

        health.text = lc.characterStats[index].healthMax.ToString();
        shield.text = lc.characterStats[index].shieldMax.ToString();
        damageAttack.text = lc.characterStats[index].damageBasicAttack.ToString();

        nameSkill.text = lc.characterStats[index].nameSkill;
        skillDamage.text = lc.characterStats[index].damageSkill.ToString();
        skillCD.text = lc.characterStats[index].skillCD.ToString() + "s";
        skillDesc.text = lc.characterStats[index].descSkill;

        nameUltimate.text = lc.characterStats[index].nameUltimate;
        ultiDamage.text = lc.characterStats[index].damageUltimate.ToString();
        ultiCD.text = lc.characterStats[index].ultimateCD.ToString() + "s";
        ultiDesc.text = lc.characterStats[index].descUltimate;

        //skillC.text = lc.characterStats[index].
    }

    public void PlaySkill()
    {
        if (nameCharacter.text == "Santa")
        {
            vc.PlayVideoLibrary("SkillSanta");
        }
        else if (nameCharacter.text == "Minos")
        {
            vc.PlayVideoLibrary("SkillMinos");
        }
        else if (nameCharacter.text == "Polyphemus")
        {
            vc.PlayVideoLibrary("SkillPolyphemus");
        }
        else if (nameCharacter.text == "Adventurer")
        {
            vc.PlayVideoLibrary("SkillAdventurer");
        }
    }

    public void PlayUlti()
    {
        if (nameCharacter.text == "Santa")
        {
            vc.PlayVideoLibrary("UltiSanta");
        }
        else if (nameCharacter.text == "Minos")
        {
            vc.PlayVideoLibrary("UltiMinos");
        }
        else if (nameCharacter.text == "Polyphemus")
        {
            vc.PlayVideoLibrary("UltiPolyphemus");
        }
        else if (nameCharacter.text == "Adventurer")
        {
            vc.PlayVideoLibrary("UltiAdventurer");
        }
    }

    public void ShowNoInfo()
    {
        characters.SetActive(true);
        info.SetActive(false);
       // videoController.videoPlayer.clip = videoData.general;
        nameCharacter.text = "";
        description.text = "";
        //statsC.text = "";
        //skillC.text = " ";
        //ultimateC.text = "";
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    AudioManager.instance.Play("GeneralButton");
        //    //EventSystem eventSystem = FindObjectOfType<EventSystem>();
        //    //eventSystem.SetSelectedGameObject(GameObject.Find("LibraryButtonSanta")); // Selecciona un nuevo boton
        //    ShowNoInfo();
        //}        
    }

    //public void PlayGeneralVideo()
    //{
    //    rawImage.texture = render;
    //    videoController.videoPlayer.clip = videoData[4].skill;
    //}
}
