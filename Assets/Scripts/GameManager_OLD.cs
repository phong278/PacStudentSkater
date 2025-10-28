using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_OLD : MonoBehaviour
{
    public static GameManager_OLD instance;  // Singleton

    private int score = 0;

    void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score); // for now, print to console
    }

    public int GetScore()
    {
        return score;
    }
}
