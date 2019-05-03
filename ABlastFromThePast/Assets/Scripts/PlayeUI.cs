using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IconUpgrade
{
    public Image[] spell;
    public Image[] selected;
    public Button[] button; 

    public int num = 0;
}

public class PlayeUI : MonoBehaviour {

    public Text roundTime;
    public Text roundCur;
    public Text[] namePlayer;
    public Image[] icon;
    public Image[] sliderHealth;
    public Image[] sliderSkill;
    public Image[] iconSkill;
    public Image[] sliderUltimate;
    public Image[] sliderShield;
    public IconUpgrade[] iconUpgrade;
    //public Image[] button;
    Color transparency = Color.white;

    //public Image[] iconUpgradePlayer2;
    //public int numUpgradePlayer1;
    //public int numUpgradePlayer2;



    public GameObject skills;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.playeUI = this;
    }

    private void Start()
    {
        gameManager.StartBattle();

        for (int i = 0; i < iconUpgrade.Length; i++)
            for (int j = 0; j < iconUpgrade[i].spell.Length; j++)
                iconUpgrade[i].spell[j].sprite = gameManager.playerStats[i].upgrade[j];

        for (int i = 0; i < namePlayer.Length; i++)
        {
            namePlayer[i].text = gameManager.playerStats[i].namePlayer;
        }
    }

    private void Update()
    {
        for (int i = 0; i < sliderHealth.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetHealth() / (float)gameManager.playerStats[i].GetHealthMax();
            sliderHealth[i].fillAmount = value;
        }

        for (int i = 0; i < sliderShield.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetShield() / (float)gameManager.playerStats[i].GetHealthMax(); //Cambiar Get Shield Max
            sliderShield[i].fillAmount = value;
        }

        for (int i = 0; i < sliderSkill.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetCurSkillCD() / (float)gameManager.playerStats[i].GetSkillCD();

            if (value == 1)
                transparency.a = 1f;
            else
                transparency.a = .3f;

            iconSkill[i].color = transparency;

            sliderSkill[i].fillAmount = value;
        }

        for (int i = 0; i < sliderUltimate.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetCurUltimateCD() / (float)gameManager.playerStats[i].GetUltimateCD();

            if (value == 1)
                transparency.a = 1f;
            else
                transparency.a = .3f;

            sliderUltimate[i].color = transparency;
            sliderUltimate[i].fillAmount = value;
        }

        roundTime.text = gameManager.round.timeCur.ToString("0.00");
        roundCur.text = (gameManager.round.roundCur + 1).ToString("Round 0");
    }

    int numPlayer;
    public void PlayerHability(int value)
    {
        numPlayer = value;
    }

    public void ActiveHability(int hability)
    {
        if (gameManager.round.roundCur - 1 < 3)
        {
            iconUpgrade[numPlayer].selected[gameManager.round.roundCur - 1].sprite = iconUpgrade[numPlayer].spell[hability].sprite;
            iconUpgrade[numPlayer].button[hability].interactable = false;
            
        }
        //gameManager.playerStats[numPlayer].numUpgrade++;
        //iconUpgrade[numPlayer].num++;
    }
}
