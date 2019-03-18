using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour {

    Transform shot;

    public float shotSpeed;

    // Use this for initialization
    void Start () {
        shot = GetComponent<Transform>();
        shotSpeed = (30 * Time.deltaTime);
    }
	
	// Update is called once per frame
	void Update () {
        shot.Translate(Vector2.right * shotSpeed);
        CheckField();
	}

    private void OnCollisionEnter2D (Collision2D col)
    {
        Destroy(gameObject);
    }

    private void CheckField()
    {
        if (shot.position.x > Screen.width || shot.position.x < -20)    
            Destroy(gameObject);
    }   
}
