using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAMove : MonoBehaviour {

    Transform shot;

    float shotSpeed;

    // Use this for initialization
    void Start () {
        shot = GetComponent<Transform>();
        shotSpeed = (30 * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update () {
        shot.Translate(Vector2.right * shotSpeed);
	}
}
