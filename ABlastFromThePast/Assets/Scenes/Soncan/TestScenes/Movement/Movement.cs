using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectArray
{
    public GameObject[] columns; // Array de Game Objects
}

public class Movement : MonoBehaviour
{
    private int nextBlock, nextLine, nextBlockOne, nextLineOne;
    private float posCorrectY = 1.5f;
    private float dirHorizontal, dirVertical;
    private float speedCharacter;
    private Vector2 v2PosActual;

    public GameObjectArray[] lines; // Array de Clases
    public Rigidbody2D rb;
    public Block[,] blocks;
    SpriteRenderer[,] m_SpriteRenderer;

    private float speed = 5.0f;
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

    int Line, Column;

    // Use this for initialization
    void Start()
    {
        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();
        v2NextPosition = new Vector2[2];
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
    }

    void SetPositionBlocks(float horizontal, float vertical)
    {
        //Debug.Log("Horizontal: " + Input.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxisRaw("Vertical"));

        if (moveToSecondPoint)
        {
            nextBlock = lastNextBlock;
            nextLine = lastNextLine;
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



        if (isMoving && ((int)vertical != 0 || (int)horizontal != 0))
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
        }

        if (!isMoving )
        {
            v2NextPosition[posV2Guardada] = blocks[nextLine, nextBlock].transform.position;
            lastNextBlock = nextBlock;
            lastNextLine = nextLine;
        }

        // Y la segunfa vuelva a ser la primera ()

        // Si me estoy moviendo guardarme la posicion siguiente en v2NextPosition

        // Si aun me estoy moviendo y vuelvo a clicar resetear la ultima posicion y guardar a v2NextPosition





        // Se guardan a los bloques que se esta moviendo.


        //Debug.Log("Pos Guardada");
        //Debug.Log("Block Actual :" + nextBlock + "// Linea Actual :" + nextLine);




        //if (isMoving && ((int)vertical != 0 || (int)horizontal != 0))
        //{
        //    moveToSecondPoint = true;

        //    saveNextBlock = lastNextBlock;
        //    saveNextLine = lastNextLine;



        //    if (horizontal > 0 && saveNextBlock < lines[saveNextLine].columns.Length - 1)
        //        if (!blocks[saveNextLine, saveNextBlock + 1].disableBlock)
        //            saveNextBlock += (int)horizontal;

        //    //--------------- Move Left --------------- //
        //    if (horizontal < 0 && saveNextBlock > 0)
        //        if (!blocks[saveNextLine, saveNextBlock - 1].disableBlock)
        //            saveNextBlock += (int)horizontal;

        //    //--------------- Move Up --------------- //
        //    if (vertical < 0 && saveNextLine < lines.Length - 1)
        //        if (!blocks[saveNextLine + 1, saveNextBlock].disableBlock)
        //            saveNextLine -= (int)vertical;

        //    //--------------- Move Down --------------- //
        //    if (vertical > 0 && saveNextLine > 0)
        //        if (!blocks[saveNextLine - 1, saveNextBlock].disableBlock)
        //            saveNextLine -= (int)vertical;

        //    //if (posV2 == 1 && !isMoving)
        //        v2NextPosition[posV2] = blocks[saveNextLine, saveNextBlock].transform.position; //(AQUI ESTAN LOS PROBLEMAS!)

        //    //    Debug.Log("Block Last :" + saveNextBlock + "// Linea Last :" + saveNextLine);

        //    //    //Guardar en la ultima posicion que has estado

        //    //    //Debug.Log(v2NextPosition[1]);
        //    // }
        //}

        //if (direction == Direction.top && nextLine > 0)
        //{
        //    if (!blocks[nextLine -1, nextBlock].disableBlock)
        //        nextLine--;
        //}

        //transform.position = new Vector2
        //    (
        //        blocks[nextLine, nextBlock].transform.position.x,
        //        blocks[nextLine, nextBlock].transform.position.y + posCorrectY
        //    );

        //Debug.Log("Line[" + nextLine + "] // Block[" + nextBlock + "]");
        //Debug.Log("Disables: " + blocks[nextLine, nextBlock].disableBlock);
        // Debug.Log("Disables++: " + bloques[nextLine, nextBlock + 1].disableBlock);
    }

    private void FixedUpdate()
    {
        MovementCharacter();
    }

    void MovementCharacter()
    {
        //bool playerCanMove = false;
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, v2NextPosition[nextPositionVector], step);

        Debug.Log("Line: " + nextLine + "// Block: " + nextBlock + "// Pos: " + nextPositionVector);

        if ((Vector2)transform.position != v2NextPosition[nextPositionVector])
        {
            isMoving = true;
        }
        else
        {

            isMoving = false; //En el blque

        }

        if (!isMoving)
            if (moveToSecondPoint)
                if (nextPositionVector == 0)
                {
                    nextPositionVector = 1;
                    moveToSecondPoint = false;
                    lastNextBlock = nextBlock;
                    lastNextLine = nextLine;
                }
                else
                {
                    nextPositionVector = 0;
                    moveToSecondPoint = false;
                    lastNextBlock = nextBlock;
                    lastNextLine = nextLine;
                }



        //if(!isMoving && posV2 == 0)
        //   v2NextPosition[0] = blocks[nextLine, nextBlock].transform.position;


        //Debug.Log("Last: " + posV2);




        //if (!isMoving)
        //    if(moveToSecondPoint)
        //    {
        //        posV2 = 1;

        //        nextLine = saveNextLine;
        //        nextBlock = saveNextBlock;
        //    }


        //if (!isMoving)
        //    if (posV2 == 1)
        //    {
        //        posV2 = 0;
        //    }

        // Usar el array de v2Next para eliguir donde moverse solo son eso!


        //Debug.Log("Pos: " + posV2 + "// Moving: " + isMoving);
        //Debug.Log("Move Second Point: " + moveToSecondPoint);
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

