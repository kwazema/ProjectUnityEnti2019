using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour {

    private int column, row;
    private int nextColumn, nextRow;
    private int columnLenth, rowLenth;

    private int numPositionMove = 0, numPositionSave = 0;

    public float speed;

    bool isMoving = false;
    bool moveToSecondBlock = false;

    private Vector2[] v2Position;
    public Rigidbody2D rb;
    Player1Input player1Input;

    // <-- Funciones Get --> //
    #region Functions
    public bool GetIsMoving() { return isMoving; }
    #endregion


    //private StatsBlock[,] blocks;
    private Map map;

    public GameObject blockPrefab;

    private void Awake()
    {
        //map = GameObject.Find("Map").GetComponent<Map>();
        map = FindObjectOfType<Map>();
    }

    // Use this for initialization
    void Start ()
    {
        //---------- Init Length ----------- //
        columnLenth = map.columnLenth / 2;
        rowLenth = map.rowLenth;

        //player1Input = gameObject.GetComponent<Player1Input>(); // Diferencia?
        player1Input = GetComponent<Player1Input>();

        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();

        //------- Init Movemen --------- //
        v2Position = new Vector2[2];
        v2Position[0] = map.blocks[0, 0].transform.position;
        v2Position[1] = map.blocks[0, 0].transform.position;
        speed = 40.0f;

        //----- Init Player Position ----- //
        transform.position = map.blocks[0, 0].transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        MovementCharacter();
    }


    //----------- Functions: Movement -----------//
    void MovementCharacter()
    {
        float step = speed * Time.deltaTime;
        Debug.Log("Position Player: " + transform.position);
        Debug.Log("Position GO: " + v2Position[numPositionMove]);
        transform.position = Vector2.MoveTowards(transform.position, v2Position[numPositionMove], step);

        // Compruba si se esta movimiento
        if ((Vector2)transform.position != v2Position[numPositionMove])
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Cambiar el num de guardado y guardar la siguiente posicion del v2.
        if (isMoving && player1Input.isInput)
        {

            if (numPositionMove == 0)
            {
                numPositionSave = 1;
            }
            else
            {
                numPositionSave = 0;
            }

            v2Position[numPositionSave] = map.blocks[column, row].transform.position;

            moveToSecondBlock = true;

            Debug.Log("Me muevo y click");
        }

        // Si tenemos una posición guardada cambiar el numPosition para movernos a la siguiente posició.
        if (!isMoving && moveToSecondBlock)
        {
            moveToSecondBlock = false;

            if (numPositionMove == 0)
                numPositionMove = 1;
            else
                numPositionMove = 0;
        }

        // Guarda la posicion del bloque actual del Player.
        if (!isMoving)
        {
            v2Position[numPositionSave] = map.blocks[column, row].transform.position;

            nextColumn = column;
            nextRow = row;
            //Debug.Log("Paso 1 Guardar");
        }
    }

    void MovementAction(int horizontal, int vertical)
    {

        //if (Can i move?)
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
    //StatsBlock[,] InstantiateBlocks(int column, int row, Vector2Int offset, float width, float height, float margin)
    //{
    //    StatsBlock[,] blocks = new StatsBlock[column, row];

    //    for (int i = 0; i < column; i++)
    //    {
    //        for (int j = 0; j < row; j++)
    //        {
    //            blocks[i, j] = Instantiate(blockPrefab).GetComponent<StatsBlock>();
    //            blocks[i, j].transform.position = offset + new Vector2(i * (width + margin), j * -(height + margin));
    //        }
    //    }

    //    return blocks;
    //}
}