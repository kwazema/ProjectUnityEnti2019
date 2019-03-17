using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour {

    [SerializeField] enum EnumPlayer { Player1, Player2 }
    [SerializeField] EnumPlayer player;

    public int column, row;
    private int nextColumn, nextRow;
    private int columnLenth, rowLenth;

    private int numPositionMove = 0, numPositionSave = 0;

    public float speed;

    bool isMoving = false;
    bool moveToSecondBlock = false;

    private Vector2[] v2Position;
    public Rigidbody2D rb;
    Player1Input player1Input;

    //private StatsBlock[,] blocks;
    private Map map;

    public GameObject blockPrefab;

    private void Awake()
    {
        //map = GameObject.Find("Map").GetComponent<Map>();
        map = FindObjectOfType<Map>();
    }

    // <-- Funciones Get --> //
    #region Functions
    public bool GetIsMoving() { return isMoving; }
    #endregion


    // Use this for initialization
    void Start ()
    {
        //---------- Init Length ----------- //
        

        //player1Input = gameObject.GetComponent<Player1Input>(); // Diferencia?
        player1Input = GetComponent<Player1Input>();

        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();

        //------- Init Movemen --------- //
        v2Position = new Vector2[2];
        speed = 40.0f;
        columnLenth = map.columnLenth / 2;
        rowLenth = map.rowLenth;

        if (EnumPlayer.Player1 == player)
        {
            column = 0; row = 0;

            v2Position[0] = map.blocks[column, row].transform.position;
            transform.position = v2Position[0];
        }
        else
        {
            column = map.columnLenth - 1; row = 0;

            v2Position[0] = map.blocks[column, row].transform.position;
            transform.position = v2Position[0];
        }

        //----- Init Player Position ----- //
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
        //Debug.Log("Position Player: " + transform.position);
        //Debug.Log("Position GO: " + v2Position[numPositionMove]);
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

    void MovementAction(int dirHorizontal, int dirVertical)
    {
        if (EnumPlayer.Player1 == player)
        {
            //if (Can i move?)
            //-------------- Move Right -------------- //
            if (dirHorizontal > 0 && column < columnLenth - 1)
                column += dirHorizontal;

            //-------------- Move Left -------------- //
            if (dirHorizontal < 0 && column > 0)
                column += dirHorizontal;

            //--------------- Move Up --------------- //
            if (dirVertical < 0 && row < rowLenth - 1)
                row -= dirVertical;

            //-------------- Move Down -------------- //
            if (dirVertical > 0 && row > 0)
                row -= dirVertical;
        }
        else
        {
            //if (Can i move?)
            //-------------- Move Right -------------- //
            if (dirHorizontal > 0 && column < map.columnLenth - 1)
                column += dirHorizontal;

            //-------------- Move Left -------------- //
            if (dirHorizontal < 0 && column > map.columnLenth / 2)
                column += dirHorizontal;

            //--------------- Move Up --------------- //
            if (dirVertical < 0 && row < rowLenth - 1)
                row -= dirVertical;

            //-------------- Move Down -------------- //
            if (dirVertical > 0 && row > 0)
                row -= dirVertical;
        }
    }

    public void SetPositionBlocks(int dirHorizontal, int dirVertical)
    {
        if (moveToSecondBlock)
        {
            column = nextColumn;
            row = nextRow;
            Debug.Log("Paso 4: Sobre escribe Segunda Pos");
        }

        MovementAction(dirHorizontal, dirVertical);
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