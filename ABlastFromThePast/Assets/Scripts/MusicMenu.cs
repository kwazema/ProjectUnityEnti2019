using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour
{
    void Start () {
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0: AudioManager.instance.Play("MusicMenu01"); break;
            case 1: AudioManager.instance.Play("MusicMenu02"); break;
        }

        AudioManager.instance.Stop("MusicBattle01");
        AudioManager.instance.Stop("MusicBattle02");
    }
}
