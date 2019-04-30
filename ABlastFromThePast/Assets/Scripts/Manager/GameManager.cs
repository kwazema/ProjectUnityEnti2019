using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerManager[] playerStats;
    public GameObject[] objectPlayer;
    public static GameManager instance = null;

    [SerializeField]
    public PlayerManager[] playerManager;
    public PlayeUI playeUI;

    public int[] playerChoise;

    public int roundCur = 0, roundMax = 3;
    public float timeToStartDefault, timeRoundMaxDefault;
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

        //public Round (float timeToStart, float timeRoundMax)
        //{
        //    timeToStartMax = timeToStart;
        //    timeToStartCur = 0;

        //    timeMax = timeRoundMax;
        //    timeCur = 0;

        //    roundMax = 0;
        //    roundCur = 0;

        //    roundsWinPlayer1 = 0;
        //    roundsWinPlayer2 = 0;
        //}

    } public Round[] round;

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
        round[0].timeCur = 5;
        round[1].timeCur = 10;

        //StartCoroutine(StartRound(roundCur));
        StartCoroutine(TimeRound(roundCur));
    }

    IEnumerator StartRound(int num)
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
                // imprimir habilidades y empezar con el StartRound si es necesario
                yield return null;
            }
        }
    }

    IEnumerator TimeRound(int num)
    {
        while (round[num].timeCur >= 0)
        {
            // Imprimit tiempo pantalla
            round[num].timeCur -= Time.deltaTime;
            //yield return new WaitForFixedUpdate();
            yield return null;
        }

        roundCur++;
        StartCoroutine(ChoiseSkills(0));
        yield return null;
    }

    IEnumerator ChoiseSkills(int num)
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
        StartCoroutine(TimeRound(1));
        yield return null;
    }

    void SetSkills()
    {
        // skills 1 / 2 / 3



    }
}
