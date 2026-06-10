using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("銃")]
    public Transform gunTransform;

    [Header("感度")]
    public float mouseSensitivity = 100f;

    [Header("銃回転速度")]
    public float gunRotateSpeed = 3f;

    float gunYaw = 0f;
    float gunPitch = 0f;

    // リコイル
    float recoilOffset = 0f;
    public float recoilRecoverySpeed = 8f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (gunTransform != null)
        {
            Vector3 rot = gunTransform.localEulerAngles;
            gunYaw = rot.y;
            gunPitch = rot.x;
        }
    }

    void Update()
    {
        float mouseX =
            Input.GetAxis("Mouse X") *
            mouseSensitivity *
            Time.deltaTime;

        float mouseY =
            Input.GetAxis("Mouse Y") *
            mouseSensitivity *
            Time.deltaTime;

        gunYaw += mouseX * gunRotateSpeed;
        gunPitch -= mouseY * gunRotateSpeed;

        gunPitch -= recoilOffset;

        gunPitch = Mathf.Clamp(
            gunPitch,
            -45f,
            45f
        );

        if (gunTransform != null)
        {
            gunTransform.localRotation =
                Quaternion.Euler(
                    gunPitch,
                    gunYaw,
                    0f
                );
        }

        recoilOffset = Mathf.Lerp(
            recoilOffset,
            0f,
            Time.deltaTime *
            recoilRecoverySpeed
        );
    }

    public void AddRecoil(float amount)
    {
        recoilOffset += amount;
    }
}
