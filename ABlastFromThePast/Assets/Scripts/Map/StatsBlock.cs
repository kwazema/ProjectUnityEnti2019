using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsBlock : MonoBehaviour {

    public bool disableBlock = false;
    public StatsBlock blocks;

    SpriteRenderer m_SpriteRenderer;

    // Use this for initialization
    void Start () {
        //blocks = new Block;
        //m_SpriteRenderer = new SpriteRenderer;

        //blocks = GetComponent<Block>();
        //m_SpriteRenderer = GetComponent<SpriteRenderer>();

        //Debug.Log("asd");
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    //Line = Random.Range(0, 3);
        //    //Column = Random.Range(0, 4);

        //    //blocks[2, 2].disableBlock = true;
        //    //m_SpriteRenderer[2, 2].color = Color.red;

        //    Debug.Log("Input");
        //}
    }
}
