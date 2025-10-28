using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text timerText;
    public GameObject roundStartPanel;
    public GameObject gameOverPanel;

    [Header("Lives UI Icons")]
    public GameObject[] lifeIcons;


    [Header("Settings")]
    public int startLives = 3;

    private int score = 0;
    private int lives;
    private float timer = 0f;
    private bool gameRunning = false;

    void Awake()
    {
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

    void Start()
    {
        lives = startLives;
        UpdateScoreUI();
        UpdateLivesUI();
        StartCoroutine(RoundStartCountdown());
    }

    void Update()
    {
        if (gameRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    //Score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    //Lives
    public void LoseLife()
    {
        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            StartCoroutine(GameOverRoutine());
        }
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

        // Save high score
        int bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
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

        // Show or hide icons depending on lives remaining
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
}




