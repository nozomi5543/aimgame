using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;   // ←超重要（二重防止）
        hasHit = true;

        Debug.Log("当たった");

        // まずTargetだけ拾う（安全版）
        Target target = collision.collider.GetComponentInParent<Target>();
        if (target != null)
        {
            target.Hit();
            Destroy(gameObject);
            return;
        }

        // StartButtonも同じく安全版
        StartButton startButton = collision.collider.GetComponentInParent<StartButton>();
        if (startButton != null)
        {
            startButton.Hit();
        }

        Destroy(gameObject);
    }
}