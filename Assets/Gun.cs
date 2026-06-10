using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    [Header("弾")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 70f;

    [Header("銃モデル")]
    [SerializeField] private Transform gunModel;

    [Header("銃リコイル")]
    [SerializeField] private float gunKickBack = 0.08f;
    [SerializeField] private float gunReturnSpeed = 10f;
    [SerializeField] private float gunSnappiness = 15f;

    private Vector3 gunCurrentPosition;
    private Vector3 gunTargetPosition;
    private Vector3 gunStartPosition;

    [Header("カメラリコイル")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float recoilUp = 2f;

    [Header("音")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunShotSound;

    void Start()
    {
        // gunModel未設定なら自分
        if (gunModel == null)
        {
            gunModel = transform;
        }

        // camera未設定ならMainCamera
        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }

        // 銃が真っすぐ向くように、positionのxを変更 0.2⇒0 BY FUKE
        gunStartPosition = gunModel.localPosition;
    }

    void Update()
    {
        // 発射
        if (Input.GetMouseButtonDown(0))
        {
            // UI上なら撃たない
            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // ゲーム中じゃないなら撃たない
            if (GameManager.instance != null &&
                !GameManager.instance.isGameStarted)
            {
                return;
            }

            Shoot();
        }

        BackRecoil();
    }
    
    void BackRecoil()
    {
       // 銃リコイル戻し
        gunTargetPosition = Vector3.Lerp(
            gunTargetPosition,
            Vector3.zero,
            gunReturnSpeed * Time.deltaTime
        );

        gunCurrentPosition = Vector3.Lerp(
            gunCurrentPosition,
            gunTargetPosition,
            gunSnappiness * Time.deltaTime
        );

        gunModel.localPosition =
            gunStartPosition + gunCurrentPosition;

    }
    
    void Shoot()
    {
        Debug.Log("発射！");

        // 弾生成
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        // 弾発射
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // 銃声
        if (audioSource != null && gunShotSound != null)
        {
            audioSource.PlayOneShot(gunShotSound);
        }

        // リコイル
        Recoil();
    }

    void Recoil()
    {
        // 銃を後ろへ
        gunTargetPosition += new Vector3(
            0,
            0,
            -gunKickBack
        );
    }
}