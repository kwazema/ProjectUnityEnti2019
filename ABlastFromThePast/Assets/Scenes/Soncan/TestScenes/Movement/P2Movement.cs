using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class P2GameObjectArray
{
    public GameObject[] columns; // Array de Game Objects
}

public class P2Movement : MonoBehaviour
{
    private int nextBlock, nextLine, nextBlockOne, nextLineOne;
    private float posCorrectY = 1.5f;
    private float dirHorizontal, dirVertical;
    private float speedCharacter;
    private Vector2 v2PosActual;

    public P2GameObjectArray[] lines; // Array de Clases
    public Rigidbody2D rb;
    public Block[,] blocks;
    SpriteRenderer[,] m_SpriteRenderer;

    public float speed;
    bool isMoving = false, moveToSecondPoint = false, aLotOfMovements = false;
    Vector2[] v2NextPosition;
    Vector2 v2SavePosition;
    int nextPositionVector = 0, posV2Guardada = 0;
    int saveNextBlock = 0;
    int saveNextLine = 0;
    int lastNextBlock = 0;
    int lastNextLine = 0;


    bool displacedHorizontal = false;
    bool displacedVertical = false;
    bool ready = false;

    int Line, Column;

    // Use this for initialization
    void Start()
    {
        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();
        v2NextPosition = new Vector2[2];
        speed = 40f;
        //rb.isKinematic = true;
        //velocity = new Vector2(1.75f, 1.1f);


        //---------- Init Blocks ----------- //
        blocks = new Block[lines.Length, lines[0].columns.Length];
        m_SpriteRenderer = new SpriteRenderer[lines.Length, lines[0].columns.Length];


        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].columns.Length; j++)
            {
                blocks[i, j] = lines[i].columns[j].GetComponent<Block>();
                m_SpriteRenderer[i, j] = lines[i].columns[j].GetComponent<SpriteRenderer>();
            }
        }

        //---------- Init First Position ----------- //
        //  v2PosActual = blocks[0, 0].transform.position;
        // v2PosActual.y += posCorrectY;
        //transform.position = v2PosActual;
        v2NextPosition[0] = blocks[0, 0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        getAxisMovement();

        if (Input.GetKeyDown(KeyCode.H))
        {
            if(blocks[Line, Column].disableBlock)
            {
                blocks[Line, Column].disableBlock = false;
                m_SpriteRenderer[Line, Column].color = Color.green;
            }

            Line = Random.Range(0, 3);
            Column = Random.Range(0, 4);

            blocks[Line, Column].disableBlock = true;
            m_SpriteRenderer[Line, Column].color = Color.red;

           // Debug.Log("Input");
        }

        MovementCharacter();

    }

    void SetPositionBlocks(float horizontal, float vertical)
    {
        //Debug.Log("Horizontal: " + Input.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxisRaw("Vertical"));

        // Carga a la pos que me estoy moviendo
        if (moveToSecondPoint)
        {
            nextBlock = lastNextBlock;
            nextLine = lastNextLine;
            Debug.Log("Paso 4: Sobre escribe Segunda Pos");

        }


        //--------------- Move Right --------------- //
        if (horizontal > 0 && nextBlock < lines[nextLine].columns.Length - 1)
            if (!blocks[nextLine, nextBlock + 1].disableBlock)
                nextBlock += (int)horizontal;

        //--------------- Move Left --------------- //
        if (horizontal < 0 && nextBlock > 0)
            if (!blocks[nextLine, nextBlock - 1].disableBlock)
                nextBlock += (int)horizontal;

        //--------------- Move Up --------------- //
        if (vertical < 0 && nextLine < lines.Length - 1)
            if (!blocks[nextLine + 1, nextBlock].disableBlock)
                nextLine -= (int)vertical;

        //--------------- Move Down --------------- //
        if (vertical > 0 && nextLine > 0)
            if (!blocks[nextLine - 1, nextBlock].disableBlock)
                nextLine -= (int)vertical;


        //if (isMoving && ((int)vertical != 0 || (int)horizontal != 0))
        //{

        //    ready = true;
        //}
        //else
        //{
        //    ready = false;
        //}
    }

    private void FixedUpdate()
    {
    }

    void MovementCharacter()
    {
        //bool playerCanMove = false;
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, v2NextPosition[nextPositionVector], step);

        // Debug.Log("Line: " + nextLine + "// Block: " + nextBlock + "// Pos: " + nextPositionVector);

        Debug.Log(ready);

        if ((Vector2)transform.position != v2NextPosition[nextPositionVector])
        {
            isMoving = true;
        }
        else
        {

            isMoving = false; //En el blque

        }

        if (isMoving && ((int)dirVertical != 0 || (int)dirHorizontal != 0))
        {

            if (nextPositionVector == 0)
            {
                posV2Guardada = 1;
            }
            else
            {
                posV2Guardada = 0;
            }

            v2NextPosition[posV2Guardada] = blocks[nextLine, nextBlock].transform.position;

            moveToSecondPoint = true;
            Debug.Log("Paso 2 Guardar Segunda pos");


            //lastNextBlock = nextBlock;
            //lastNextLine = nextLine;
        }

        if (!isMoving)
            if (moveToSecondPoint)
                if (nextPositionVector == 0)
                {
                    nextPositionVector = 1;
                    moveToSecondPoint = false;
                    //lastNextBlock = nextBlock;
                    //lastNextLine = nextLine;
                    Debug.Log("Paso 3 Mueve Segunda pos");

                }
                else
                {
                    nextPositionVector = 0;
                    moveToSecondPoint = false;
                    //lastNextBlock = nextBlock;
                    //lastNextLine = nextLine;
                    Debug.Log("Paso 3 Mueve Segunda pos");

                }


        // Antes de moverse, guarda la pos donde se esta desplazando.
        if (!isMoving)
        {

            v2NextPosition[posV2Guardada] = blocks[nextLine, nextBlock].transform.position;

            lastNextBlock = nextBlock;
            lastNextLine = nextLine;
            Debug.Log("Paso 1 Guardar");
        }
    }

    void getAxisMovement()
    {
        bool posConfirmed = false; // Si presionas dos taclas a la vez el player no ira en horizontal.

        //Input.GetJoystickNames()[i]
        //--------------- Direction Horizontal --------------- //
        dirHorizontal = Input.GetAxisRaw("Mouse X");
        //dirHorizontal = 0;
        //dirVertical = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            if (!posConfirmed)
            {
                dirHorizontal = -1f;
                posConfirmed = true;
            }

        if (Input.GetKeyDown(KeyCode.RightArrow))
            if (!posConfirmed)
            {
                dirHorizontal = 1f;
                posConfirmed = true;
            }

        //--------------- Direction Vertical --------------- //
        dirVertical = Input.GetAxisRaw("Mouse Y");

        if (Input.GetKeyDown(KeyCode.UpArrow))
            if (!posConfirmed)
            {
                dirVertical = 1f;
                posConfirmed = true;
            }

        if (Input.GetKeyDown(KeyCode.DownArrow))
            if (!posConfirmed)
            {
                dirVertical = -1;
                posConfirmed = true;
            }

        //Debug.Log("dirHorizontal" + dirHorizontal + "dirVertical" + dirVertical);

        //--------------- Direction Horizontal --------------- //
        if (dirHorizontal != 0)
        {
            if (displacedHorizontal == false)
            {
                SetPositionBlocks(dirHorizontal, 0);
                displacedHorizontal = true;
            }
        }
        if (dirHorizontal == 0)
        {
            displacedHorizontal = false;
        }

        //--------------- Direction Vertical --------------- //
        if (dirVertical != 0)
        {
            if (displacedVertical == false)
            {
                SetPositionBlocks(0, dirVertical);
                displacedVertical = true;
            }
        }
        if (dirVertical == 0)
        {
            displacedVertical = false;
        }
    }
}