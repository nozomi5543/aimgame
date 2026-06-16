using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool hasHit = false;

    [SerializeField]
    private float centerRange = 0.25f;

    [Header("中央ヒット時の倍率")]
    [SerializeField]
    private int turbo = 2;

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
            Debug.Log("ターゲット:" + target.name);

            // 真ん中に当たったかどうか
            if (IsCenterHit(collision))
            {
                target.HitCenter(turbo);
            }
            else
            {
                target.Hit();
            }

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

    public bool IsCenterHit(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        
        Vector3 center = transform.position;
        
        float distance = Vector3.Distance(contact.point, center);
        if (distance <= centerRange)
        {
            Debug.Log("中央付近にヒット");
            return true;
        }

        return false;
    }
}