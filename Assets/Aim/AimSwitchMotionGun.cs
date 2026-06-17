using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static AimUISliderShortcut;

public class AimSwitchMotionGun : MonoBehaviour
{
    [SerializeField]
    private GameObject leftGun;

    [SerializeField]
    private GameObject rightGun;

    public enum GunSelectMode
    {
        Left,
        Right
    }

    [Header("èeÇç∂âEÇ«ÇøÇÁÇÃéËÇ…éùÇΩÇπÇÈÇ©")]
    [SerializeField] private GunSelectMode gunSelectMode = GunSelectMode.Right;

    private bool isProcessing = false;

    void Start()
    {
        if (gunSelectMode == GunSelectMode.Left)
        {
            leftGun.SetActive(true);
            rightGun.SetActive(false);
        }
        else
        {
            leftGun.SetActive(false);
            rightGun.SetActive(true);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isProcessing)
            {
                gunSelectMode = GunSelectMode.Left;
                Execute();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isProcessing)
            {
                gunSelectMode = GunSelectMode.Right;
                Execute();
            }
        }
    }

    public void OnClickEvent()
    {
        if (!isProcessing)
        {
            Execute();
        }
    }

    public void Execute()
    {
        if (isProcessing)
            return;

        StartCoroutine(Process());
    }

    IEnumerator Process()
    {
        isProcessing = true;

        if (gunSelectMode == GunSelectMode.Left)
        {
            leftGun.SetActive(true);
            rightGun.SetActive(false);
        }
        else
        {
            leftGun.SetActive(false);
            rightGun.SetActive(true);
        }

        yield return new WaitForSeconds(1f);

        isProcessing = false;
    }
}
