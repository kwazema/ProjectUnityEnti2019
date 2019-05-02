using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {
    public GameObject clockManecilla1;
    public GameObject clockManecilla2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        clockManecilla1.transform.Rotate(Vector3.forward * -1);
        clockManecilla2.transform.Rotate(Vector3.forward * -0.075f);
    }
}
