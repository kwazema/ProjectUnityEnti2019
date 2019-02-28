using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//[System.Serializable]
//public class GameObjectArray
//{
//    public GameObject[] columns; // Array de Game Objects
//}

public class Movement : MonoBehaviour
{
    private int nextColumn, nextRow, nextBlockOne, nextLineOne;
    private float posCorrectY = 1.5f;
    private float dirHorizontal, dirVertical;
    private float speedCharacter;
    private Vector2 v2PosActual;

    public GameObject blockPrefab;
    //public GameObjectArray[] lines; // Array de Clases
    public Rigidbody2D rb;

    public Block[,] blocks;
    SpriteRenderer[,] m_SpriteRenderer;

    private float speed;
    bool isMoving = false, moveToSecondPoint = false;
    Vector2[] v2NextPosition;
    Vector2 v2SavePosition;
    int nextPositionVector = 0, posV2Guardada = 0;
    int lastNextColumn = 0;
    int lastNextRow = 0;

    int columnLenth = 4;
    int rowLenth = 3;


    bool displacedHorizontal = false;
    bool displacedVertical = false;
    bool ready = false;


    //aLotOfMovements = false;
    //int saveNextBlock = 0;
    //int saveNextLine = 0;
    // Use this for initialization
    void Start()
    {
        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();
        v2NextPosition = new Vector2[2];
        speed = 40.0f;

        //rb.isKinematic = true;
        //velocity = new Vector2(1.75f, 1.1f);


        //---------- Init Blocks ----------- //
        //blocks = new Block[lines.Length, lines[0].columns.Length];
        blocks = InstantiateBlocks(columnLenth, rowLenth, new Vector2Int(-15, 3), 3, 3, 1);
        m_SpriteRenderer = new SpriteRenderer[columnLenth, rowLenth];


        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                blocks[i, j] = blocks[i, j].GetComponent<Block>();
                m_SpriteRenderer[i, j] = blocks[i, j].GetComponent<SpriteRenderer>();
            }
        }

        //---------- Init First Position ----------- //
        //  v2PosActual = blocks[0, 0].transform.position;
        // v2PosActual.y += posCorrectY;
        //transform.position = v2PosActual;
        //v2NextPosition[0] = blocks[0, 0].transform.position;
    }

    Block[,] InstantiateBlocks(int column, int row, Vector2Int offset, float width, float height, float margin)
    {
        Block[,] blocks = new Block[column, row];

        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                blocks[i, j] = Instantiate(blockPrefab).GetComponent<Block>();
                blocks[i, j].transform.position = offset + new Vector2(i * (width + margin), j * -(height + margin));

                //blocks[i, j] = blocks[i, j].GetComponent<Block>();
                //m_SpriteRenderer[i, j] = blocks[i, j].GetComponent<SpriteRenderer>();
            }
        }
        return blocks;
    }

    void destroyBlock(int columnLenth, int rowLenth)
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H");
            int column = Random.Range(0, columnLenth);
            int row = Random.Range(0, rowLenth);

            if (blocks[column, row].disableBlock)
            {
                blocks[column, row].disableBlock = false;
                m_SpriteRenderer[column, row].color = Color.green;
            }

            blocks[column, row].disableBlock = true;
            m_SpriteRenderer[column, row].color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        getAxisMovement();

        destroyBlock(columnLenth, rowLenth);

        MovementCharacter();

    }

    void SetPositionBlocks(float horizontal, float vertical)
    {
        //Debug.Log("Horizontal: " + Input.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxisRaw("Vertical"));

        // Carga a la pos que me estoy moviendo
        if (moveToSecondPoint)
        {
            nextColumn = lastNextColumn;
            nextRow = lastNextRow;
            Debug.Log("Paso 4: Sobre escribe Segunda Pos");

        }


        ////--------------- Move Right --------------- //
        //if (horizontal > 0 && nextColumn < blocks.[nextRow].Length - 1)
        //    if (!blocks[nextRow, nextColumn + 1].disableBlock)
        //        nextColumn += (int)horizontal;

        ////--------------- Move Left --------------- //
        //if (horizontal < 0 && nextColumn > 0)
        //    if (!blocks[nextRow, nextColumn - 1].disableBlock)
        //        nextColumn += (int)horizontal;

        ////--------------- Move Up --------------- //
        //if (vertical < 0 && nextRow < lines.Length - 1)
        //    if (!blocks[nextRow + 1, nextColumn].disableBlock)
        //        nextRow -= (int)vertical;

        ////--------------- Move Down --------------- //
        //if (vertical > 0 && nextRow > 0)
        //    if (!blocks[nextRow - 1, nextColumn].disableBlock)
        //        nextRow -= (int)vertical;


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

            v2NextPosition[posV2Guardada] = blocks[nextColumn, nextRow].transform.position;

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

            v2NextPosition[posV2Guardada] = blocks[nextColumn, nextRow].transform.position;

            lastNextColumn = nextColumn;
            lastNextRow = nextRow;
            Debug.Log("Paso 1 Guardar");
        }

        
    }

    void getAxisMovement()
    {
        bool posConfirmed = false; // Si presionas dos taclas a la vez el player no ira en horizontal.


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

        if (Input.GetAxisRaw("Mouse Y") > 0f)
            
            Debug.Log(Input.GetJoystickNames() + " is moved");
        
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

