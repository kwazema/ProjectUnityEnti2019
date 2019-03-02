using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

    public Slider player1;
    public Slider player2;


    void AssociateLifeWithSlide()
    {
        int life1 = 100;
        int life2 = 100;

        player1.value = life1;
        player2.value = life2;
    }

    void ReduceLife()
    {     
        player1.value -= 1;
        player2.value -= 1;
    }

    private void Start()
    {
        AssociateLifeWithSlide();
    }

    private void Update()
    {
        ReduceLife();
    }    
}
