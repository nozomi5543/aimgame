using UnityEngine;

public class MocopiGunLook : MonoBehaviour
{
    [Header("銃のオブジェクト")]
    public Transform gunTransform;

    [Header("MOCOPIの右手ボーン")]
    public Transform rightHandTransform;

    [Header("感度調整")]
    public float yawSensitivity = 150f;
    public float pitchSensitivity = 150f;

    [Header("リコイル設定")]
    public float recoilRecoverySpeed = 8f;
    private float recoilOffset = 0f;

    private Vector3 lastPosition;
    private float currentYaw = 0f;
    private float currentPitch = 0f;

    void Start()
    {
        if (rightHandTransform != null)
        {
            lastPosition = rightHandTransform.position;
        }
    }

    void Update()
    {
        if (gunTransform != null && rightHandTransform != null)
        {
            // 1. 今回の移動ベクトルを算出（最新 - 前回）
            Vector3 deltaPosition = rightHandTransform.position - lastPosition;

            // 2. 移動量からYawとPitchの増分を計算
            // ローカル座標系で動かすため、移動ベクトルを銃の相対方向に変換
            float yawDelta = deltaPosition.x * yawSensitivity;
            float pitchDelta = deltaPosition.y * pitchSensitivity;

            // 3. 現在の角度に加算
            currentYaw += yawDelta;
            currentPitch += pitchDelta;
            
            // 4. 範囲制限
            currentYaw = Mathf.Clamp(currentYaw, -50f, 50f);
            currentPitch = Mathf.Clamp(currentPitch - recoilOffset, -45f, 30f);

            // 5. 回転適用
            gunTransform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

            // 6. 次のフレームのために現在の位置を保存
            lastPosition = rightHandTransform.position;
        }

        recoilOffset = Mathf.Lerp(recoilOffset, 0f, Time.deltaTime * recoilRecoverySpeed);
    }

    public void AddRecoil(float amount)
    {
        recoilOffset += amount;
    }
}