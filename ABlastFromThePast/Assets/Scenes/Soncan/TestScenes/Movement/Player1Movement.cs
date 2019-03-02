using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour {
    private int column, row;
    int nextColumn = 0;
    int nextRow = 0;

    int columnLenth = 4;
    int rowLenth = 3;

    int numPositionMove = 0; // 
    int numPositionSave = 0; // 

    public float speed;
    bool isMoving = false;
    bool moveToSecondBlock = false;

    public GameObject blockPrefab;
    private Block[,] blocks;
    private Vector2[] v2Position;
    public Rigidbody2D rb;

    Player1Input player1Input;

    // Use this for initialization
    void Start ()
    {
        //player1Input = gameObject.GetComponent<Player1Input>(); // Diferencia?
        player1Input = GetComponent<Player1Input>();

        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();

        //------- Init Movemen --------- //
        v2Position = new Vector2[2];
        speed = 40.0f;

        //----- Init Game Objects ----- //
        blocks = InstantiateBlocks(columnLenth, rowLenth, new Vector2Int(-15, 3), 3, 3, 1);
    }

    // Update is called once per frame
    void Update ()
    {
        MovementCharacter();
    }

    void MovementCharacter()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, v2Position[numPositionMove], step);

        // Resumir...
        if ((Vector2)transform.position != v2Position[numPositionMove])
        {
            isMoving = true;
        }
        else
        {
            isMoving = false; 
        }

        // Cambiar el num de guardado y guardar la siguiente posicion del v2.
        if (isMoving && player1Input.actionMove)
        {

            if (numPositionMove == 0)
            {
                numPositionSave = 1;
            }
            else
            {
                numPositionSave = 0;
            }

            v2Position[numPositionSave] = blocks[column, row].transform.position;

            moveToSecondBlock = true;

            Debug.Log("Me muevo y click");
        }

        // Resumir...
        if (!isMoving && moveToSecondBlock)
        {
            if (numPositionMove == 0)
            {
                numPositionMove = 1;
                moveToSecondBlock = false;
            }
            else
            {
                numPositionMove = 0;
                moveToSecondBlock = false;
            }
        }

        // Guarda la posicion del bloque actual del Player.
        if (!isMoving)
        {
            v2Position[numPositionSave] = blocks[column, row].transform.position;

            nextColumn = column;
            nextRow = row;
            //Debug.Log("Paso 1 Guardar");
        }
    }


    //----------- Functions: Movement -----------//
    void MovementAction(int horizontal, int vertical)
    {
        //-------------- Move Right -------------- //
        if (horizontal > 0 && column < columnLenth - 1)
            column += horizontal;

        //-------------- Move Left -------------- //
        if (horizontal < 0 && column > 0)
            column += horizontal;

        //--------------- Move Up --------------- //
        if (vertical < 0 && row < rowLenth - 1)
            row -= vertical;

        //-------------- Move Down -------------- //
        if (vertical > 0 && row > 0)
            row -= vertical;
    }

    public void SetPositionBlocks(int horizontal, int vertical)
    {
        if (moveToSecondBlock)
        {
            column = nextColumn;
            row = nextRow;
            Debug.Log("Paso 4: Sobre escribe Segunda Pos");
        }

        MovementAction(horizontal, vertical);
    }


    //----------- Functions: Game Objects -----------//
    Block[,] InstantiateBlocks(int column, int row, Vector2Int offset, float width, float height, float margin)
    {
        Block[,] blocks = new Block[column, row];

        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                blocks[i, j] = Instantiate(blockPrefab).GetComponent<Block>();
                blocks[i, j].transform.position = offset + new Vector2(i * (width + margin), j * -(height + margin));
            }
        }

        return blocks;
    }
}