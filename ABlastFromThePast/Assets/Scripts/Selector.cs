using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

    public Animator animSelector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectedBlue()
    {
        animSelector.SetTrigger("SelectedBlue");
    }

    public void SelectedRed()
    {
        animSelector.SetTrigger("SelectedRed");
    }

    public void SelectedNone()
    {
        animSelector.SetTrigger("None");
    }
}
