using UnityEngine;

public class GunMove : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.right * 2f * Time.deltaTime;
    }
}