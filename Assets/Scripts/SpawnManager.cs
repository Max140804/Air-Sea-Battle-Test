using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawn Points")]
    Transform[] spawnPoints;

    [Header("Settings")]
    [SerializeField] float spacing = 1.5f;
    [SerializeField] float checkDelay = 1f;

    void Start()
    {
        spawnPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (!GameManager.Instance.isGameOver)
        {
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

            if (GameManager.Instance.isGameOver)
                yield break;

            yield return new WaitForSeconds(checkDelay);

            SpawnWave();
        }
    }

    void SpawnWave()
    {
        if (GameManager.Instance.isGameOver) return;

        GameManager.Instance.NextWave();

        int formationSize = Random.value < 0.5f ? 3 : 5;
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        SpawnFormation(spawnPoint, formationSize);
    }

    void SpawnFormation(Transform point, int count)
    {
        Vector3[] pattern = GetFormationPattern(count);

        for (int i = 0; i < pattern.Length; i++)
        {
            Vector3 spawnPos = point.position + pattern[i];
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector3[] GetFormationPattern(int count)
    {
        if (count == 3)
        {
            return new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(-spacing, -spacing, 0),
                new Vector3(-spacing, spacing, 0)
            };
        }

        return new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(-spacing, -spacing, 0),
            new Vector3(-spacing, spacing, 0),
            new Vector3(-spacing * 2, -spacing * 2, 0),
            new Vector3(-spacing * 2, spacing * 2, 0)
        };
    }
}