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

    public PlayerManager statsPlayer;

    private PlayerMovement[] playerMovement;
    public SpriteRenderer spriteBlock;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteBlock = GetComponent<SpriteRenderer>();
        InitPlayerMovement();

    }

    private void Start()
    {
    }

    private void Update()
    {
        if (comprobarPosition)
        {
            Debug.Log("Player: " + GetPlayerStatsBlock());
            Debug.Log("Player Skill " + GetPlayerStatsBlock().GetDamageSkill());
            Debug.Log("Get Player: " + GetPlayerStatsBlock().GetHealth());
        }
    }

    public void InitPlayerMovement()
    {
        playerMovement = new PlayerMovement[2];

        for (int i = 0; i < playerMovement.Length; i++) {
            playerMovement[i] = gameManager.objectPlayer[gameManager.playerChoise[i]].GetComponent<PlayerMovement>();
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

    public PlayerManager GetPlayerStatsBlock()
    {
        PlayerManager playerStats = new PlayerManager();

        for (int i = 0; i < 2; i++)
        {
            bool playerColumn = playerMovement[i].playerColumn == columnPosition; // Column
            bool playerRow = playerMovement[i].playerRow == rowPosition; // Row

            if ((playerColumn && playerRow) && playerStats == null)
            {
                playerStats = /*GameObject.Find("Map").*/GetComponentInParent<GameManager>().playerStats[i]; 
            }
        }
        return playerStats;
    }


}


