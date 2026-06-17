using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using TMPro;

public class AimSwitchMode : MonoBehaviour
{
    [SerializeField]
    private MouseLook mouseLook;

    [SerializeField]
    private MocopiGunLook mocopiGunLook;

    [SerializeField]
    private GameObject mouseGun;

    [SerializeField]
    private GameObject mocopiModel;

    [Header("UI")]
    public TMP_Text buttonText;

    private bool isProcessing = false;
    private bool isMouseMode = true;

    void Start()
    {
        // 初期状態をマウスモードに設定
        mouseLook.enabled = true;
        mocopiGunLook.enabled = false;
        mouseGun.SetActive(true);
        mocopiModel.SetActive(false);
        buttonText.text = "SwitchMode [Now Mouse] (M)";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isProcessing)
            {
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

        isMouseMode = !isMouseMode;
        if (isMouseMode)
        {
            mouseLook.enabled = true;
            mocopiGunLook.enabled = false;
            mouseGun.SetActive(true);
            mocopiModel.SetActive(false);
            buttonText.text = "SwitchMode [Now Mouse] (M)";
        }
        else
        {
            mouseLook.enabled = false;
            mocopiGunLook.enabled = true;
            mouseGun.SetActive(false);
            mocopiModel.SetActive(true);
            buttonText.text = "SwitchMode [Now MotionCapture] (M)";
        }

        yield return new WaitForSeconds(1f);

        isProcessing = false;
    }
}
