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

[System.Serializable]
public class PlayerReferences
{
    public Text name;

    public Image damagedHealthBar;
    public Image healthBar;
    public Image shielBar;

    public Image skillBar;
    public Image ultimateBar;

    private float damageFadeTimerCur;
    private float damageFadeTimerMax = 1.3f;
    private float lastResivedDamage;

    //public Text descriptionPlayer1;
    //public Text descriptionPlayer2;
    //public Text roundsWin;
    //public Image icon;
    //public Text textHealth;
    //public Image iconSkill;
    //public IconUpgrade iconUpgrade;

    public void UpdateSprite(Sprite sprite)
    {
    }

    public void UpdateName(string playerName)
    {
        //Example:
        //InitName(GameManager.instance.playerStats[0].namePlayer)
        name.text = playerName;
    }

    public void UpdateHealthDamageBar(float valueCur, float valueMax)
    {
        //damagedHealthBar.fillAmount = valueCur / valueMax;

        if (damagedHealthBar.color.a > 0)
        {
            // Cound down fade timer
            damageFadeTimerCur -= Time.deltaTime;

            if (damageFadeTimerCur < 0)
            {
                // Fade timer over, lower alpha
                Color newColor = damagedHealthBar.color;

                newColor.a -= Time.deltaTime * 2f;
                damagedHealthBar.color = newColor;

                if (damagedHealthBar.color.a <= 0)
                {
                    ReseHealthDamageBar(valueMax);
                }
            }
        }
    }

    private void ReseHealthDamageBar(float valueMax)
    {
        // Health changed, reset fade timer
        damageFadeTimerCur = damageFadeTimerMax; // Dentro de for?

        // damaged health bar no visible, set size
        damagedHealthBar.fillAmount = healthBar.fillAmount;

        // Make damaged health bar visible
        Color fullAplha = damagedHealthBar.color;

        fullAplha.a = 1f;
        damagedHealthBar.color = fullAplha;
    }

    public void UpdateHealthBar(float valueCur, float valueMax)
    {
        healthBar.fillAmount = valueCur / valueMax;

        if (lastResivedDamage != valueCur && damagedHealthBar.color.a == 1)
        {
            damageFadeTimerCur = damageFadeTimerMax; // Working: Hacer que la o las barras hagan un flash al recibir un golpe
        }

        lastResivedDamage = valueCur;
    }

    public void UpdateSkillBar(float valueCur, float valueMax)
    {
        skillBar.fillAmount = valueCur / valueMax;
        //Jugar con la tranparencia de la barra o con particulas o ambas

        //Example:
            //if (value >= 1)
            //    transparency.a = 1f;
            //else
            //    transparency.a = .3f;

            //sliderUltimate[i].color = transparency;
            //sliderUltimate[i].fillAmount = value;
    }

    public void UpdateUltimateBar(float valueCur, float valueMax)
    {
        ultimateBar.fillAmount = valueCur / valueMax;
        //Jugar con la tranparencia de la barra o con particulas o ambas
    }

    public void ResetFadeTimer()
    {
        damageFadeTimerCur = damageFadeTimerMax;
    }

    public void DebugLog()
    {
       // Debug.Log("Fade Timer Cur: " + damageFadeTimerCur);
    }


    //            textHealth[i].text = gameManager.playerStats[i].GetHealth().ToString();

    //  Hacer funciones para cada uso
    // Si no puedo acceder a gameManager porque esta fuera de esta clase cual es la mejor forma?
    // En que casos una clase hay que crearla en un scripts a Parte.

}

public class PlayeUI : MonoBehaviour
{

    public Text roundTime;
    private Text winPlayerRound;
    private Text roundCur;

    public PlayerReferences leftPlayer, rightPlayer;
    Color transparency = Color.white;

    public BattleSystem battleSystem;
    public GameObject skills;
    private GameManager gameManager;

    public RectTransform clockPos, leftPlayerPos, rightPlayerPos;
    public RectTransform clockPosPoint, leftPlayerPosPoint, rightPlayerPosPoint;

    private void Awake()
    {
        //healthImage[i] = transform.Find("player_1/health").Find("bar").GetComponent<Image>(); // Busca los hijos

        //healthImage = new Image[2];
        //damagedHealthImage = new Image[2];

        //for (int i = 0; i < 2; i++)
        //{
        //    string player = "player_" + (i + 1);
        //    healthImage[i] = GameObject.Find(player + "/health/bar").GetComponent<Image>();
        //    damagedHealthImage[i] = GameObject.Find(player + "/health/damaged").GetComponent<Image>();
        //}


        //---------------------------------//


        //gameManager = FindObjectOfType<GameManager>();
        //gameManager.playeUI = this;
    }

