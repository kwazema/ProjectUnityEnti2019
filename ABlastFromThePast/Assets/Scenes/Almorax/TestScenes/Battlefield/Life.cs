using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

    private GameManager gameManager;

    public Slider player1;
    public Slider player2;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    private void Start()
    {
        //AssociateLifeWithSlide();
    }

    private void Update()
    {
        SliderHealth();
    }    

    void AssociateLifeWithSlide()
    {
        float life1 = gameManager.playerManager[0].GetHealth() / 20f * 100f; 
        float life2 = gameManager.playerManager[1].GetHealth() / 20f * 100f;

        player1.value = life1;
        player2.value = life2;
    }

    void SliderHealth()
    {
        float life1 = gameManager.playerManager[0].GetHealth() / 100f * 100f; 
        float life2 = gameManager.playerManager[1].GetHealth() / 100f * 100f;

        player1.value = life1;
        player2.value = life2;
    }

    void ReduceLife()
    {
        int damage1 = gameManager.playerManager[0].GetDamageBasicAttack();
        int damage2 = gameManager.playerManager[1].GetDamageBasicAttack();


        player1.value -= damage1;
        player2.value -= damage2;
    }

}
