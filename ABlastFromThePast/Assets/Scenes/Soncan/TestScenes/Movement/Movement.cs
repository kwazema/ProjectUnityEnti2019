using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//[System.Serializable]
//[System.NonSerialized]

public class Movement : MonoBehaviour
{
    private int nextColumn, nextRow, nextBlockOne, nextLineOne;
    private float dirHorizontal, dirVertical;
    private float speedCharacter;
    private Vector2 v2PosActual;

    public GameObject blockPrefab;
    //public GameObjectArray[] lines; // Array de Clases
    public Rigidbody2D rb;

    public StatsBlock[,] blocks;
    public SpriteRenderer[,] spriteRenderers;

    private float speed;
    bool isMoving = false, moveToSecondPoint = false;
    Vector2[] v2NextPosition;
    Vector2 v2SavePosition;
    int nextPositionVector = 0, posV2Guardada = 0;
    int lastNextColumn = 0;
    int lastNextRow = 0;

    int columnLenth = 4;
    int rowLenth = 3;
    int randomColumn = 0;
    int randomRow = 0;

    bool displacedHorizontal = false;
    bool displacedVertical = false;

    void Start()
    {
        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();
        v2NextPosition = new Vector2[2];
        speed = 40.0f;

        //---------- Init Blocks ----------- //
        blocks = InstantiateBlocks(columnLenth, rowLenth, new Vector2Int(-15, 3), 3, 3, 1);
        spriteRenderers = InitRenderBlocks(columnLenth, rowLenth);

        //---------- Init First Position ----------- //
        //  v2PosActual = blocks[0, 0].transform.position;
        // v2PosActual.y += posCorrectY;
        //transform.position = v2PosActual;
        //v2NextPosition[0] = blocks[0, 0].transform.position;
    }

    SpriteRenderer[,] InitRenderBlocks(int columnLenth, int rowLenth)
    {
        SpriteRenderer[,] spriteRenderers = new SpriteRenderer[columnLenth, rowLenth];

        for (int i = 0; i < columnLenth; i++)
        {
            for (int j = 0; j < rowLenth; j++)
            {
                spriteRenderers[i, j] = blocks[i, j].GetComponent<SpriteRenderer>();
            }
        }

        return spriteRenderers;
    }


    StatsBlock[,] InstantiateBlocks(int column, int row, Vector2Int offset, float width, float height, float margin)
    {
        StatsBlock[,] blocks = new StatsBlock[column, row];

        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                blocks[i, j] = Instantiate(blockPrefab).GetComponent<StatsBlock>();
                blocks[i, j].transform.position = offset + new Vector2(i * (width + margin), j * -(height + margin));
            }
        }

        return blocks;
    }

    void destroyBlock(int columnLenth, int rowLenth)
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Debug.Log("H " + randomColumn + " // " + randomRow);

            if (blocks[randomColumn, randomRow].disableBlock)
            {
                blocks[randomColumn, randomRow].disableBlock = false;
                spriteRenderers[randomColumn, randomRow].color = Color.green;
            }

            randomColumn = Random.Range(0, columnLenth);
            randomRow = Random.Range(0, rowLenth);

            blocks[randomColumn, randomRow].disableBlock = true;
            spriteRenderers[randomColumn, randomRow].color = Color.red;
        }
    }

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

        //--------------- Move Right --------------- //
        if (horizontal > 0 && nextColumn < columnLenth - 1)
            if (!blocks[nextColumn + 1, nextRow].disableBlock)
                nextColumn += (int)horizontal;

        //--------------- Move Left --------------- //
        if (horizontal < 0 && nextColumn > 0)
            if (!blocks[nextColumn - 1, nextRow].disableBlock)
                nextColumn += (int)horizontal;

        //--------------- Move Up --------------- //
        if (vertical < 0 && nextRow < rowLenth - 1)
            if (!blocks[nextColumn, nextRow + 1].disableBlock)
                nextRow -= (int)vertical;

        //--------------- Move Down --------------- //
        if (vertical > 0 && nextRow > 0)
            if (!blocks[nextColumn, nextRow - 1].disableBlock)
                nextRow -= (int)vertical;

        //Debug.Log("Column: " + nextColumn + " // Row: " + nextRow);
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

        //Debug.Log(ready);

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
            //Debug.Log("Paso 2 Guardar Segunda pos");
        }

        if (!isMoving)
            if (moveToSecondPoint)
                if (nextPositionVector == 0)
                {
                    nextPositionVector = 1;
                    moveToSecondPoint = false;
                    //Debug.Log("Paso 3 Mueve Segunda pos");
                }
                else
                {
                    nextPositionVector = 0;
                    moveToSecondPoint = false;
                    //Debug.Log("Paso 3 Mueve Segunda pos");
                }


        // Antes de moverse, guarda la pos donde se esta desplazando.
        if (!isMoving)
        {

            v2NextPosition[posV2Guardada] = blocks[nextColumn, nextRow].transform.position;

            lastNextColumn = nextColumn;
            lastNextRow = nextRow;
            //Debug.Log("Paso 1 Guardar");
        }
    }








    //---------------------------------------------------------------------------------------//





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

        //Debug.Log(Input.GetJoystickNames() + " is moved");

        //Debug.Log("dirHorizontal" + dirHorizontal + "dirVertical" + dirVertical);
        //----------- Horizontal Controller ------------ //
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

        //----------- Vertical Controller ------------ //
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

