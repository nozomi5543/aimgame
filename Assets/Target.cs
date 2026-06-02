using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    [Header("音")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    [Header("電子表示")]
    [SerializeField] private TextMeshPro hitText;

    private void Start()
    {
        if (hitText != null)
        {
            hitText.text = "READY";
            hitText.color = Color.cyan;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (hitText != null)
        {
            hitText.text = "READY";
            hitText.color = Color.cyan;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Hit()
    {
        if (GameManager.instance == null) return;
        if (!GameManager.instance.isGameStarted) return;

        // スコア追加
        GameManager.instance.AddScore(1);

        // ヒット音
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // 電子表示
        if (hitText != null)
        {
            hitText.text = "HIT!";
            hitText.color = Color.green;
        }

        // 少し待って消す
        Invoke(nameof(Hide), 0.5f);
    }

    private void OnMouseDown()
    {
        Hit();
    }
}