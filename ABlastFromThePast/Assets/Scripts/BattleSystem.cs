﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using XInputDotNetPure;

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
    private Map map;

    public ListCharacters lc;
    public EventSystem eventSystem;

    private void Awake()
    {
        map = FindObjectOfType<Map>();
        gameManager = FindObjectOfType<GameManager>();
        playeUI = FindObjectOfType<PlayeUI>();
    }

    private void Start ()
    {
        Invoke("StartBattle", 1);

        if (Random.Range(0, 100) > 50)
            AudioManager.instance.Play("MusicBattle01");
        else
            AudioManager.instance.Play("MusicBattle02");

        AudioManager.instance.Stop("MusicMenu01");
        AudioManager.instance.Stop("MusicMenu02");
        AudioManager.instance.Stop("MusicMenu03");
        AudioManager.instance.Stop("MusicMenu04");
        AudioManager.instance.Stop("MusicMenu05");

        lc = GameManager.instance.LoadFileToString();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private string lastSelectect;

    private void Update ()
    {
      
    }

    public void StartBattle()
    {
        StartCoroutine(ChoiseSkills());
    }

    IEnumerator ChoiseSkills()
    {
        //playeUI.skills.SetActive(true);
        bool stop = false;

        //gameManager.playerManager[0].ResetCharacter();
        //gameManager.playerManager[1].ResetCharacter();
        eventSystem.SetSelectedGameObject(GameObject.Find("uppgrade01p1")); // Selecciona un nuevo boton

        gameManager.playerManager[0].anim.SetBool("attack", false);
        gameManager.playerManager[1].anim.SetBool("attack", false);

        gameManager.playerManager[0].SetPlayerPos();
        gameManager.playerManager[1].SetPlayerPos();

        //gameManager.playerManager[0].SetPlayerInputs(false);
        //gameManager.playerManager[1].SetPlayerInputs(false);

        StartCoroutine(gameManager.playerManager[0].SetPlayerInputs(false));
        StartCoroutine(gameManager.playerManager[1].SetPlayerInputs(false));

        round.timeToFadeUpgradeOutCur = round.timeToFadeUpgradeOutMax;
        map.ResetBlocks();

        float timeForSelectUpgradeMax = 1;
        float timeForSelectUpgrade = timeForSelectUpgradeMax;

        while (!stop)
        {
            timeForSelectUpgrade -= Time.deltaTime;

            // Imprimit tiempo pantalla
            playeUI.SetLateralPanelsAnimation(true, 500);
            playeUI.SetTopPanelsAnimation(false, 700);
            playeUI.SetLateralPanelsIconAnimation(false, 200);

            playeUI.selectUpgradeText.SetTrigger("fadeIn");
            playeUI.selectUpgradeText.SetTrigger("normal");

            if (selectPlayer1 && selectPlayer2)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Start0") || Input.GetButton("Start1"))
                    stop = true;

                    playeUI.continueText.SetTrigger("fadeIn");

                playeUI.selectUpgradeText.ResetTrigger("fadeIn");
                playeUI.selectUpgradeText.ResetTrigger("normal");
                // TODO: Botones de movimiento de player 1 izquierda player 2 derecha
            }


            yield return null;
        }

        selectPlayer1 = false;
        selectPlayer2 = false;

        playeUI.continueText.ResetTrigger("fadeIn");
        playeUI.selectUpgradeText.SetTrigger("fadeOut");

        while (round.timeToFadeUpgradeOutCur > 0)
        {
            playeUI.SetLateralPanelsAnimation(false, 700);
            playeUI.SetTopPanelsAnimation(false, 500);
            playeUI.continueText.SetTrigger("fadeOut");
            // TODO: Tener otro panel que solo tenga los logos y cuando se muestre empezar la partida



            round.timeToFadeUpgradeOutCur -= Time.deltaTime;
            yield return null;
        }

        playeUI.continueText.ResetTrigger("fadeOut");
        round.timeToFadeUpgradeOutCur = round.timeToFadeUpgradeOutMax;
        //playeUI.skills.SetActive(false);
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        bool stopWhile = false;

        round.timeToStartCur = round.timeToStartMax;
        round.timeToStartFightCur = round.timeToStartFightMax;

        switch (round.roundCur)
        {
            case 0:
                AudioManager.instance.Play("Round1");
                playeUI.imageRound1.SetTrigger("Fade");
                break;

            case 1:
                AudioManager.instance.Play("Round2");
                playeUI.imageRound2.SetTrigger("Fade");
                break
                    ;
            case 2:
                AudioManager.instance.Play("Round3");
                playeUI.imageRound3.SetTrigger("Fade");
                break;
        }

        StartCoroutine(ControllerManager.ControllerVibration(0, 1, 1, 0.25f, 0.5f));
        StartCoroutine(ControllerManager.ControllerVibration(1, 1, 1, 0.25f, 0.5f));

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
                    //Debug.Log("Empieza la Partida"); 
                    // Mostrar Animation
                    //switch (round.roundCur)
                    //{
                    //    case 0: playeUI.imageRound1.SetTrigger("Fade"); break;
                    //    case 1: playeUI.imageRound2.SetTrigger("Fade"); break;
                    //    case 2: playeUI.imageRound3.SetTrigger("Fade"); break;
                    //}

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

        gameManager.playerManager[0].ResetCharacter();
        gameManager.playerManager[1].ResetCharacter();

        yield return new WaitForSeconds(1.5f);

        //gameManager.playerManager[0].SetPlayerInputs(true);
        //gameManager.playerManager[1].SetPlayerInputs(true);

        StartCoroutine(gameManager.playerManager[0].SetPlayerInputs(true));
        StartCoroutine(gameManager.playerManager[1].SetPlayerInputs(true));

        AudioManager.instance.Play("Fight");
        playeUI.imageFight.SetTrigger("Fade");

        StartCoroutine(ControllerManager.ControllerVibration(0, 1, 1, 0.5f, 0.3f));
        StartCoroutine(ControllerManager.ControllerVibration(1, 1, 1, 0.5f, 0.3f));
        
        //Comprobar si algun persnaje muere si el que gane se lleva round win
        while (round.timeCur >= 0 && gameManager.playerManager[0].GetHealth() > 0 && gameManager.playerManager[1].GetHealth() > 0)
        {
            round.timeCur -= Time.deltaTime;
            yield return null;
        }

//        if (gameManager.playerManager[0].GetHealth() <= 0 || gameManager.playerManager[1].GetHealth() <= 0)
//          yield return new WaitForSeconds(1.5f); // <-- Esto es para que cuando se muera se espere 3 segundos antes de enviar al personaje a su lugar
        
        round.timeCur = round.timeMax;

        float healhPlayer1 = (float)gameManager.playerManager[0].GetHealth() / (float)gameManager.playerManager[0].GetHealthMax() * 100;
        //Debug.Log("Vida Player 1: " + healhPlayer1);
        float healhPlayer2 = (float)gameManager.playerManager[1].GetHealth() / (float)gameManager.playerManager[1].GetHealthMax() * 100;
        //Debug.Log("Vida Player 2: " + healhPlayer2);

        if (healhPlayer1 > healhPlayer2)
        {
            round.roundsWinPlayer1++;
            playeUI.leftPlayer.SetWinPlayer(round.roundsWinPlayer1);

            lc.characterStats[GameManager.instance.playerChoise[0]].gameStats.roundsWin++;
            lc.characterStats[GameManager.instance.playerChoise[1]].gameStats.roundsLose++;

            //Debug.Log(lc.characterStats[GameManager.instance.playerChoise[0]].gameStats.roundsWin);
            //Debug.Log(lc.characterStats[GameManager.instance.playerChoise[1]].gameStats.roundsWin);
        }
        else if (healhPlayer1 < healhPlayer2)
        {
            round.roundsWinPlayer2++;
            playeUI.rightPlayer.SetWinPlayer(round.roundsWinPlayer2);

            lc.characterStats[GameManager.instance.playerChoise[0]].gameStats.roundsLose++;
            lc.characterStats[GameManager.instance.playerChoise[1]].gameStats.roundsWin++;

            //Debug.Log(lc.characterStats[GameManager.instance.playerChoise[0]].gameStats.roundsWin);
            //Debug.Log(lc.characterStats[GameManager.instance.playerChoise[1]].gameStats.roundsWin);
        }
        else if (healhPlayer1 == healhPlayer2)
        {
            round.roundMax++; //empate se juega una ronda mas.
            // TODO: Comprobar porque entra
        }

        if (round.roundsWinPlayer1 == 2 || round.roundsWinPlayer2 == 2)
        {
            Invoke("WinPlayer", 1);
            Invoke("Fade", 4);
            Invoke("GoToMenu", 7);
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
        if (round.roundsWinPlayer1 > round.roundsWinPlayer2)
        {
            playeUI.leftPlayer.WinGame.SetActive(true);
            playeUI.leftPlayer.WinGame.GetComponent<Animator>().SetTrigger("FadeIn");

            lc.characterStats[GameManager.instance.playerChoise[0]].gameStats.gamesWin++;
            lc.characterStats[GameManager.instance.playerChoise[1]].gameStats.gamesLose++;
        }
        else
        {
            playeUI.rightPlayer.WinGame.SetActive(true);
            playeUI.rightPlayer.WinGame.GetComponent<Animator>().SetTrigger("FadeIn");

            lc.characterStats[GameManager.instance.playerChoise[0]].gameStats.gamesLose++;
            lc.characterStats[GameManager.instance.playerChoise[1]].gameStats.gamesWin++;
        }

        GameManager.instance.SaveStringToFile(lc);
    }
    
    void Fade()
    {
        FindObjectOfType<FadeImage>().FadeToBlack();
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    //--------------- Upgrades -----------------//

    public bool selectPlayer1, selectPlayer2;

    public void UpgradePlayer1(int upgrade)
    {
        if (!selectPlayer1)
        {
            switch (upgrade)
            {
                case 0:
                    GameManager.instance.playerManager[0].Upgrade1();
                    playeUI.leftPlayer.buttonUpgrade01.interactable = false;
                    break;

                case 1:
                    GameManager.instance.playerManager[0].Upgrade2();
                    playeUI.leftPlayer.buttonUpgrade02.interactable = false;
                    break;

                case 2:
                    GameManager.instance.playerManager[0].Upgrade3();
                    playeUI.leftPlayer.buttonUpgrade03.interactable = false;
                    break;
            }
        }

        selectPlayer1 = true;
        eventSystem.SetSelectedGameObject(GameObject.Find("uppgrade01p2")); // Selecciona un nuevo boton
    }

    public void UpgradePlayer2(int upgrade)
    {
        if (!selectPlayer2)
        {
            switch (upgrade)
            {
                case 0:
                    GameManager.instance.playerManager[1].Upgrade1();
                    playeUI.rightPlayer.buttonUpgrade01.interactable = false;
                    break;

                case 1:
                    GameManager.instance.playerManager[1].Upgrade2();
                    playeUI.rightPlayer.buttonUpgrade02.interactable = false;
                    break;

                case 2:
                    GameManager.instance.playerManager[1].Upgrade3();
                    playeUI.rightPlayer.buttonUpgrade03.interactable = false;
                    break;
            }
        }

        selectPlayer2 = true;
    }
}
