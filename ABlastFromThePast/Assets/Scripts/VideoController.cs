using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[System.Serializable]
public class VideoData
{
    // Todo esto en el video principa
    //public VideoClip general;
    //public VideoClip basic;
    //public VideoClip shield;

    public VideoClip skill;
    public VideoClip ultimate;



    public void SetSkillVideo(VideoPlayer vp)
    {
        vp.clip = skill;
    }

    public void SetUltimateVideo(VideoPlayer vp)
    {
        vp.clip = ultimate;
    }

}

public class VideoController : MonoBehaviour {
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    //public RawImage rawImageUltimate;
    //public VideoPlayer videoPlayerUltimate;

    public RenderTexture render;
    //public RawImage rawImageUlti;
    //public VideoPlayer videoPlayerUlti;

	// Use this for initialization
	void Start () {
        StartCoroutine(PlayVideo());


        rawImage.texture = render;
        //rawImageUltimate.texture = render;
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        while (!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
}
