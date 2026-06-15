using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("ゲーム設定")]
    public bool isGameStarted = false;
    public bool isCountingDown = false;
    public bool isGameOver = false;

    public int score = 0;
    public float startTime = 30f;
    private float time;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text countdownText;

    [Header("ゲームオーバーUI")]
    public GameObject gameOverPanel;

    [Header("音")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bgmSound;
    [SerializeField] private AudioClip countDownSound;
    [SerializeField] private AudioClip gameStartSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private float seVolume = 1f;
    [SerializeField] private float bgmVolume = 0.5f;

    private int oldScore = -1;
    private int oldTime = -1;

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

        ShowCountdown("3");
        audioSource.PlayOneShot(countDownSound);
        yield return new WaitForSeconds(1f);

        ShowCountdown("2");
        audioSource.PlayOneShot(countDownSound);
        yield return new WaitForSeconds(1f);

        ShowCountdown("1");
        audioSource.PlayOneShot(countDownSound);
        yield return new WaitForSeconds(1f);

        ShowCountdown("START!");
        audioSource.PlayOneShot(gameStartSound);
        yield return new WaitForSeconds(1f);

        audioSource.volume = bgmVolume;
        audioSource.PlayOneShot(bgmSound);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        score = 0;
        time = startTime;

        isGameOver = false;
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
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        audioSource.volume = seVolume;
        audioSource.PlayOneShot(gameOverSound);
    }

    public void AddScore(int amount)
    {
        if (!isGameStarted) return;

        score += amount;
        UpdateUI();
    }

    public bool IsBonusTime()
    {
        return isGameStarted && time <= 10f;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;

        int intTime = Mathf.CeilToInt(time);

        if (timeText != null)
            timeText.text = "Time : " + intTime;

        if (oldScore != score)
        {
            oldScore = score;

            if (score >= 0)
            {
                AimEventDispatcher.Fire("updateScore", new object[] { score });
            }
        }

        if (oldTime != intTime)
        {
            oldTime = intTime;
            AimEventDispatcher.Fire("updateTime", new object[] { intTime });
        }
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
        isGameOver = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        UpdateUI();
    }
}