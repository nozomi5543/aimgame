using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{
    [Header("的9個")]
    public Target[] targets;

    [Header("配置場所9個")]
    public Transform[] basePoints;

    [Header("通常表示時間")]
    public float interval = 2f;

    [Header("ボーナスタイム表示時間")]
    public float bonusInterval = 1f;

    [Header("切り替え待機時間")]
    public float changeDelay = 0.5f;

    void Start()
    {
        foreach (Target t in targets)
        {
            if (t != null)
                t.Hide();
        }

        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        while (GameManager.instance == null ||
               !GameManager.instance.isGameStarted)
        {
            yield return null;
        }

        while (true)
        {
            // ゲーム終了したら停止
            if (GameManager.instance != null &&
                !GameManager.instance.isGameStarted)
            {
                yield return null;
                continue;
            }

            // 的を表示
            foreach (Target t in targets)
            {
                if (t != null)
                    t.Show();
            }

            // ボーナスタイム判定
            float currentInterval = interval;

            if (GameManager.instance != null &&
                GameManager.instance.IsBonusTime())
            {
                currentInterval = bonusInterval;
            }

            // 表示時間
            yield return new WaitForSeconds(currentInterval);

            // 的を消す
            foreach (Target t in targets)
            {
                if (t != null)
                    t.Hide();
            }

            // 切り替え待機
            yield return new WaitForSeconds(changeDelay);

            // ランダム移動
            MoveTargetsRandomly();
        }
    }

    void MoveTargetsRandomly()
    {
        List<Transform> points = new List<Transform>(basePoints);

        for (int i = 0; i < points.Count; i++)
        {
            int randomIndex = Random.Range(i, points.Count);

            Transform temp = points[i];
            points[i] = points[randomIndex];
            points[randomIndex] = temp;
        }

        for (int i = 0; i < targets.Length && i < points.Count; i++)
        {
            if (targets[i] != null)
            {
                targets[i].transform.position = points[i].position;
            }
        }
    }
}