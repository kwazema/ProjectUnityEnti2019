using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//[System.Serializable]
//[System.NonSerialized]

//public class Movement : MonoBehaviour
//{
//    private Vector2 v2PosActual;

//    //public GameObjectArray[] lines; // Array de Clases

//    public SpriteRenderer[,] spriteRenderers;


//    int randomColumn = 0;
//    int randomRow = 0;

//    void Start()
//    {
//        //---------- Init RB ----------- //
//        rb = GetComponent<Rigidbody2D>();
//        v2NextPosition = new Vector2[2];
//        speed = 40.0f;

//        //---------- Init Blocks ----------- //
//        blocks = InstantiateBlocks(columnLenth, rowLenth, new Vector2Int(-15, 3), 3, 3, 1);
//        spriteRenderers = InitRenderBlocks(columnLenth, rowLenth);

//        //---------- Init First Position ----------- //
//        //  v2PosActual = blocks[0, 0].transform.position;
//        // v2PosActual.y += posCorrectY;
//        //transform.position = v2PosActual;
//        //v2NextPosition[0] = blocks[0, 0].transform.position;
//    }

//    SpriteRenderer[,] InitRenderBlocks(int columnLenth, int rowLenth)
//    {
//        SpriteRenderer[,] spriteRenderers = new SpriteRenderer[columnLenth, rowLenth];

//        for (int i = 0; i < columnLenth; i++)
//        {
//            for (int j = 0; j < rowLenth; j++)
//            {
//                spriteRenderers[i, j] = blocks[i, j].GetComponent<SpriteRenderer>();
//            }
//        }

//        return spriteRenderers;
//    }


  

//    void destroyBlock(int columnLenth, int rowLenth)
//    {
//        if (Input.GetKeyDown(KeyCode.H))
//        {
//            //Debug.Log("H " + randomColumn + " // " + randomRow);

//            if (blocks[randomColumn, randomRow].disableBlock)
//            {
//                blocks[randomColumn, randomRow].disableBlock = false;
//                spriteRenderers[randomColumn, randomRow].color = Color.green;
//            }

//            randomColumn = Random.Range(0, columnLenth);
//            randomRow = Random.Range(0, rowLenth);

//            blocks[randomColumn, randomRow].disableBlock = true;
//            spriteRenderers[randomColumn, randomRow].color = Color.red;
//        }
//    }

//    void Update()
//    {

//        destroyBlock(columnLenth, rowLenth);

//        MovementCharacter();

//    }

    

//}

