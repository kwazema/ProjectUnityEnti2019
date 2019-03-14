using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour {

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

    private void OnCollisionEnter2D (Collision2D col)
    {
        Debug.Log("Shot to point");
        Destroy(gameObject);
    }

}
