using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{
    public Target[] targets;
    public float interval = 2f;
    public int showCount = 4;

    void Start()
    {
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("targets が設定されていない！");
            return;
        }

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

                // リスト化
                List<Target> shuffled = new List<Target>(targets);

                // シャッフル
                for (int i = 0; i < shuffled.Count; i++)
                {
                    Target temp = shuffled[i];
                    int randomIndex = Random.Range(i, shuffled.Count);
                    shuffled[i] = shuffled[randomIndex];
                    shuffled[randomIndex] = temp;
                }

                // 表示数を安全に制限
                int max = Mathf.Min(showCount, shuffled.Count);

                for (int i = 0; i < max; i++)
                {
                    if (shuffled[i] != null)
                        shuffled[i].Show();
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }
}