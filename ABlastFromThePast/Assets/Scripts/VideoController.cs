﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[System.Serializable]
public class VideoData
{
    public VideoClip general;

    public VideoClip basic;
    public VideoClip skill;
    public VideoClip ultimate;

    public VideoClip shield;


    public void SetGeneral(VideoPlayer vp)
    {
        vp.clip = skill;
    }

    public void SetBasic(VideoPlayer vp)
    {
        vp.clip = ultimate;
    }

}

public class VideoController : MonoBehaviour {
    public RawImage rawImageSkill;
    public VideoPlayer videoPlayerSkill;

    public RenderTexture render;
    //public RawImage rawImageUlti;
    //public VideoPlayer videoPlayerUlti;

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayVideo());
        rawImageSkill.texture = render;
    }

    IEnumerator PlayVideo()
    {
        videoPlayerSkill.Prepare();
        //videoPlayerUlti.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
        while (!videoPlayerSkill.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        //while (!videoPlayerUlti.isPrepared)
        //{
        //    yield return waitForSeconds;
        //    break;
        //}

        rawImageSkill.texture = videoPlayerSkill.texture;
        videoPlayerSkill.Play();
        //rawImageUlti.texture = videoPlayerUlti.texture;
        //videoPlayerUlti.Play();
    }    
}
