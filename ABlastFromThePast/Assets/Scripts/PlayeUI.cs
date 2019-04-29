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
        float test = gameManager.playerStats[0].GetHealth() * 1f / gameManager.playerStats[0].GetHealthMax();
        Debug.Log("Vida: " + test);
        Debug.Log("Vida Total: " + gameManager.playerStats[0].GetHealth());
        Debug.Log("Vida Maxima: " + gameManager.playerStats[0].GetHealthMax());
        sliderHealth[0].fillAmount = test;
	}
}
