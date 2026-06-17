using UnityEngine;
using UnityEngine.UI;
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

    [Header("BONUS TIME画像")]
    public Image bonusTimeImage;

    [Header("LED枠")]
    public GameObject[] ledLines;

    [Header("点滅間隔")]
    public float blinkInterval = 0.25f;

    [Header("メインライト")]
    public Light mainLight;

    [Header("通常明るさ")]
    public float normalIntensity = 1f;

    [Header("ボーナス時明るさ")]
    public float bonusIntensity = 0.2f;

    [Header("ボーナスタイム発光")]
    public Renderer[] bonusRenderers;

    [Header("通常発光")]
    public float normalEmission = 1f;

    [Header("ボーナス発光")]
    public float bonusEmission = 10f;

    private Coroutine frameBlinkCoroutine;

    void Start()
    {
        foreach (Target t in targets)
        {
            if (t != null)
                t.Hide();
        }

        if (bonusTimeImage != null)
        {
            bonusTimeImage.gameObject.SetActive(false);
        }

        if (mainLight != null)
        {
            mainLight.intensity = normalIntensity;
        }

        SetEmission(normalEmission);

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
            if (GameManager.instance != null &&
                !GameManager.instance.isGameStarted)
            {
                yield return null;
                continue;
            }

            // 的表示
            foreach (Target t in targets)
            {
                if (t != null)
                    t.Show();
            }

            float currentInterval = interval;

            // ボーナスタイム
            if (GameManager.instance != null &&
                GameManager.instance.IsBonusTime())
            {
                currentInterval = bonusInterval;

                // BONUS TIME表示
                if (bonusTimeImage != null)
                {
                    bonusTimeImage.enabled = true;
                    bonusTimeImage.gameObject.SetActive(true);
                }

                // マップ暗転
                if (mainLight != null)
                {
                    mainLight.intensity = bonusIntensity;
                }

                // 発光強化
                SetEmission(bonusEmission);

                // LED枠点滅開始
                if (frameBlinkCoroutine == null)
                {
                    frameBlinkCoroutine = StartCoroutine(BlinkFrame());
                }
            }
            else
            {
                // BONUS TIME非表示
                if (bonusTimeImage != null)
                {
                    bonusTimeImage.gameObject.SetActive(false);
                }

                // 明るさ戻す
                if (mainLight != null)
                {
                    mainLight.intensity = normalIntensity;
                }

                // 発光戻す
                SetEmission(normalEmission);

                // LED点滅停止
                if (frameBlinkCoroutine != null)
                {
                    StopCoroutine(frameBlinkCoroutine);
                    frameBlinkCoroutine = null;
                }

                // LED表示状態に戻す
                foreach (GameObject led in ledLines)
                {
                    if (led != null)
                    {
                        led.SetActive(true);
                    }
                }
            }

            yield return new WaitForSeconds(currentInterval);

            // 的を消す
            foreach (Target t in targets)
            {
                if (t != null)
                    t.Hide();
            }

            yield return new WaitForSeconds(changeDelay);

            MoveTargetsRandomly();
        }
    }

    IEnumerator BlinkFrame()
    {
        while (true)
        {
            foreach (GameObject led in ledLines)
            {
                if (led != null)
                {
                    led.SetActive(!led.activeSelf);
                }
            }

            yield return new WaitForSeconds(blinkInterval);
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

    void SetEmission(float intensity)
    {
        foreach (Renderer r in bonusRenderers)
        {
            if (r == null) continue;

            Material mat = r.material;

            mat.EnableKeyword("_EMISSION");

            Color emissionColor = Color.white * intensity;

            mat.SetColor("_EmissionColor", emissionColor);
        }
    }
}