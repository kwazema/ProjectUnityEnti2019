using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectArray
{
    public GameObject[] blocks; // Array de Game Objects
}

public class Movement : MonoBehaviour {

    enum Direction { right, left, down, top }

    private int nextBlock, nextLine;
    private float posCharacter = 150, posCorrectY = 1.5f;
    private Vector2 v2PosActual;

    public GameObjectArray[] lines; // Array de Clases
    Block[,] bloques;

    // Use this for initialization
    void Start () {
        bloques = new Block[lines.Length, lines[0].blocks.Length];
            
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].blocks.Length; j++)
            {
                bloques[i,j] = lines[i].blocks[j].GetComponent<Block>();
            }
        }

        //v2PosActual.x = boxes[0,0].transform.position.x;
        // v2PosActual.y = boxes[0,0].transform.position.y;

        v2PosActual = bloques[0, 0].transform.position;
        v2PosActual.y += posCorrectY;
        transform.position = v2PosActual;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            movement(Direction.right);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            movement(Direction.left);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            movement(Direction.top);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            movement(Direction.down);
        }
    }

    void movement(Direction direction)
    {
        if (direction == Direction.right && nextBlock < lines[nextLine].blocks.Length - 1)
        {
            if (!bloques[nextLine, nextBlock + 1].disableBlock)
            nextBlock++;
        }

        if (direction == Direction.left && nextBlock > 0)
        {
            if (!bloques[nextLine, nextBlock - 1].disableBlock)
                nextBlock--;
        }

        if (direction == Direction.down && nextLine < lines.Length - 1)
        {
            if (!bloques[nextLine + 1, nextBlock].disableBlock)
                nextLine++;
        }

        if (direction == Direction.top && nextLine > 0)
        {
            if (!bloques[nextLine -1, nextBlock].disableBlock)
                nextLine--;
        }

        transform.position = new Vector2(lines[nextLine].blocks[nextBlock].transform.position.x,
                                         lines[nextLine].blocks[nextBlock].transform.position.y + posCorrectY);


        Debug.Log("Line[" + nextLine + "] // Block[" + nextBlock + "]");
        Debug.Log("Disables: " + bloques[nextLine, nextBlock].disableBlock);
       // Debug.Log("Disables++: " + bloques[nextLine, nextBlock + 1].disableBlock);
    }
}
