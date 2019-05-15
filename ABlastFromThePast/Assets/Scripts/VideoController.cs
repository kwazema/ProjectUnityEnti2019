using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[System.Serializable]
public class VideoData
{
    public string name;
    public VideoPlayer videoPlayer;

    public VideoClip general;
    public VideoClip skill;
    public VideoClip ultimate;

    //public RawImage rawContainer;

    public bool loop;
}

public class VideoController : MonoBehaviour
{
    public VideoData[] videoData;

    /*Tambien podemos eleguir que videodata y video player cargar*/
    public void PlayVideoLibrary(string name)
    {
        VideoData vd = System.Array.Find(videoData, video => video.name == name);

        if (vd == null)
        {
            return;
        }

        //videoPlayer.targetTexture = render; Podemos escoger donde se reproducia
        //vd.rawContainer.texture = render;
        //StartCoroutine(PlayVideo(vd));
        vd.videoPlayer.clip = vd.general;
    }

    IEnumerator PlayVideo(VideoData videoData)
    {
        videoData.videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

        while (!videoData.videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        videoData.videoPlayer.Play();
        //videoData.rawImage.texture = videoData.videoPlayer.texture;
    }
}
