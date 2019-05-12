using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int playerColumn, playerRow;
    private int nextColumn, nextRow;
    private int columnLenth, rowLenth;

    public int whichIs;
    public int GetWhichIs() { return whichIs; }

    private int numPositionMove = 0, numPositionSave = 0;

    public float speed;

    bool isMoving = false;
    bool moveToSecondBlock = false;

    private Vector2[] v2Position;
    public Rigidbody2D rb;
    private PlayerInput playerInput;
    private PlayerManager playerStats;

    private Map map;

    #region Functions Gets

    public bool GetIsMoving() { return isMoving; }

    #endregion

    private void Awake()
    {
        //map = GameObject.Find("Map").GetComponent<Map>();
        map = FindObjectOfType<Map>();

        playerInput = GetComponent<PlayerInput>();
        playerStats = GetComponent<PlayerManager>();


        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        //player1Input = gameObject.GetComponent<Player1Input>(); // Diferencia?
        Debug.Log("This player is: " + playerStats.whichIsThisPlayer);

        //------- Init Movemen --------- //
        v2Position = new Vector2[2];
        speed = 40.0f;
        columnLenth = map.columnLenth / 2;
        rowLenth = map.rowLenth;

        InitPlayerPosition();
    }

    // Update is called once per frame
    void Update ()
    {
        MovementCharacter();
        if (isMoving)
            playerStats.DeployParticles(PlayerManager.Particles.Move);
    }


    //----------- Functions: Movement -----------//
    void MovementCharacter()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, v2Position[numPositionMove], step);

        // Cambiar el num de guardado y guardar la siguiente posicion del v2.
        if (isMoving && playerInput.IsInput())
        {
            if (numPositionMove == 0)
                numPositionSave = 1;
            else
                numPositionSave = 0;

            v2Position[numPositionSave] = map.blocks[playerColumn, playerRow].transform.position;

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
            v2Position[numPositionSave] = map.blocks[playerColumn, playerRow].transform.position;

            nextColumn = playerColumn;
            nextRow = playerRow;
            //Debug.Log("Paso 1 Guardar");
        }

        // Compruba si se esta movimiento
        if ((Vector2)transform.position != v2Position[numPositionMove])
            isMoving = true;
        else
            isMoving = false;
    }

    void MovementAction(int dirHorizontal, int dirVertical)
    {
        if (playerInput != null)
        {

            if (PlayerManager.ThisPlayerIs.Player1 == playerStats.thisPlayerIs)
            {
                int column = playerColumn;
                int row = playerRow;
                bool goMove = false;

                if (map.blocks[playerColumn, playerRow].recovering)
                {
                    //goMove = true;
                }

                //-------------- Move Right -------------- //
                if (dirHorizontal > 0 && playerColumn < columnLenth - 1)
                    playerColumn += dirHorizontal;

                //-------------- Move Left -------------- //
                if (dirHorizontal < 0 && playerColumn > 0)
                    playerColumn += dirHorizontal;

                //--------------- Move Up --------------- //
                if (dirVertical < 0 && playerRow < rowLenth - 1)
                    playerRow -= dirVertical;

                //-------------- Move Down -------------- //
                if (dirVertical > 0 && playerRow > 0)
                    playerRow -= dirVertical;



                if (map.blocks[playerColumn, playerRow].recovering && !goMove)
                {
                    playerColumn = column;
                    playerRow = row;
                }
            }
            else if (PlayerManager.ThisPlayerIs.Player2 == playerStats.thisPlayerIs)
            {
                int column = playerColumn;
                int row = playerRow;
                bool goMove = false;

                if (map.blocks[playerColumn, playerRow].recovering)
                {
                   // goMove = true;
                }
                //if (Can i move?)
                //-------------- Move Right -------------- //
                if (dirHorizontal > 0 && playerColumn < map.columnLenth - 1)
                    playerColumn += dirHorizontal;

                //-------------- Move Left -------------- //
                if (dirHorizontal < 0 && playerColumn > columnLenth)
                    playerColumn += dirHorizontal;

                //--------------- Move Up --------------- //
                if (dirVertical < 0 && playerRow < rowLenth - 1)
                    playerRow -= dirVertical;

                //-------------- Move Down -------------- //
                if (dirVertical > 0 && playerRow > 0)
                    playerRow -= dirVertical;

                if (map.blocks[playerColumn, playerRow].recovering && !goMove)
                {
                    playerColumn = column;
                    playerRow = row;
                }
            }
        }
        else
        {
            if (dirHorizontal > 0 && playerColumn < map.columnLenth - 1)
                playerColumn += dirHorizontal;

            //-------------- Move Left -------------- //
            if (dirHorizontal < 0 && playerColumn > columnLenth)
                playerColumn += dirHorizontal;

            //--------------- Move Up --------------- //
            if (dirVertical < 0 && playerRow < rowLenth - 1)
                playerRow -= dirVertical;

            //-------------- Move Down -------------- //
            if (dirVertical > 0 && playerRow > 0)
                playerRow -= dirVertical;
        }
    }

    public void SetPositionBlocks(int dirHorizontal, int dirVertical)
    {
        if (moveToSecondBlock)
        {
            playerColumn = nextColumn;
            playerRow = nextRow;
            Debug.Log("Paso 4: Sobre escribe Segunda Pos");
        }

        MovementAction(dirHorizontal, dirVertical);
    }

    void InitPlayerPosition()
    {
        if (PlayerInput.EnumPlayer.Player1 == playerInput.enumPlayer)
        {
            playerColumn = 0;
            playerRow = 0;
        }
        else if (PlayerInput.EnumPlayer.Player2 == playerInput.enumPlayer) {
            playerColumn = map.columnLenth - 1;
            playerRow = map.rowLenth - 1;
        }

        v2Position[0] = map.blocks[playerColumn, playerRow].transform.position;
        transform.position = v2Position[0];

        //playerRow = 0;
    }
}