using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AimInit : MonoBehaviour
{
    private bool isProcessing = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
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

        AimEventDispatcher.Fire("initPos");

        yield return new WaitForSeconds(1f);

        isProcessing = false;
    }
}
