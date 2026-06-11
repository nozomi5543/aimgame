using UnityEngine;

public class StartButton : MonoBehaviour
{
    private bool used = false;

    public void Hit()
    {
        if (used) return;

        used = true;

        Debug.Log("ゲームスタート");

        GameManager.instance.StartGame();

        gameObject.SetActive(false);
    }
}