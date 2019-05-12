using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Library : MonoBehaviour {

    public GameObject info;
    public GameObject characters;

    public VideoController videoController;
    public VideoData videoData;

    public ListCharacters lc;

    public Text nameC;
    public Text descriptionC;
    public Text statsC;
    public Text skillC;
    public Text ultimateC;

    //private void Awake()
    //{

    //}
    // Use this for initialization
    void Start ()
    {
        lc = GameManager.instance.LoadFileToString();
    }

    public void ShowInfoCharacter(int index)
    {
        nameC.text = "Name: " + lc.characterStats[index].name;

        descriptionC.text = "Description: " + lc.characterStats[index].description;

        statsC.text = "Life: " + lc.characterStats[index].healthMax + "\nShield: " + lc.characterStats[index].shieldMax + "\nBasic damage: " +
            lc.characterStats[index].damageBasicAttack + "\nFire Rate: "+ lc.characterStats[index].fireRate + "\nSkill Damage: " +
            lc.characterStats[index].damageSkill + "\nSkill Cooldown: " + lc.characterStats[index].skillCD + "\nUltimate Damage: " +
            lc.characterStats[index].damageUltimate + "\nUltimate Cooldown: " + lc.characterStats[index].ultimateCD;


        videoController.videoPlayer.clip = videoData.skill;

        //skillC.text = lc.characterStats[index].
    }

    public void ShowNoInfo()
    {
        characters.SetActive(true);
        info.SetActive(false);
       // videoController.videoPlayer.clip = videoData.general;
        nameC.text = "";
        descriptionC.text = "";
        statsC.text = "";
        //skillC.text = " ";
        //ultimateC.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.Play("GeneralButton");
            ShowNoInfo();
        }        
    }
}
