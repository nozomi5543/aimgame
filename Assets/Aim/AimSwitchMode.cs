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

    [Header("UI")]
    public TMP_Text buttonText;

    private bool isProcessing = false;
    private bool isMouseMode = true;

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
            buttonText.text = "SwitchMode [Now Mouse] (M)";
        }
        else
        {
            mouseLook.enabled = false;
            mocopiGunLook.enabled = true;
            buttonText.text = "SwitchMode [Now MotionCapture] (M)";
        }

        yield return new WaitForSeconds(1f);

        isProcessing = false;
    }
}
