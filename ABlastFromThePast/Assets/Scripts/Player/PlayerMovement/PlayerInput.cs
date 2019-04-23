using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XInputDotNetPure; // Required in C#

public class PlayerInput : MonoBehaviour {

    [SerializeField] public enum EnumPlayer { Player1, Player2 }
    [SerializeField] public EnumPlayer enumPlayer;
    
    private float dirHorizontal, dirVertical;

    private bool displacedHorizontal = false;
    private bool displacedVertical = false;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Use this for initialization
    void Start ()
    {
        //playerMovement = gameObject.GetComponent<Player1Movement>(); // Diferencia?
    }

    // Update is called once per frame
    void Update ()
    {
        getAxisMovement();

        IsInput();
    }

    //----------- Functions: Movement -----------//
    public bool IsInput()
    {
        if (((int)dirVertical != 0) || ((int)dirHorizontal != 0))
            return true;
        else
            return false;
    }

    void getAxisMovement()
    {
        InputMovement(enumPlayer);

        JostickController();

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

    void JostickController()
    {
        string[] names = Input.GetJoystickNames();

        //Debug.Log("Josticks: " + names.Length);

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
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

    void InputKeyPlayer1()
    {
        //--------------- Direction Horizontal --------------- //
        dirHorizontal = Input.GetAxisRaw("Joy0X");

        if (Input.GetKeyDown(KeyCode.A))
            dirHorizontal = -1f;

        if (Input.GetKeyDown(KeyCode.D))
            dirHorizontal = 1f;

        //--------------- Direction Vertical --------------- //
        if (dirHorizontal == 0)
        {
            dirVertical = Input.GetAxisRaw("Joy0Y");

            if (Input.GetKeyDown(KeyCode.W))
                dirVertical = 1f;

            if (Input.GetKeyDown(KeyCode.S))
                dirVertical = -1f;
        }
    }

    void InputKeyPlayer2()
    {
        //--------------- Direction Horizontal --------------- //
        dirHorizontal = Input.GetAxisRaw("Joy1X");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            dirHorizontal = -1f;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            dirHorizontal = 1f;

        //--------------- Direction Vertical --------------- //
        if (dirHorizontal == 0)
        {
            dirVertical = Input.GetAxisRaw("Joy1Y");

            if (Input.GetKeyDown(KeyCode.UpArrow))
                dirVertical = 1f;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                dirVertical = -1f;
        }
    }
}