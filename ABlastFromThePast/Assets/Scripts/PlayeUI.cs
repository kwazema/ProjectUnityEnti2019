using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayeUI : MonoBehaviour {

    public Image[] icon;
    public Image[] sliderHealth;
    public Image[] sliderSkill;
    public Image[] sliderUltimate;
    public Image[] sliderShield;
    public string[] namePlayer;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
		
	}

    private void Update()
    {
        for (int i = 0; i < sliderHealth.Length; i++)
        {
            float value = (float)gameManager.playerStats[i].GetHealth() / (float)gameManager.playerStats[i].GetHealthMax();
            sliderHealth[i].fillAmount = value;
        }
	}
}
