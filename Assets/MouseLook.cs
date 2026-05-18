using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    // リコイル用
    float recoilOffset = 0f;
    public float recoilRecoverySpeed = 8f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 通常の視点操作
        xRotation -= mouseY;

        // リコイルは「オフセット」として加える
        float finalRotation = xRotation - recoilOffset;
        finalRotation = Mathf.Clamp(finalRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(finalRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // リコイルを戻す
        recoilOffset = Mathf.Lerp(recoilOffset, 0f, Time.deltaTime * recoilRecoverySpeed);
    }

    // Gunから呼ぶ用
    public void AddRecoil(float amount)
    {
        recoilOffset += amount;
    }
}