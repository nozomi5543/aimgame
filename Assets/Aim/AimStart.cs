using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class AimStart : MonoBehaviour
{
    private bool isProcessing = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
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

        GameManager.instance.StartGame();

        yield return new WaitForSeconds(1f);

        isProcessing = false;
    }
}
