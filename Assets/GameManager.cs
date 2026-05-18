using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStarted = false;

    public int score = 0;
    public float time = 30f;

    [Header("UI (TextMeshPro)")]
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
        UpdateUI();

        // 最初は必ず非表示
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameOverPanelが未設定！");
        }

        StartCoroutine(StartCountdown());
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

    IEnumerator StartCountdown()
    {
        isGameStarted = false;

        ShowCountdown("3");
        yield return new WaitForSeconds(1f);

        ShowCountdown("2");
        yield return new WaitForSeconds(1f);

        ShowCountdown("1");
        yield return new WaitForSeconds(1f);

        ShowCountdown("START!");
        yield return new WaitForSeconds(1f);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);

        isGameStarted = true;
    }

    void ShowCountdown(string text)
    {
        Debug.Log(text);

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            countdownText.text = text;
        }
        else
        {
            Debug.LogWarning("CountdownText未設定");
        }
    }

    void GameOver()
    {
        isGameStarted = false;

        Debug.Log("Finish!!");
        Debug.Log("最終スコア: " + score);

        // パネル強制表示
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogWarning("GameOverPanel未設定だから表示されない");
        }

        // スコア表示
        if (resultText != null)
        {
            resultText.text = "SCORE: " + score;
        }
        else
        {
            Debug.LogWarning("ResultText未設定");
        }
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
            scoreText.text = "Score: " + score;

        if (timeText != null)
            timeText.text = "Time: " + Mathf.CeilToInt(time);
    }
}
