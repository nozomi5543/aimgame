using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{
    public Target[] targets; // 9個入れる
    public float interval = 2f;
    public int showCount = 4; // 4つ出す

    void Start()
    {
        // 最初は全部消す
        foreach (Target t in targets)
        {
            if (t != null)
                t.Hide();
        }

        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        while (true)
        {
            if (GameManager.instance != null && GameManager.instance.isGameStarted)
            {
                // 全部消す
                foreach (Target t in targets)
                {
                    if (t != null)
                        t.Hide();
                }

                // ランダム順に並べ替え
                List<Target> shuffled = new List<Target>(targets);

                for (int i = 0; i < shuffled.Count; i++)
                {
                    Target temp = shuffled[i];
                    int randomIndex = Random.Range(i, shuffled.Count);
                    shuffled[i] = shuffled[randomIndex];
                    shuffled[randomIndex] = temp;
                }

                // 最初の4つを表示
                for (int i = 0; i < showCount; i++)
                {
                    if (shuffled[i] != null)
                        shuffled[i].Show();
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }
}