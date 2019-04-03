using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

    Game game;

    public Slider player1;
    public Slider player2;

    private void Awake()
    {
        game = GameObject.Find("Map").GetComponent<Game>();
    }


    private void Start()
    {
        //AssociateShieldWithSlide();
    }

    private void Update()
    {
        SliderShield();
    }

    void SliderShield()
    {
        float shield1 = game.playerStats[0].GetShield() / 20f * 100f;
        float shield2 = game.playerStats[1].GetShield() / 20f * 100f;

        player1.value = shield1;
        player2.value = shield2;
    }

    void AssociateShieldWithSlide()
    {
        int shield1 = game.playerStats[0].GetShield();
        int shield2 = game.playerStats[1].GetShield();

        player1.value = shield1;
        player2.value = shield2;
    }

    void ReduceShield()
    {
        player1.value -= 1;
        player2.value -= 1;
    }

}
