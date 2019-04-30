using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerManager[] playerStats;
    public GameObject[] objectPlayer;
    public static GameManager instance = null;

    [SerializeField]
    public PlayerManager[] playerManager;

    public int[] playerChoise;

    public int roundCur, roundMax = 5;
    public struct Round
    {
        public float timeToStartMax;
        public float timeToStartCur;

        public float timeMax;
        public float timeCur;

        public int roundMax;
        public int roundCur;

        public int roundsWinPlayer1;
        public int roundsWinPlayer2;

        public Round (float timeToStart, float timeRoundMax)
        {
            timeToStartMax = timeToStart;
            timeToStartCur = 0;

            timeMax = timeRoundMax;
            timeCur = 0;

            roundMax = 0;
            roundCur = 0;

            roundsWinPlayer1 = 0;
            roundsWinPlayer2 = 0;
    }

    } Round[] round;

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

            if (playerStats[i].whichIsThisPlayer == 0)
                playerStats[0].transform.rotation = new Quaternion(0, 0, 0, 0);
            else
                playerStats[1].transform.rotation = new Quaternion(0, 180, 0, 0);

            GameObject.Find(playerStats[i].name).GetComponent<PlayerMovement>().whichIs = i;
            
            // Class Stats
            playerStats[i].thisPlayerIs = (PlayerManager.ThisPlayerIs)i; 

            // Class Input Attack
            GameObject.Find(playerStats[i].name).GetComponent<PlayerAttackInput>().enumPlayer = (PlayerAttackInput.EnumPlayer)i;

            // Class Input Movement
            GameObject.Find(playerStats[i].name).GetComponent<PlayerInput>().enumPlayer = (PlayerInput.EnumPlayer)i;

        }
    }

    public void StartBattle()
    {
        round = new Round[roundMax];

        //StartCoroutine(TimeRound(roundCur));
        StartCoroutine(StartRound(roundCur));
    }

    protected IEnumerator TimeRound(int num)
    {
        while (true)
        {
            if (round[num].timeCur <= round[num].timeMax)
            {
               // Imprimit tiempo pantalla
                yield return new WaitForSeconds(1);
            }
            else
            {
                // imprimir habilidades y empezar con el StartRound si es necesario
                yield return null;
            }
        }
    }

    protected IEnumerator StartRound(int num)
    {
        while (true)
        {
            if (round[num].timeToStartCur <= round[num].timeToStartCur)
            {
                // Imprimit tiempo pantalla
                yield return new WaitForSeconds(1);
            }
            else
            {
                // TimeBattle
                // Siguiente ronda
                roundCur++;
                yield return null;
            }
        }
    }
}
