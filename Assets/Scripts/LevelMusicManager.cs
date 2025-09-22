using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    public AudioSource introMusic;
    public AudioSource copNormalMusic;


    void Start()
    {
        introMusic.Play();
        Invoke("PlayNormalMusic", 3f); // switch after 3 seconds

    }

    void PlayNormalMusic()
    {
        introMusic.Stop();
        copNormalMusic.Play();
    }
}
