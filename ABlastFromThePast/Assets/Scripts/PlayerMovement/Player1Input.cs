using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Input : MonoBehaviour {

    private float dirHorizontal, dirVertical;

    private bool displacedHorizontal = false;
    private bool displacedVertical = false;

    [System.NonSerialized]
    public bool isInput = false;

    private Player1Movement playerMovement;

    [SerializeField] enum EnumPlayer { Player1, Player2}
    [SerializeField] EnumPlayer player;

    // Use this for initialization
    void Start ()
    {
        //playerMovement = gameObject.GetComponent<Player1Movement>(); // Diferencia?
        playerMovement = GetComponent<Player1Movement>();
    }

    // Update is called once per frame
    void Update ()
    {
        getAxisMovement();

        ActionMove();
    }

    //----------- Functions: Movement -----------//
    void ActionMove()
    {
        if (((int)dirVertical != 0) || ((int)dirHorizontal != 0))
            isInput = true;
        else
            isInput = false;
    }

    void getAxisMovement()
    {
        InputMovement(player);

        GetController();

        SetController();
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

    void GetController()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Input.GetAxis("Joy" + 0 + "Y") > 0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 0 + " TOP");
            }

            if (Input.GetAxis("Joy" + 1 + "Y") > 0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 1 + " TOP");
            }

            //----------------------------------------//

            if (Input.GetAxis("Joy" + 0 + "Y") < -0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 0 + " Down");
            }

            if (Input.GetAxis("Joy" + 1 + "Y") < -0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 1 + " Down");
            }

            //----------------------------------------//

            if (Input.GetAxis("Joy" + 0 + "X") > 0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 0 + " Right");
            }

            if (Input.GetAxis("Joy" + 1 + "X") > 0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 1 + " Right");
            }

            //----------------------------------------//

            if (Input.GetAxis("Joy" + 0 + "X") < -0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 0 + " Left");
            }

            if (Input.GetAxis("Joy" + 1 + "X") < -0f)
            {
                Debug.Log(Input.GetJoystickNames()[i] + " is moved: " + 1 + " Left");
            }
        }
    }

    void InputMovement(EnumPlayer player)
    {
        //bool posConfirmed = false;

        //Arreglar el poblema si pulsas dos teclas en el mismo frame.
        switch (player)
        {
        case EnumPlayer.Player1:
            InputKeyPlayer1();
            break;

        case EnumPlayer.Player2:
            InputKeyPlayer2();
            break;
        }
    }


    //// Se comprueba 
    //bool isInputPress(bool posConfirmed)
    //{

    //    if (dirVertical != 0 && dirHorizontal)

    //        return false;
    //}


    void InputKeyPlayer1()
    {
        //--------------- Direction Horizontal --------------- //
        dirHorizontal = Input.GetAxisRaw("Mouse X");

        if (Input.GetKeyDown(KeyCode.A))
            dirHorizontal = -1f;

        if (Input.GetKeyDown(KeyCode.D))
            dirHorizontal = 1f;

        //--------------- Direction Vertical --------------- //
        dirVertical = Input.GetAxisRaw("Mouse Y");

        if (Input.GetKeyDown(KeyCode.W))
            dirVertical = 1f;

        if (Input.GetKeyDown(KeyCode.S))
            dirVertical = -1f;
    }

    void InputKeyPlayer2()
    {
        //--------------- Direction Horizontal --------------- //
        dirHorizontal = Input.GetAxisRaw("Mouse X");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            dirHorizontal = -1f;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            dirHorizontal = 1f;

        //--------------- Direction Vertical --------------- //
        dirVertical = Input.GetAxisRaw("Mouse Y");

        if (Input.GetKeyDown(KeyCode.UpArrow))
            dirVertical = 1f;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            dirVertical = -1f;
    }
}