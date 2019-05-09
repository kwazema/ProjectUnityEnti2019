using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //public BrayanStats playerStats;
    public GameManager game;
    PlayerMovement playerMove;

    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer player;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        game = GameObject.Find("Map").GetComponent<GameManager>();
    }

    // Use this for initialization
    void Start () {

        //playerStats = GetComponent<BrayanStats>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Health: " + playerStats.GetHealth());
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        //GetDamage();
        Debug.Log("Hit " + col.gameObject.name);

        if (EnumPlayer.Player1 == player)
        {
            game.playerManager[0].TakeDamage(game.playerManager[1].GetDamageBasicAttack());
        }
        else
        {
            game.playerManager[1].TakeDamage(game.playerManager[0].GetDamageBasicAttack());
        }
        //playerStats.TakeDamage(20);
    }
}
