using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int columnLenth, rowLenth;

    public GameObject blockPrefab;
    public GameObject map;
    public StatsBlock[,] blocks;

    public Sprite  blockBlueNormal, blockBlueBroken, blockBlueVoid;
    public Sprite  blockRedNormal, blockRedBroken, blockRedVoid;

    public GameObject pause_menu;

    enum Tipe
    {
        blueNormal,
        redNormal,
        blueActive,
        redActive
    } Tipe tipe;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        blocks = InstantiateBlocks(columnLenth, rowLenth, new Vector2(-12.04f, 2f), 2.45f, 1.45f, 1f);

        gameManager.InitPlayers();
    }

    private void Start() { }


    // Activa el menu de pause
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pause_menu.SetActive(true);
        }
    }

    StatsBlock[,] InstantiateBlocks(int column, int row, Vector2 offset, float width, float height, float margin)
    {
        StatsBlock[,] blocks = new StatsBlock[column, row];

        for (int i = 0; i < column; i++) // Horizontal
        {
            for (int j = 0; j < row; j++) // Vertical
            {
                blocks[i, j] = Instantiate(blockPrefab).GetComponent<StatsBlock>();
                blocks[i, j].transform.SetParent(map.transform);
                blocks[i, j].transform.position = offset + new Vector2(i * (width + margin), j * -(height + margin));
                blocks[i, j].SetColumn(i);
                blocks[i, j].SetRow(j);

                if (i < column / 2)
                {
                    blocks[i, j].sp.sprite = blockBlueNormal;

                    blocks[i, j].blockNormal = blockBlueNormal;
                    blocks[i, j].blockBroken = blockBlueBroken;
                    blocks[i, j].blockVoid = blockBlueVoid;

                    blocks[i, j].colorBlock = StatsBlock.ColorBlock.blue;
                }
                else
                {
                    blocks[i, j].sp.sprite = blockRedNormal;

                    blocks[i, j].blockNormal = blockRedNormal;
                    blocks[i, j].blockBroken = blockRedBroken;
                    blocks[i, j].blockVoid = blockRedVoid;

                    blocks[i, j].colorBlock = StatsBlock.ColorBlock.red;
                }
            }
        }
        return blocks;
    }

    public void ColorBlocks(int pos_x, int pos_y, Color color)
    {
        blocks[pos_x, pos_y].sp.color = color;
    }

    public void SetAlert(int pos_x, int pos_y, bool setBool)
    {
        blocks[pos_x, pos_y].anim.SetBool("alert", setBool);
    }
}
