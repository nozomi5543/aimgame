using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [Header("音")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    [Header("スコア")]
    [SerializeField] private int normalScoreValue = 1;
    [SerializeField] private int bonusScoreValue = 10;

    [Header("見た目")]
    [SerializeField] private Renderer targetRenderer;

    [Header("マテリアル")]
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material bonusMaterial;

    [Header("点滅設定")]
    [SerializeField] private float blinkDuration = 0.5f;
    [SerializeField] private float blinkInterval = 0.08f;

    private Coroutine blinkCoroutine;
    private bool isHit = false;

    private void Start()
    {
        if (targetRenderer == null)
        {
            targetRenderer = GetComponent<Renderer>();
        }

        UpdateMaterial();
    }

    private void Update()
    {
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        if (targetRenderer == null) return;
        if (GameManager.instance == null) return;

        if (GameManager.instance.IsBonusTime())
        {
            if (bonusMaterial != null)
                targetRenderer.material = bonusMaterial;
        }
        else
        {
            if (normalMaterial != null)
                targetRenderer.material = normalMaterial;
        }
    }

    public void Hit()
    {
        HitRtn();
    }

    public void HitCenter(int turbo)
    {
        HitRtn(turbo);
    }

    private void HitRtn(int turbo = 1)
    {
        if (isHit) return;
        isHit = true;

        if (GameManager.instance == null) return;
        if (!GameManager.instance.isGameStarted) return;

        Debug.Log("BlinkEffect:" + this.name);

        int addScore = normalScoreValue * turbo;

        if (GameManager.instance.IsBonusTime())
        {
            addScore = bonusScoreValue * turbo;
        }

        GameManager.instance.AddScore(addScore);

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        blinkCoroutine = StartCoroutine(BlinkEffect());

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
        isHit = false;

        if (targetRenderer != null)
        {
            targetRenderer.enabled = true;
        }

        UpdateMaterial();
    }
}