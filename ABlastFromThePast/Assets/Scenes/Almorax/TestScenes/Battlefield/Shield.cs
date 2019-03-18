using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

    public Slider player1;
    public Slider player2;


    void AssociateShieldWithSlide()
    {
        int life1 = 100;
        int life2 = 100;

        player1.value = life1;
        player2.value = life2;
    }

    void ReduceShield()
    {
        player1.value -= 0.01f;
        player2.value -= 0.01f;
    }

    private void Start()
    {
        AssociateShieldWithSlide();
    }

    private void Update()
    {
        ReduceShield();
    }
}