    private void Start()
    {
        //battleSystem.StartBattle();

        leftPlayer.UpdateName(GameManager.instance.playerManager[0].namePlayer);
        rightPlayer.UpdateName(GameManager.instance.playerManager[1].namePlayer);

        leftPlayer.ResetFadeTimer();
        rightPlayer.ResetFadeTimer();
        
        //leftPlayer.UpdateSprite()
        //for (int i = 0; i < iconUpgrade.Length; i++)
        //    for (int j = 0; j < iconUpgrade[i].spell.Length; j++)
        //        iconUpgrade[i].spell[j].sprite = gameManager.playerStats[i].upgrade[j];

        //for (int i = 0; i < descriptionPlayer1.Length; i++)
        //        descriptionPlayer1[i].text = gameManager.playerStats[0].upgrade_text[i];

        //for (int i = 0; i < descriptionPlayer2.Length; i++)
        //        descriptionPlayer2[i].text = gameManager.playerStats[1].upgrade_text[i];
    }

    private void Update()
    {
        leftPlayer.UpdateHealthBar(GameManager.instance.playerManager[0].GetHealth(), GameManager.instance.playerManager[0].GetHealthMax());
        rightPlayer.UpdateHealthBar(GameManager.instance.playerManager[1].GetHealth(), GameManager.instance.playerManager[1].GetHealthMax());

        leftPlayer.UpdateHealthDamageBar(GameManager.instance.playerManager[0].GetHealth(), GameManager.instance.playerManager[0].GetHealthMax());
        rightPlayer.UpdateHealthDamageBar(GameManager.instance.playerManager[1].GetHealth(), GameManager.instance.playerManager[1].GetHealthMax());

        //leftPlayer.UpdateSkillBar(GameManager.instance.playerStats[0].GetCurSkillCD(), GameManager.instance.playerStats[0].GetSkillCD());
        //rightPlayer.UpdateSkillBar(GameManager.instance.playerStats[1].GetCurSkillCD(), GameManager.instance.playerStats[1].GetSkillCD());

        //leftPlayer.UpdateUltimateBar(GameManager.instance.playerStats[0].GetCurUltimateCD(), GameManager.instance.playerStats[0].GetUltimateCD());
        //rightPlayer.UpdateUltimateBar(GameManager.instance.playerStats[1].GetCurUltimateCD(), GameManager.instance.playerStats[1].GetUltimateCD());

        roundTime.text = battleSystem.round.timeCur.ToString("#");
        //rightPlayer.DebugLog();
        //roundCur.text = (battleSystem.round.roundCur).ToString("Round 0");
    }


    // Working: Cuando empieza la partida hacer una animacion de las barras que vayan al centro
    // Y al relok que venga de afuera


    public int speedUI = 500;
    public void AnimationUI()
    {
        clockPos.anchoredPosition = Vector2.MoveTowards(clockPos.anchoredPosition, clockPosPoint.anchoredPosition, speedUI * Time.deltaTime);
        leftPlayerPos.anchoredPosition = Vector2.MoveTowards(leftPlayerPos.anchoredPosition, leftPlayerPosPoint.anchoredPosition, speedUI * Time.deltaTime);
        rightPlayerPos.anchoredPosition = Vector2.MoveTowards(rightPlayerPos.anchoredPosition, rightPlayerPosPoint.anchoredPosition, speedUI * Time.deltaTime);
    }











    //int numPlayer;
    //public void PlayerHability(int value)
    //{
    //    numPlayer = value;
    //}

    //public void ActiveHability(int hability)
    //{
    //    //if (battleSystem.round.roundCur - 1 < 3)
    //    //{
    //    //    iconUpgrade[numPlayer].selected[battleSystem.round.roundCur - 1].sprite = iconUpgrade[numPlayer].spell[hability].sprite;
    //    //    iconUpgrade[numPlayer].button[hability].interactable = false;

    //    //    switch (hability)
    //    //    {
    //    //        case 0: gameManager.playerStats[numPlayer].Upgrade1(); break;

    //    //        case 1: gameManager.playerStats[numPlayer].Upgrade2(); break;

    //    //        case 2: gameManager.playerStats[numPlayer].Upgrade3(); break;
    //    //    }
    //    //}
    //    //gameManager.playerStats[numPlayer].numUpgrade++;
    //    //iconUpgrade[numPlayer].num++;
    //}

    //public GameObject winPlayer1;
    //public GameObject winPlayer2;
    //public void WinPlayer()
    //{
    //    if (battleSystem.round.roundsWinPlayer1 > battleSystem.round.roundsWinPlayer2)
    //    {
    //        //win1 
    //        winPlayer2.SetActive(true);
    //    }
    //    else
    //    {
    //        winPlayer1.SetActive(true);
    //        //win 2
    //    }
    //}
}
