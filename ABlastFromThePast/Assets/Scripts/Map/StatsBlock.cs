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

    //public PlayerManager playerManager;

    private PlayerMovement[] playerMovement;
    public SpriteRenderer spriteBlock;
    private GameManager gameManager;
    public Animator anim;

    public LayerMask whatIsPlayer;
    public Vector2 size;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteBlock = GetComponent<SpriteRenderer>();
        //InitPlayerMovement();
    }

    private void Start() { }
    private void Update() { }

    //{
    //    playerMovement = new PlayerMovement[2];

    //    for (int i = 0; i < playerMovement.Length; i++)
    //        playerMovement[i] = gameManager.objectPlayer[gameManager.playerChoise[i]].GetComponent<PlayerMovement>();
    //}

    #region void IsPlayerInThisBlock()
    //public bool IsPlayerInThisBlock(int whatIsThisPlayer)
    //{
    //    bool value = false;

    //    Collider2D[] playerBlock = Physics2D.OverlapBoxAll(transform.position, size, whatIsPlayer);

    //    for (int i = 0; i < playerBlock.Length; i++) {
    //        PlayerManager player = playerBlock[i].GetComponent<PlayerManager>();

    //        if (player != null)
    //         if (player.whichIsThisPlayer != whatIsThisPlayer)
    //             return  true;
    //        else
    //         return  false;
    //    }

    //    return false;
    //}
    #endregion

    public PlayerManager GetPlayerStatsBlock(int whatIsThisPlayer)
    {
        Collider2D[] playerBlock = Physics2D.OverlapBoxAll(transform.position, size, whatIsPlayer);
        PlayerManager playerManager = null;
        
        for (int i = 0; i < playerBlock.Length; i++)
        {
            PlayerManager player = playerBlock[i].GetComponent<PlayerManager>();

            if (player != null)
                if (player.whichIsThisPlayer != whatIsThisPlayer)
                    playerManager = playerBlock[i].GetComponent<PlayerManager>();
        }

        return playerManager;
    }

    //public bool IsPlayerInThisBlock()
    //{
    //    bool value = false;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        bool test0 = playerMovement[i].playerColumn == columnPosition; // Column
    //        bool test1 = playerMovement[i].playerRow == rowPosition; // Row

    //        if (test0 && test1)
    //        {
    //            value = true;
    //        }
    //    }

    //    return value;
    //}

    //public PlayerManager GetPlayerStatsBlock()
    //{
    //    PlayerManager playerStats = new PlayerManager();

    //    for (int i = 0; i < 2; i++)
    //    {
    //        bool playerColumn = playerMovement[i].playerColumn == columnPosition; // Column
    //        bool playerRow = playerMovement[i].playerRow == rowPosition; // Row

    //        if ((playerColumn && playerRow) && playerStats == null)
    //        {
    //            playerStats = /*GameObject.Find("Map").*/GetComponentInParent<GameManager>().playerStats[i]; 
    //        }
    //    }

    //    return playerStats;
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();

    //    if (collision)
    //    {
    //        Debug.Log("Enter: " + collision.name);
    //    }
    //}
}


