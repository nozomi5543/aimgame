using UnityEngine;
using System.Collections;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;

    public float spawnInterval = 3f;
    public int spawnCount = 3;

    public Vector2 minXY = new Vector2(-5f, 1f);
    public Vector2 maxXY = new Vector2(5f, 3f);

    public float spawnZ = 58f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (GameManager.instance != null && GameManager.instance.isGameStarted)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    SpawnTarget();
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTarget()
    {
        if (targetPrefab == null) return;

        Vector3 pos = new Vector3(
            Random.Range(minXY.x, maxXY.x),
            Random.Range(minXY.y, maxXY.y),
            spawnZ
        );

        Instantiate(targetPrefab, pos, Quaternion.identity);
    }
}