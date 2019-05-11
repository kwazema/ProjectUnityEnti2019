using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Round
{
    public float timeToStartMax;
    public float timeToStartCur;

    public float timeToStartFightMax;
    public float timeToStartFightCur;

    public float timeToFadeUpgradeOutMax;
    public float timeToFadeUpgradeOutCur;

    public float timeMax;
    public float timeCur;

    //public int roundMax;
    //public int roundCur;

    public int roundsWinPlayer1;
    public int roundsWinPlayer2;

    public int roundCur;
    public int roundMax;
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
        Invoke("StartBattle", 1);
    }
    //private void Update () { }

    public void StartBattle()
    {
        StartCoroutine(ChoiseSkills());
    }

    IEnumerator ChoiseSkills()
    {
        //playeUI.skills.SetActive(true);
        bool stop = false;

        gameManager.playerManager[0].ResetCharacter();
        gameManager.playerManager[1].ResetCharacter();

        gameManager.playerManager[0].SetPlayerInputs(false);
        gameManager.playerManager[1].SetPlayerInputs(false);

        round.timeToFadeUpgradeOutCur = round.timeToFadeUpgradeOutMax;

        while (!stop)
        {
            // Imprimit tiempo pantalla
            playeUI.SetLateralPanelsAnimation(true, 500);
            playeUI.SetTopPanelsAnimation(false, 600);
            playeUI.SetLateralPanelsIconAnimation(false, 200);
            playeUI.continueText.SetTrigger("fadeIn");


            if (Input.GetKeyDown(KeyCode.Space))
                stop = true;

            yield return null;
        }

        playeUI.continueText.ResetTrigger("fadeIn");
        while (round.timeToFadeUpgradeOutCur > 0)
        {
            playeUI.SetLateralPanelsAnimation(false, 700);
            playeUI.continueText.SetTrigger("fadeOut");
            // TODO: Tener otro panel que solo tenga los logos y cuando se muestre empezar la partida

            round.timeToFadeUpgradeOutCur -= Time.deltaTime;
            yield return null;
        }

        round.timeToFadeUpgradeOutCur = round.timeToFadeUpgradeOutMax;
        //playeUI.skills.SetActive(false);
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        bool stopWhile = false;
        round.timeToStartCur = round.timeToStartMax;
        round.timeToStartFightCur = round.timeToStartFightMax;

        while (!stopWhile)
        {
            // Imprimir Animacion de Round 1 Con su fade
            round.timeToStartCur -= Time.deltaTime;
            // Debug.Log("Se Muestra Round: " + round.timeToStartCur);

            if (round.timeToStartCur < 0)
            {
                round.timeToStartFightCur -= Time.deltaTime;
               // Debug.Log("Se Muestra Fight");
                //Cuando se haya terminado fade mostrar Fight que desaparezma y empieze la partida
                playeUI.SetTopPanelsAnimation(true, 700);
                playeUI.SetLateralPanelsIconAnimation(true, 200);


                if (round.timeToStartFightCur < 0)
                {
                    // Termina el fade y empieza la partida
                    StartCoroutine(TimeRound());
                    Debug.Log("Empieza la Partida"); 
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

        gameManager.playerManager[0].SetPlayerInputs(true);
        gameManager.playerManager[1].SetPlayerInputs(true);

        //Comprobar si algun persnaje muere si el que gane se lleva round win
        while (round.timeCur >= 0 && gameManager.playerManager[0].GetHealth() > 0 && gameManager.playerManager[1].GetHealth() > 0)
        {
            round.timeCur -= Time.deltaTime;
            yield return null;
        }

        round.timeCur = round.timeMax;


        float healhPlayer1 = gameManager.playerManager[0].GetHealth() / gameManager.playerManager[0].GetHealthMax() * 100;
        float healhPlayer2 = gameManager.playerManager[1].GetHealth() / gameManager.playerManager[1].GetHealthMax() * 100;

        if (healhPlayer1 > healhPlayer2)
            round.roundsWinPlayer2++;
        else if (healhPlayer1 < healhPlayer2)
            round.roundsWinPlayer1++;
        else
            round.roundMax++; //empate se juega una ronda mas.

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
}
