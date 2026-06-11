using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [Header("音")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    [Header("スコア")]
    [SerializeField] private int scoreValue = 1;

    [Header("見た目")]
    [SerializeField] private Renderer targetRenderer;

    [Header("点滅設定")]
    [SerializeField] private float blinkDuration = 0.5f;
    [SerializeField] private float blinkInterval = 0.08f;

    private Coroutine blinkCoroutine;

    private bool isHit = false; // ⭐これ追加（超重要）

    private void Start()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }
    }

    public void Hit()
    {
        // 💥 ここが核心（これで完全防止）
        if (isHit) return;
        isHit = true;

        if (GameManager.instance == null) return;
        if (!GameManager.instance.isGameStarted) return;

        // スコア加算（1回だけ）
        GameManager.instance.AddScore(scoreValue);

        // 音
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // 点滅
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(BlinkEffect());

        // 消す
        Invoke(nameof(Hide), 0.6f);
    }

    private IEnumerator BlinkEffect()
    {
        float timer = 0f;
        bool state = true;

        while (timer < blinkDuration)
        {
            if (targetRenderer != null)
            {
                state = !state;
                targetRenderer.enabled = state;
            }

            timer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        if (targetRenderer != null)
        {
            targetRenderer.enabled = true;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        // 🔥 復活対策（超重要）
        isHit = false;

        if (targetRenderer != null)
            targetRenderer.enabled = true;
    }

    private void OnMouseDown()
    {
        Hit();
    }
}