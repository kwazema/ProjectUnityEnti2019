using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{

    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        //Invoke("", 4f);
        StartVideo();
        InvokeRepeating("CheckOver", 1f, .1f);

    }

    private void StartVideo()
    {
        videoPlayer.Play();
        //audioSource.Play();
    }

    private void CheckOver()
    {

        long playerCurrentFrame = videoPlayer.frame;
        long playerFrameCount = (int)videoPlayer.frameCount;


        if (playerCurrentFrame < playerFrameCount)
        {

        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }


}