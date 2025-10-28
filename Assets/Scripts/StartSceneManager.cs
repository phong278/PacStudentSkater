using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartSceneManager : MonoBehaviour
{
    public TMP_Text statsText; 

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);

        statsText.text =
            $"High Score: {highScore}\nBest Time: {FormatTime(bestTime)}";
    }

    string FormatTime(float t)
    {
        int mins = Mathf.FloorToInt(t / 60f);
        int secs = Mathf.FloorToInt(t % 60f);
        return $"{mins:00}:{secs:00}";
    }
}
