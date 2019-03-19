using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBlock : MonoBehaviour {

    public bool disableBlock = false;
    public bool comprobarPosition = false;


    public int thisColumn;
    public int thisRow;

    public StatsBlock blocks;
    public Stats statsPlayer;

    PlayerMovement[] playerMovement;

    SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        //blockPosition = new int[2];
    }

    // Use this for initialization
    void Start () {
        InitPlayerMovement();
        //blocks = new Block;
        //m_SpriteRenderer = new SpriteRenderer;

        //blocks = GetComponent<Block>();
        //m_SpriteRenderer = GetComponent<SpriteRenderer>();

        //Debug.Log("asd");
    }
	
	// Update is called once per frame
	void Update () {

        //PlayerInThisBlock();

        if (comprobarPosition)
        {
            Debug.Log("Player: " + GetPlayerStats());
            Debug.Log("Player Skill " + GetPlayerStats().GetDamageSkill());
            Debug.Log("Get Player: " + GetPlayerStats().GetHealth());
        }
        //GetStatsPlayer().SetDamageBasicAttack(20);
        //Map.blocks[1, 2].PlayerInThisBlock();
        //Map.blocks[1, 2].GetStatsPlayer().TakeDamage();
    }

    void InitPlayerMovement()
    {
        playerMovement = new PlayerMovement[2];

        for (int i = 0; i < 2; i++)
        {
            playerMovement[i] = GameObject.Find("Player" + (i + 1)).GetComponent<PlayerMovement>();
            //Debug.Log(playerMovement[i].name);
        }
    }

    bool PlayerInThisBlock()
    {
        bool value = false;

        for (int i = 0; i < 2; i++)
        {
            bool test0 = playerMovement[i].playerColumn == thisColumn; // Column
            bool test1 = playerMovement[i].playerRow == thisRow; // Row

            if (test0 && test1)
            {
                value = true;
            }
        }

        return value;
    }

    Stats GetPlayerStats()
    {
        Stats playerStats = new Stats();

        for (int i = 0; i < 2; i++)
        {
            bool playerColumn = playerMovement[i].playerColumn == thisColumn; // Column
            bool playerRow = playerMovement[i].playerRow == thisRow; // Row

            if ((playerColumn && playerRow) && playerStats == null)
            {
                playerStats = /*GameObject.Find("Map").*/GetComponentInParent<Game>().playerStats[i]; 
            }
        }

        return playerStats;
    }
}


