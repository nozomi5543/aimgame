using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("音")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    public void Show()
    {
        gameObject.SetActive(true);
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

        // 的を消す
        Hide();
    }

    void OnMouseDown()
    {
        Hit();
    }
}