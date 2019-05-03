using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeImage : MonoBehaviour {

    public Animator anim;

    void Start () {
        anim.SetTrigger("FadeToImage");
    }

    public void FadeToImage()
    {
        anim.SetTrigger("FadeToImage");
    }

    public void FadeToBlack()
    {
        anim.SetTrigger("FadeToBlack");
    }
}
