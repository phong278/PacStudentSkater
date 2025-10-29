using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    public AudioSource introMusic;
    public AudioSource copNormalMusic;
    public AudioSource copScaredMusic; 

    void Start()
    {
        introMusic.Play();
        Invoke(nameof(PlayNormalMusic), 3f);
    }

    public void PlayNormalMusic()
    {
        introMusic.Stop();
        copScaredMusic?.Stop();    
        copNormalMusic.Stop();     
        copNormalMusic.Play();
    }

    public void PlayScaredMusic()
    {
        copNormalMusic.Stop();
        introMusic.Stop();
        copScaredMusic.Stop();
        copScaredMusic.Play();
    }
}
