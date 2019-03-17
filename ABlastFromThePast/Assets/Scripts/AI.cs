using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer player;

    private float dirHorizontal, dirVertical;
    private bool displacedHorizontal = false;
    private bool displacedVertical = false;

    private AIMovement playerMovement;
    private Map map;

    private void Awake()
    {
        //map = GameObject.Find("Map").GetComponent<Map>();
        map = FindObjectOfType<Map>();
    }

    // Use this for initialization
    void Start()
    {
        //playerMovement = gameObject.GetComponent<Player1Movement>(); // Diferencia?
        playerMovement = GetComponent<AIMovement>();
        player = EnumPlayer.Player2;
        StartCoroutine(Horizontal());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("horizontal: " + dirHorizontal);


    }

    void statLevel(int level)
    {

    }

    IEnumerator Horizontal()
    {
        while (true)
        { // loops forever...
            if (GetMove())
            {
                Debug.Log("Left");
                dirHorizontal = -1;
                playerMovement.SetPositionBlocks(-1, 0);

            }
            else
            {
                //playerMovement.SetPositionBlocks(1, 0);
                //dirHorizontal = -1;
            }

            //SetController();

            dirHorizontal = 0;
            //dirVertical = 0;
            yield return new WaitForSeconds(1);
        }
    }

    bool GetMove()
    {
        bool value = true;

        if (playerMovement.playerColumn == map.columnLenth / 2)
        {
            value = true;
        }

        if (playerMovement.playerColumn > map.columnLenth - 1)
        {
            value = false;
        }

        return value;
    }

    void set()
    {
        playerMovement.SetPositionBlocks((int)dirHorizontal, 0);
    }

    void SetController()
    {
        //----------- Horizontal Controller ------------ //
        if (dirHorizontal != 0)
        {
            if (displacedHorizontal == false)
            {
                playerMovement.SetPositionBlocks((int)dirHorizontal, 0);
                displacedHorizontal = true;
            }
        }
        if (dirHorizontal == 0)
        {
            displacedHorizontal = false;
        }

        //----------- Vertical Controller ------------ //
        if (dirVertical != 0)
        {
            if (displacedVertical == false)
            {
                playerMovement.SetPositionBlocks(0, (int)dirVertical);
                displacedVertical = true;
            }
        }
        if (dirVertical == 0)
        {
            displacedVertical = false;
        }
    }

   

    //void InputMovement(EnumPlayer player)
    //{
    //    //bool posConfirmed = false;

    //    //Arreglar el poblema si pulsas dos teclas en el mismo frame.
    //    switch (player)
    //    {
    //        case EnumPlayer.Player1:
    //            InputKeyPlayer1();
    //            break;

    //        case EnumPlayer.Player2:
    //            InputKeyPlayer2();
    //            break;
    //    }
    //}

    //void InputKeyPlayer1()
    //{
    //    //--------------- Direction Horizontal --------------- //
    //    dirHorizontal = Input.GetAxisRaw("Joy0X");

    //    if (Input.GetKeyDown(KeyCode.A))
    //        dirHorizontal = -1f;

    //    if (Input.GetKeyDown(KeyCode.D))
    //        dirHorizontal = 1f;

    //    //--------------- Direction Vertical --------------- //
    //    if (dirHorizontal == 0)
    //    {
    //        dirVertical = Input.GetAxisRaw("Joy0Y");

    //        if (Input.GetKeyDown(KeyCode.W))
    //            dirVertical = 1f;

    //        if (Input.GetKeyDown(KeyCode.S))
    //            dirVertical = -1f;
    //    }
    //}

    //void InputKeyPlayer2()
    //{
    //    //--------------- Direction Horizontal --------------- //
    //    dirHorizontal = Input.GetAxisRaw("Joy1X");

    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //        dirHorizontal = -1f;

    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //        dirHorizontal = 1f;

    //    //--------------- Direction Vertical --------------- //
    //    if (dirHorizontal == 0)
    //    {
    //        dirVertical = Input.GetAxisRaw("Joy1Y");

    //        if (Input.GetKeyDown(KeyCode.UpArrow))
    //            dirVertical = 1f;

    //        if (Input.GetKeyDown(KeyCode.DownArrow))
    //            dirVertical = -1f;
    //    }
    //}
}
