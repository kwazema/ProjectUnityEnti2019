using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Input : MonoBehaviour {

    private float dirHorizontal, dirVertical;

    bool displacedHorizontal = false;
    bool displacedVertical = false;

    private Player1Movement playerMovement;

    public bool actionMove = false;



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

        if (((int)dirVertical != 0) || ((int)dirHorizontal != 0))
        {
            actionMove = true;
        }
        else
        {
            actionMove = false;

        }

    }


    void getAxisMovement()
    {
        // Si presionas dos taclas a la vez el player no ira en horizontal.
        bool posConfirmed = false; 

        //--------------- Direction Horizontal --------------- //
        dirHorizontal = Input.GetAxisRaw("Mouse X");


        if (Input.GetKeyDown(KeyCode.A))
            if (!posConfirmed)
            {
                dirHorizontal = -1f;
                posConfirmed = true;
            }

        if (Input.GetKeyDown(KeyCode.D))
            if (!posConfirmed)
            {
                dirHorizontal = 1f;
                posConfirmed = true;
            }

        //--------------- Direction Vertical --------------- //
        dirVertical = Input.GetAxisRaw("Mouse Y");

        if (Input.GetKeyDown(KeyCode.W))
            if (!posConfirmed)
            {
                dirVertical = 1f;
                posConfirmed = true;
            }

        if (Input.GetKeyDown(KeyCode.S))
            if (!posConfirmed)
            {
                dirVertical = -1;
                posConfirmed = true;
            }

        //if (Input.GetAxisRaw("Mouse Y") > 0f)

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
}
