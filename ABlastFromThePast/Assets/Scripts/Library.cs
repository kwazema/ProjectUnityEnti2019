using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public int index = 0;

    //private void Awake()
    //{

    //}
    // Use this for initialization
    void Start () {
        lc = GameManager.instance.LoadFileToString();

    }

    public void ShowInfoSanta()
    {
        nameC.text = "Name: " + lc.characterStats[index].name;
        descriptionC.text = "Description: " + lc.characterStats[index].description;
        statsC.text = "Life: " + lc.characterStats[index].healthMax + "\nShield: " + lc.characterStats[index].shieldMax + "\nBasic damage: " +
            lc.characterStats[index].damageBasicAttack + "\nFire Rate: "+ lc.characterStats[index].fireRate + "\nSkill Damage: " +
            lc.characterStats[index].damageSkill + "\nSkill Cooldown: " + lc.characterStats[index].skillCD + "\nUltimate Damage: " +
            lc.characterStats[index].damageUltimate + "\nUltimate Cooldown: " + lc.characterStats[index].ultimateCD;

        videoController.videoPlayerSkill.clip = videoData.skill;

        //skillC.text = lc.characterStats[index].
        //TODO: Add skills and ultimate name to file
    }

    public void ShowInfoMinos()
    {
        nameC.text = "Name: " + lc.characterStats[index + 1].name;
        descriptionC.text = "Description: " + lc.characterStats[index +1].description;
        statsC.text = "Life: " + lc.characterStats[index + 1].healthMax + "\nShield: " + lc.characterStats[index + 1].shieldMax + "\nBasic damage: " +
            lc.characterStats[index+1].damageBasicAttack + "\nFire Rate: " + lc.characterStats[index + 1].fireRate + "\nSkill Damage: " +
            lc.characterStats[index+1].damageSkill + "\nSkill Cooldown: " + lc.characterStats[index + 1].skillCD + "\nUltimate Damage: " +
            lc.characterStats[index + 1].damageUltimate + "\nUltimate Cooldown: " + lc.characterStats[index + 1].ultimateCD;

        videoController.videoPlayerSkill.clip = videoData.ultimate;

        //skillC.text = lc.characterStats[index].
        //TODO: Add skills and ultimate name to file
    }

    public void ShowNoInfo()
    {
        characters.SetActive(true);
        info.SetActive(false);
        videoController.videoPlayerSkill.clip = videoData.general;
        nameC.text = "";
        descriptionC.text = "";
        statsC.text = "";
        skillC.text = "";
        ultimateC.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowNoInfo();
        }        
    }
}
