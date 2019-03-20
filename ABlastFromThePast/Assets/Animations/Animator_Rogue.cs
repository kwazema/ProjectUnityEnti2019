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
        if (Input.GetButtonDown("Fire1"))
        {
            int attack = Random.Range(0, 1);

            if (attack == 0)
            {
                animator.SetFloat("attack", 0.1f);
            } else
            {
                animator.SetFloat("attack", 1.1f);
            }
            animator.SetBool("isAttacking", true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isAttacking", false);
        }

        //if (Input.GetButtonDown("Horizontal"))
        //{
        //    animator.SetFloat("speed", 0.2f);
        //} else if (Input.GetButtonUp("Horizontal"))
        //{
        //    animator.SetFloat("speed", 0);
        //}
    }
}

