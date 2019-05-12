using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Library : MonoBehaviour {

    public GameObject info;
    public GameObject characters;

    public VideoController videoController;
    public VideoData[] videoData;

    public ListCharacters lc;

    public Text nameC;
    public Text descriptionC;
    public Text statsC;
    public Text skillC;
    public Text ultimateC;
    public Text skillDamage;
    public Text ultiDamage;

    public Button skillPlay;
    public Button ultiPlay;

    public RawImage rawImage;
    public RenderTexture render;

    public int indexVideo;

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
        nameC.text = lc.characterStats[index].name;

        descriptionC.text = lc.characterStats[index].description;

        statsC.text = "Life: " + lc.characterStats[index].healthMax + "\nShield: " + lc.characterStats[index].shieldMax + "\nBasic damage: " +
            lc.characterStats[index].damageBasicAttack;

        skillC.text = lc.characterStats[index].nameSkill;
        skillDamage.text = lc.characterStats[index].damageSkill.ToString();
        ultimateC.text = lc.characterStats[index].nameUltimate;
        ultiDamage.text = lc.characterStats[index].damageUltimate.ToString();

        //skillC.text = lc.characterStats[index].
    }

    public void PlaySkill(int index)
    {
        rawImage.texture = render;
        videoController.videoPlayer.clip = videoData[index].skill;
    }

    public void PlayUlti(int index)
    {
        Debug.Log(index);
        rawImage.texture = render;
        videoController.videoPlayer.clip = videoData[index].ultimate;
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
