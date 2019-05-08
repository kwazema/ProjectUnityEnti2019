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

    public Image[] healthImage;
    public Image[] damagedHealthImage;
    private float damageFadeTimerCur;
    private float damageFadeTimerMax = 2f;
    //https://www.youtube.com/watch?v=oLEEPL2WmAk //min 25
    // hacer que la barra verde haga un flash blanco cuando te hacen daño
    //------------------------//

    private Text roundTime;
    private Text winPlayerRound;
    private Text roundCur;
    private Text[] namePlayer;
    private Text[] descriptionPlayer1;
    private Text[] descriptionPlayer2;
    private Text[] roundsWin;
    private Image[] icon;
    private Text[] textHealth;
    private Image[] sliderSkill;
    private Image[] iconSkill;
    private Image[] sliderUltimate;
    private Image[] sliderShield;
    private IconUpgrade[] iconUpgrade;

    Color transparency = Color.white;

    public BattleSystem battleSystem;
    public GameObject skills;
    private GameManager gameManager;

    private void Awake()
    {
        //healthImage[i] = transform.Find("player_1/health").Find("bar").GetComponent<Image>(); // Busca los hijos

        healthImage = new Image[2];
        damagedHealthImage = new Image[2];
        
        for (int i = 0; i < 2; i++)
        {
            string player = "player_" + (i + 1);
            healthImage[i] = GameObject.Find(player + "/health/bar").GetComponent<Image>();
            damagedHealthImage[i] = GameObject.Find(player + "/health/damaged").GetComponent<Image>();
        }


        //---------------------------------//


        gameManager = FindObjectOfType<GameManager>();
        //gameManager.playeUI = this;
    }

    private void Start()
    {
        battleSystem.StartBattle();

        for (int i = 0; i < iconUpgrade.Length; i++)
            for (int j = 0; j < iconUpgrade[i].spell.Length; j++)
                iconUpgrade[i].spell[j].sprite = gameManager.playerStats[i].upgrade[j];

        for (int i = 0; i < namePlayer.Length; i++)
            namePlayer[i].text = gameManager.playerStats[i].namePlayer;

        for (int i = 0; i < descriptionPlayer1.Length; i++)
                descriptionPlayer1[i].text = gameManager.playerStats[0].upgrade_text[i];

        for (int i = 0; i < descriptionPlayer2.Length; i++)
                descriptionPlayer2[i].text = gameManager.playerStats[1].upgrade_text[i];
    }

    private void Update()
    {
        for (int i = 0; i < healthImage.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetHealth() / (float)gameManager.playerStats[i].GetHealthMax();
            healthImage[i].fillAmount = value;

            textHealth[i].text = gameManager.playerStats[i].GetHealth().ToString();
        }

        for (int i = 0; i < sliderShield.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetShield() / (float)gameManager.playerStats[i].GetShieldMax(); 
            sliderShield[i].fillAmount = value;
        }

        ////for (int i = 0; i < roundsWin.Length; i++)
        {
            roundsWin[0].text = battleSystem.round.roundsWinPlayer1.ToString();
            roundsWin[1].text = battleSystem.round.roundsWinPlayer2.ToString();
        }

        for (int i = 0; i < sliderSkill.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetCurSkillCD() / (float)gameManager.playerStats[i].GetSkillCD();

            if (value >= 1)
                transparency.a = 1f;
            else
                transparency.a = .3f;

            iconSkill[i].color = transparency;

            sliderSkill[i].fillAmount = value;
        }

        for (int i = 0; i < sliderUltimate.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetCurUltimateCD() / (float)gameManager.playerStats[i].GetUltimateCD();

            if (value >= 1)
                transparency.a = 1f;
            else
                transparency.a = .3f;

            sliderUltimate[i].color = transparency;
            sliderUltimate[i].fillAmount = value;
        }

        roundTime.text = battleSystem.round.timeCur.ToString("0.00");
        roundCur.text = (battleSystem.round.roundCur).ToString("Round 0");
    }

    int numPlayer;
    public void PlayerHability(int value)
    {
        numPlayer = value;
    }

    public void ActiveHability(int hability)
    {
        if (battleSystem.round.roundCur - 1 < 3)
        {
            iconUpgrade[numPlayer].selected[battleSystem.round.roundCur - 1].sprite = iconUpgrade[numPlayer].spell[hability].sprite;
            iconUpgrade[numPlayer].button[hability].interactable = false;

                switch (hability)
                {
                    case 0: gameManager.playerStats[numPlayer].Upgrade1(); break;

                    case 1: gameManager.playerStats[numPlayer].Upgrade2(); break;

                    case 2: gameManager.playerStats[numPlayer].Upgrade3(); break;
                }
        }
        //gameManager.playerStats[numPlayer].numUpgrade++;
        //iconUpgrade[numPlayer].num++;
    }

    public GameObject winPlayer1;
    public GameObject winPlayer2;
    public void WinPlayer()
    {
        if (battleSystem.round.roundsWinPlayer1 > battleSystem.round.roundsWinPlayer2)
        {
            //win1 
            winPlayer2.SetActive(true);
        }
        else
        {
            winPlayer1.SetActive(true);
            //win 2
        }
    }
}
