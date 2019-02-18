using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruaba : MonoBehaviour {

    public GameObject explosion;
    public CameraShake cameraShake;
    public Transform explosionPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Explosion");
            Instantiate(explosion, explosionPosition.position, Quaternion.identity);
            StartCoroutine(cameraShake.Shake(.15f, .4f));
        }
    }
}
