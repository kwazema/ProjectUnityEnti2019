﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int columnLenth, rowLenth;

    public GameObject blockPrefab;
    public GameObject map;
    public StatsBlock[,] blocks;
    public Sprite  blockSprite;

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
        blocks = InstantiateBlocks(columnLenth, rowLenth, new Vector2Int(-14, 5), 3.15f, 2.15f, 1f);
        gameManager.InitPlayers();
    }

    private void Start() { }

    StatsBlock[,] InstantiateBlocks(int column, int row, Vector2Int offset, float width, float height, float margin)
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

                if (i >= column / 2)
                {
                    blocks[i, j].spriteBlock.sprite = blockSprite;
                }
            }
        }
        return blocks;
    }

    public void ColorBlocks(int pos_x, int pos_y, Color color)
    {
        blocks[pos_x, pos_y].spriteBlock.color = color;
    }
}
