using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AimRestart : MonoBehaviour
{
    private bool isProcessing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
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

        GameManager.instance.RestartGame();

        yield return new WaitForSeconds(1f);

        isProcessing = false;
    }
}
