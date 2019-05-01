using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Rogue : MonoBehaviour
{
    public Animator animator;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) ||Input.GetKeyDown(KeyCode.Alpha2))
        {
            //int attack = Random.Range(0, 1);

            //if (attack == 0)
            //{
            //    animator.SetFloat("attack", 0.1f);
            //} else
            //{
            //    animator.SetFloat("attack", 1.1f);
            //}
            //animator.SetBool("dash", true);

            //yield  return new WaitForSeconds(3);


        }
        //else if (Input.GetKeyUp(KeyCode.B) || Input.GetKeyUp(KeyCode.Alpha2))
        //{
        //    animator.SetBool("dash", false);
        //}

        //if (Input.GetButtonDown("Horizontal"))
        //{
        //    animator.SetFloat("speed", 0.2f);
        //} else if (Input.GetButtonUp("Horizontal"))
        //{
        //    animator.SetFloat("speed", 0);
        //}
    }
}

