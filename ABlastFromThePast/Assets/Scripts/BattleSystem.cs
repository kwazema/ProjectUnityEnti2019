using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Round
{
    public float timeToStartMax = 5;
    public float timeToStartCur;

    public float timeToStartFightMax = 2;
    public float timeToStartFightCur;

    public float timeMax = 60;
    public float timeCur;

    //public int roundMax;
    //public int roundCur;

    public int roundsWinPlayer1 = 0;
    public int roundsWinPlayer2 = 0;

    public int roundCur = 0;
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

    private void Start ()
    {
        StartBattle();
    }
    //private void Update () { }

    public void StartBattle()
    {
        //StartCoroutine(StartRound(roundCur));
        StartCoroutine(StartRound()); // Antes mostrar habilidades de subir
    }

    IEnumerator StartRound()
    {
        bool stopWhile = false;
        round.timeToStartCur = round.timeToStartMax;
        round.timeToStartFightCur = round.timeToStartFightMax;

        while (!stopWhile)
        {
            // Imprimir Animacion de Round 1 Con su fade
            round.timeToStartCur -= Time.deltaTime; Debug.Log("Se Muestra Round");
            if (round.timeToStartCur < 0)
            {
                round.timeToStartFightCur -= Time.deltaTime; Debug.Log("Se Muestra Fight");
                //Cuando se haya terminado fade mostrar Fight que desaparezma y empieze la partida
                playeUI.AnimationUI();
                if (round.timeToStartFightCur < 0)
                {
                    // Termina el fade y empieza la partida
                    StartCoroutine(TimeRound()); Debug.Log("Empieza la Partida"); 
                    // Mostrar Animation

                    // Reseteo los tiempos
                    round.timeToStartCur = round.timeToStartMax;
                    round.timeToStartFightCur = round.timeToStartFightMax;
                    stopWhile = true;
                }
            }

            yield return null;
        }
    }

    IEnumerator TimeRound()
    {
        round.timeCur = round.timeMax;

        //Comprobar si algun persnaje muere si el que gane se lleva round win
        while (round.timeCur >= 0 && gameManager.playerStats[0].GetHealth() > 0 && gameManager.playerStats[1].GetHealth() > 0)
        {
            round.timeCur -= Time.deltaTime;
            yield return null;
        }

        round.timeCur = round.timeMax;


        float healhPlayer1 = gameManager.playerStats[0].GetHealth() / gameManager.playerStats[0].GetHealthMax() * 100;
        float healhPlayer2 = gameManager.playerStats[1].GetHealth() / gameManager.playerStats[1].GetHealthMax() * 100;

        if (healhPlayer1 > healhPlayer2)
        {
            round.roundsWinPlayer2++;
        }
        else if (healhPlayer1 < healhPlayer2)
        {
            round.roundsWinPlayer1++;
        }
        else
        {
            //empate se juega una ronda mas.
            round.roundMax++; // Pensar si esto es correcto
        }

        if (round.roundsWinPlayer1 == 2 || round.roundsWinPlayer2 == 2)
        {
                //Mostrar Ganador y puntuacion
                //Mostrar boton para continuar
                //volver menu
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
        //playeUI.WinPlayer();
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
        //playeUI.skills.SetActive(true);
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

        //playeUI.skills.SetActive(false);
        StartCoroutine(TimeRound());
        //yield return null;
    }
}
