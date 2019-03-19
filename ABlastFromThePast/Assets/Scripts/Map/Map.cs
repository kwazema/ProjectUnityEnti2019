﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public int columnLenth, rowLenth;

    public GameObject blockPrefab;
    public GameObject map;
    public StatsBlock[,] blocks;

    private void Awake()
    {
        columnLenth = 8; rowLenth = 4;
        blocks = InstantiateBlocks(columnLenth, rowLenth, new Vector2Int(-15, 3), 3, 3, 1);
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    StatsBlock[,] InstantiateBlocks(int column, int row, Vector2Int offset, float width, float height, float margin)
    {
        StatsBlock[,] blocks = new StatsBlock[column, row];

        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                blocks[i, j] = Instantiate(blockPrefab).GetComponent<StatsBlock>();
                blocks[i, j].transform.SetParent(map.transform);
                blocks[i, j].transform.position = offset + new Vector2(i * (width + margin), j * -(height + margin));
                blocks[i, j].thisColumn = i;
                blocks[i, j].thisRow = j;
            }
        }

        return blocks;
    }
}
