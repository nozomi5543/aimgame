using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("ゲーム設定")]
    public bool isGameStarted = false;
    public bool isCountingDown = false;

    public int score = 0;
    public float startTime = 30f;
    private float time;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text countdownText;

    [Header("ゲームオーバーUI")]
    public GameObject gameOverPanel;
    public TMP_Text resultText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (!isGameStarted) return;

        time -= Time.deltaTime;

        if (time <= 0)
        {
            time = 0;
            GameOver();
        }

        UpdateUI();
    }

    public void StartGame()
    {
        if (isGameStarted || isCountingDown)
            return;

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        isCountingDown = true;

        ShowCountdown("3"); yield return new WaitForSeconds(1f);
        ShowCountdown("2"); yield return new WaitForSeconds(1f);
        ShowCountdown("1"); yield return new WaitForSeconds(1f);
        ShowCountdown("START!"); yield return new WaitForSeconds(1f);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        score = 0;
        time = startTime;

        isCountingDown = false;
        isGameStarted = true;

        UpdateUI();
    }

    void ShowCountdown(string text)
    {
        if (countdownText == null) return;

        countdownText.gameObject.SetActive(true);
        countdownText.text = text;
    }

    void GameOver()
    {
        isGameStarted = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (resultText != null)
            resultText.text = "SCORE : " + score;
    }

    public void AddScore(int amount)
    {
        if (!isGameStarted) return;

        score += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;

        if (timeText != null)
            timeText.text = "Time : " + Mathf.CeilToInt(time);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    void ResetGame()
    {
        score = 0;
        time = startTime;
        isGameStarted = false;
        isCountingDown = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        UpdateUI();
    }
}