using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{
    [Header("的9個")]
    public Target[] targets;

    [Header("配置場所9個")]
    public Transform[] basePoints;

    [Header("表示時間")]
    public float interval = 2f;

    [Header("切り替え待機時間")]
    public float changeDelay = 0.5f;

    void Start()
    {
        // 最初は全部消す
        foreach (Target t in targets)
        {
            if (t != null)
                t.Hide();
        }

        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        // スタート待ち
        while (GameManager.instance == null ||
               !GameManager.instance.isGameStarted)
        {
            yield return null;
        }

        while (true)
        {
            // 的を表示
            foreach (Target t in targets)
            {
                if (t != null)
                    t.Show();
            }

            // 表示時間
            yield return new WaitForSeconds(interval);

            // 全的を非表示
            foreach (Target t in targets)
            {
                if (t != null)
                    t.Hide();
            }

            // 0.5秒待機
            yield return new WaitForSeconds(changeDelay);

            // ランダム位置へ移動
            MoveTargetsRandomly();
        }
    }

    void MoveTargetsRandomly()
    {
        List<Transform> points = new List<Transform>(basePoints);

        // シャッフル
        for (int i = 0; i < points.Count; i++)
        {
            int randomIndex = Random.Range(i, points.Count);

            Transform temp = points[i];
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }

        // 的をランダムなマスへ配置
        for (int i = 0; i < targets.Length && i < points.Count; i++)
        {
            if (targets[i] != null)
            {
                targets[i].transform.position = points[i].position;
            }
        }
    }
}