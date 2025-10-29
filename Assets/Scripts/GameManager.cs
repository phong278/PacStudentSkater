using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton

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
<<<<<<< Updated upstream
=======

    private bool isRespawning = false;


    //Death 
    public void PacStudentDeath()
    {
        if (isRespawning) return;
        isRespawning = true;

        gameRunning = false; //Stop timer

        //Disable movement
        pacStudent.enabled = false;
        foreach (GhostController g in ghosts)
            g.enabled = false;

        //Play animation + particles
        Instantiate(deathParticle, pacStudent.transform.position, Quaternion.identity);

        pacStudent.animator.Play("Skater_Death_Pose", 0, 0);
        pacStudent.animator.speed = 0;
        Debug.Log("Now playing: " + pacStudent.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        pacStudent.deathAudio.Play();


        StartCoroutine(RespawnSequence(3f));
    }

    IEnumerator RespawnSequence(float deathDelay)
    {
        yield return new WaitForSeconds(deathDelay);

        pacStudent.animator.speed = 1;


        LoseLife(); //Handles life UI + game over if 0

        if (lives <= 0)
        {
            isRespawning = false;
            yield break;
        }

        // Reset positions
        pacStudent.transform.position = pacStartPos.position;
        pacStudent.ResetMovement();
        pacStudent.animator.Play("Skater_Cruising_Forwards");

        foreach (GhostController g in ghosts)
        {
            g.transform.position = g.startPosition; //Store this inside each ghost
            g.animator.Play(g.normalAnim);
            g.enabled = true;
        }

        Collider2D col = pacStudent.GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        //Short pause before re-enabling everything
        yield return new WaitForSeconds(1f);

        if (col != null) col.enabled = true;
        pacStudent.enabled = true;
        gameRunning = true;
        isRespawning = false;
    }



    //Round Start
    IEnumerator RoundStartCountdown()
    {
        roundStartPanel.SetActive(true);
        TMP_Text countdownText = roundStartPanel.GetComponentInChildren<TMP_Text>();

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);

        roundStartPanel.SetActive(false);
        gameRunning = true;
    }

    //Game Over
    IEnumerator GameOverRoutine()
    {
        gameRunning = false;
        gameOverPanel.SetActive(true);

        //Load saved values
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", 99999f);

        //If this run beats high score or ties but faster time
        if (score > bestScore || (score == bestScore && timer < bestTime))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.SetFloat("BestTime", timer);
            PlayerPrefs.Save();
        }

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("StartScene");
    }


    //UI Helpers
    void UpdateScoreUI() { if (scoreText) scoreText.text = "Score: " + score; }
    void UpdateLivesUI()
    {
        if (lifeIcons.Length == 0) return;

        //Show or hide icons depending on lives remaining
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(i < lives);
        }
    }

    void UpdateTimerUI()
    {
        if (timerText)
        {
            int mins = Mathf.FloorToInt(timer / 60);
            int secs = Mathf.FloorToInt(timer % 60);
            timerText.text = $"{mins:00}:{secs:00}";
        }

    }

    public void ActivatePowerPellet()
    {
        AddScore(50);

        //Switch background music
        normalMusic.Stop();
        scaredMusic.Play();

        //Make all ghosts scared
        foreach (GhostController g in ghosts)
        {
            g.animator.Play("Cop_Scared_Pose");
        }

        //Begin the 10 sec timer
        StartCoroutine(GhostScaredTimer());
    }

    IEnumerator GhostScaredTimer()
    {
        yield return new WaitForSeconds(7f);
        Debug.Log("Ghosts are now recovering!");


        yield return new WaitForSeconds(3f);
        Debug.Log("Ghosts back to normal!");

        scaredMusic.Stop();
        normalMusic.Play();

        foreach (GhostController g in ghosts)
        {
            g.animator.Play(g.normalAnim);
        }
    }



>>>>>>> Stashed changes
}
