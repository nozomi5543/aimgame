using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Transform firePoint;

    void Update()
    {
        Vector3 targetPos =
            firePoint.position +
            firePoint.forward * 100f;

        Vector3 screenPos =
            mainCamera.WorldToScreenPoint(targetPos);

        transform.position = screenPos;
    }
}
