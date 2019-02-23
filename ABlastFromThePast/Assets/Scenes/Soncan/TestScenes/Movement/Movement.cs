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
    private int nextBlock, nextLine;
    private float posCorrectY = 1.5f;
    private float dirHorizontal, dirVertical;
    private float speedCharacter;
    private Vector2 v2PosActual;

    public GameObjectArray[] lines; // Array de Clases
    public Rigidbody2D rb;
    public Block[,] blocks;
    SpriteRenderer[,] m_SpriteRenderer;
    private float speed = 10.0f;

    bool displacedHorizontal = false;
    bool displacedVertical = false;

    int Line, Column;

    // Use this for initialization
    void Start()
    {
        //---------- Init RB ----------- //
        rb = GetComponent<Rigidbody2D>();
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
        v2PosActual = blocks[0, 0].transform.position;
        v2PosActual.y += posCorrectY;
        transform.position = v2PosActual;
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

            Debug.Log("Input");
        }
    }

    void SetPositionBlocks(float horizontal, float vertical)
    {
        Debug.Log("Horizontal: " + Input.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxisRaw("Vertical"));

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
        // Configurar que no puedas moverte hacia otra posición hasta que llegues a la celda deseada.

        //rb.MovePosition((Vector2)blocks[nextLine, nextBlock].transform.position + velocity * Time.fixedDeltaTime);
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, blocks[nextLine, nextBlock].transform.position, step);
        //Debug.Log(rb.isKinematic);
    }

    void getAxisMovement()
    {

        //--------------- Direction Horizontal --------------- //
        dirHorizontal = Input.GetAxisRaw("Mouse X");

        if (Input.GetKeyDown(KeyCode.A))
           dirHorizontal = -1f;

        if (Input.GetKeyDown(KeyCode.D))
           dirHorizontal = 1;

        //--------------- Direction Vertical --------------- //
        dirVertical = Input.GetAxisRaw("Mouse Y");

        if (Input.GetKeyDown(KeyCode.W))
            dirVertical = 1f;

        if (Input.GetKeyDown(KeyCode.S))
            dirVertical = -1;


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

