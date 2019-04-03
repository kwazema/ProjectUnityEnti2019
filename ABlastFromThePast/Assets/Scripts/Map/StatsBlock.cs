using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBlock : MonoBehaviour {

    public bool disableBlock = false;
    public bool comprobarPosition = false;

    private int columnPosition;
    private int rowPosition;

    #region Function Set

    public void SetColumn(int value) { columnPosition = value; }
    public void SetRow(int value) { rowPosition = value; }

    #endregion

    public Stats statsPlayer;

    private PlayerMovement[] playerMovement;
    public SpriteRenderer spriteBlock;

    //private void Awake() { }

    private void Start () {
        InitPlayerMovement();

        spriteBlock = GetComponent<SpriteRenderer>();
    }

    private void Update () {

        if (comprobarPosition)
        {
            Debug.Log("Player: " + GetPlayerStatsBlock());
            Debug.Log("Player Skill " + GetPlayerStatsBlock().GetDamageSkill());
            Debug.Log("Get Player: " + GetPlayerStatsBlock().GetHealth());
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
            playerMovement[i] = GameObject.Find(BattleChoose.namePlayer[i]).GetComponent<PlayerMovement>();
            //playerMovement[i] = GameObject.Find("Player" + (i + 1)).GetComponent<PlayerMovement>();
            //playerMovement[i] = GameObject.Find("GAME.ARRAY STRING NAME)).GetComponent<PlayerMovement>();
            //Debug.Log(playerMovement[i].name);
        }
    }

    public bool IsPlayerInThisBlock()
    {
        bool value = false;

        for (int i = 0; i < 2; i++)
        {
            bool test0 = playerMovement[i].playerColumn == columnPosition; // Column
            bool test1 = playerMovement[i].playerRow == rowPosition; // Row

            if (test0 && test1)
            {
                value = true;
            }
        }

        return value;
    }

    public Stats GetPlayerStatsBlock()
    {
        Stats playerStats = new Stats();

        for (int i = 0; i < 2; i++)
        {
            bool playerColumn = playerMovement[i].playerColumn == columnPosition; // Column
            bool playerRow = playerMovement[i].playerRow == rowPosition; // Row

            if ((playerColumn && playerRow) && playerStats == null)
            {
                playerStats = /*GameObject.Find("Map").*/GetComponentInParent<Game>().playerStats[i]; 
            }
        }

        return playerStats;
    }
}


