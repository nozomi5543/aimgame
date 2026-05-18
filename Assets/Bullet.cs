using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    void Start()
    {
        
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("当たった");

        Target target = collision.gameObject.GetComponent<Target>();

        if (target != null)
        {
            Debug.Log("当たった");
            target.Hit();
        }

    }
}
