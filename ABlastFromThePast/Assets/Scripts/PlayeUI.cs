using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayeUI : MonoBehaviour {

    public Text roundTime;
    public Text roundCur;
    public Image[] icon;
    public Image[] sliderHealth;
    public Image[] sliderSkill;
    public Image[] sliderUltimate;
    public Image[] sliderShield;
    public string[] namePlayer;

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
	}

    private void Update()
    {
        for (int i = 0; i < sliderHealth.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetHealth() / (float)gameManager.playerStats[i].GetHealthMax();
            sliderHealth[i].fillAmount = value;
        }

        roundTime.text = gameManager.round.timeCur.ToString("0.00");
        roundCur.text = (gameManager.round.roundCur + 1).ToString("Round 0");
    }
}
