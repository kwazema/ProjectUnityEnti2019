using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Round
{
    public float timeToStartMax = 5;
    public float timeToStartCur;

    public float timeSet = 60;
    public float timeCur;

    //public int roundMax;
    //public int roundCur;

    public int roundsWinPlayer1 = 0;
    public int roundsWinPlayer2 = 0;

    public int roundCur = -1;
    public int roundMax = 3;
}


public class BattleSystem : MonoBehaviour
{
    public Round round;
    private GameManager gameManager;
    private PlayeUI playeUI;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playeUI = FindObjectOfType<PlayeUI>();
    }

    //private void Start () { }
    //private void Update () { }

    public void StartBattle()
    {
        //StartCoroutine(StartRound(roundCur));
        StartCoroutine(TimeRound());
    }

    IEnumerator StartRound()
    {
        while (true)
        {
            if (round.timeToStartCur <= round.timeToStartCur)
            {
                // Imprimit tiempo pantalla
                yield return new WaitForSeconds(0);
            }
            else
            {
                // imprimir habilidades y empezar con el StartRound si es necesario
                yield return null;
            }
        }
    }

    IEnumerator TimeRound()
    {
        round.timeCur = 60;

        while (round.timeCur >= 0 && gameManager.playerStats[0].GetHealth() > 0 && gameManager.playerStats[1].GetHealth() > 0)
        {
            //Comprobar si algun persnaje muere si el que gane se lleva round win
            // 

            round.timeCur -= Time.deltaTime;
            yield return null;
        }

        round.timeCur = 60;

        if (gameManager.playerStats[0].GetHealth() > gameManager.playerStats[1].GetHealth())
        {
            round.roundsWinPlayer2++;
            //playeUI
        }
        else if (gameManager.playerStats[0].GetHealth() < gameManager.playerStats[1].GetHealth())
        {
            round.roundsWinPlayer1++;
        }
        else
        {
            //empate se juega una ronda mas.
            round.roundMax++;
        }

        if (round.roundsWinPlayer1 == 2 || round.roundsWinPlayer2 == 2)
        {
            if (round.roundCur == 3)
            {
                //Mostrar Ganador y puntuacion
                //Mostrar boton para continuar
                //volver menu
            }
            Invoke("WinPlayer", 1);
            Invoke("Fade", 4);
            Invoke("GoToMenu", 6);

        }
        else
        {
            round.roundCur++;
            StartCoroutine(ChoiseSkills());
        }

        //yield return null;
    }

    void WinPlayer()
    {
        playeUI.WinPlayer();
    }

    //void GoToMenu()
    //{
    //    round.roundsWinPlayer1 = 0;
    //    round.roundsWinPlayer2 = 0;
    //    round.roundCur = -1;
    //    SceneManager.LoadScene("Menu");
    //}

    IEnumerator ChoiseSkills()
    {
        playeUI.skills.SetActive(true);
        bool choised = false;

        while (!choised)
        {
            // Imprimit tiempo pantalla
            if (Input.GetKeyDown(KeyCode.Space)) // un intento
            {
                choised = true;
                gameManager.playerStats[0].ResetCharacter();
                gameManager.playerStats[1].ResetCharacter();
                Debug.Log("Espacio");
            }

            yield return null;
        }

        playeUI.skills.SetActive(false);
        StartCoroutine(TimeRound());
        //yield return null;
    }
}
