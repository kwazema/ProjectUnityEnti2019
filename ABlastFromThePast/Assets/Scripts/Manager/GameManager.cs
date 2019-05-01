using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Round
{
    public float timeToStartMax = 5;
    public float timeToStartCur;

    public float timeSet = 15;
    public float timeCur;

    //public int roundMax;
    //public int roundCur;

    public int roundsWinPlayer1;
    public int roundsWinPlayer2;

    public int roundCur = 0;
    public int roundMax = 3;
}

public class GameManager : MonoBehaviour {

    public PlayerManager[] playerStats;
    public GameObject[] objectPlayer;
    public static GameManager instance = null;

    public PlayerManager[] playerManager;
    public PlayeUI playeUI;

    public int[] playerChoise;
    public Round round;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    //void Start () { }

    //void Update() { }

    public void InitPlayers()
    {
        playerStats = new PlayerManager[2];

        for (int i = 0; i < playerStats.Length; i++)
        {
            Debug.Log("Player " + i + " " + playerChoise[i]);

            switch (playerChoise[i])
            {
                case 0: playerStats[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<BrayanStats>();  break;
                case 1: playerStats[i] = Instantiate(objectPlayer[playerChoise[i]]).GetComponent<ScepterStats>(); break;
            }
            playerStats[i].name = (playerStats[i].namePlayer + (i + 1));

            playerStats[i].whichIsThisPlayer = i;


            GameObject.Find(playerStats[i].name).GetComponent<PlayerMovement>().whichIs = i;
            
            // Class Manager
            playerStats[i].thisPlayerIs = (PlayerManager.ThisPlayerIs)i;

            if (playerStats[i].thisPlayerIs == PlayerManager.ThisPlayerIs.Player1)
            {
                playerStats[0].transform.rotation = new Quaternion(0, 0, 0, 0);
                playerStats[0].gameObject.layer = 11;
            }
            else
            {
                playerStats[1].transform.rotation = new Quaternion(0, 180, 0, 0);
                playerStats[1].gameObject.layer = 12;
            }


            // Class Input Attack
            GameObject.Find(playerStats[i].name).GetComponent<PlayerAttackInput>().enumPlayer = (PlayerAttackInput.EnumPlayer)i;

            // Class Input Movement
            GameObject.Find(playerStats[i].name).GetComponent<PlayerInput>().enumPlayer = (PlayerInput.EnumPlayer)i;

        }
    }

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
        round.timeCur = round.timeSet;

        while (round.timeCur >= 0)
        {
            //Comprobar si algun persnaje muere si el que gane se lleva round win
            // 

            round.timeCur -= Time.deltaTime;
            yield return null;
        }

        round.timeCur = round.timeSet;

        if (playerStats[0].GetHealth() > playerStats[1].GetHealth())
        {
            round.roundsWinPlayer1++;
        }
        else if (playerStats[0].GetHealth() < playerStats[1].GetHealth())
        {
            round.roundsWinPlayer2++;
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
            }
        }
        else
        {
            round.roundCur++;
            StartCoroutine(ChoiseSkills());
        }

        //yield return null;
    }

    IEnumerator ChoiseSkills()
    {
        playeUI.skills.SetActive(true);
        bool choised = false;

        while (!choised)
        {
            // Imprimit tiempo pantalla
            if (Input.GetKeyDown(KeyCode.Space))
            {
                choised = true;
                Debug.Log("Espacio");
            }

            yield return null;
        }

        playeUI.skills.SetActive(false);
        StartCoroutine(TimeRound());
        //yield return null;
    }

    void SetSkills()
    {
        // skills 1 / 2 / 3



    }
}
