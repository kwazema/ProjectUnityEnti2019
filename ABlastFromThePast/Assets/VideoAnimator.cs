using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoAnimator : MonoBehaviour {

    public Animator anim;

    public void EnterButton()
    {
        anim.ResetTrigger("FadeOut");
        anim.SetTrigger("FadeIn");
    }

    public void ExitButton()
    {
        anim.ResetTrigger("FadeIn");
        anim.SetTrigger("FadeOut");
    }
}
