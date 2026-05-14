using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject basicEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject swarmEnemyPrefab;
    public GameObject tankEnemyPrefab;
    public GameObject bossEnemyPrefab;
    public TMP_Text prepareText;
    public float prepareTime = 10f;
    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public Transform[] waypoints;

    public class WaveData
    {
        public string enemyType;
        public int count;
        public float spawnDelay;
        public float waitAfterWave;

        public WaveData(string enemyType, int count, float spawnDelay, float waitAfterWave)
        {
            this.enemyType = enemyType;
            this.count = count;
            this.spawnDelay = spawnDelay;
            this.waitAfterWave = waitAfterWave;
        }
    }

    void Start()
    {
        WaveData[] waves = GetLevelWaves();

        int total = 0;
        foreach (WaveData wave in waves)
        {
            total += wave.count;
        }

        GameManager.Instance.SetTotalEnemiesToSpawn(total);

        StartCoroutine(BeginLevel(waves));
    }

    IEnumerator BeginLevel(WaveData[] waves)
    {
        float timer = prepareTime;

        while (timer > 0)
        {
            prepareText.text = "Prepare Time: " + Mathf.Ceil(timer);

            timer -= Time.deltaTime;

            yield return null;
        }

        prepareText.gameObject.SetActive(false);

        StartCoroutine(SpawnSequence(waves));
    }

    WaveData[] GetLevelWaves()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Level1")
        {
            return new WaveData[]
            {
                new WaveData("Basic", 5, 0.5f, 2f),
                new WaveData("Fast", 5, 0.4f, 2f),
                new WaveData("Swarm", 8, 0.2f, 3f),
                new WaveData("Tank", 2, 1.0f, 2f),
                new WaveData("Boss", 1, 0.5f, 0f)
            };
        }

        if (sceneName == "Level2")
        {
            return new WaveData[]
            {
                new WaveData("Basic", 8, 0.4f, 2f),
                new WaveData("Swarm", 12, 0.15f, 3f),
                new WaveData("Fast", 8, 0.3f, 2f),
                new WaveData("Tank", 3, 0.8f, 2f),
                new WaveData("Boss", 1, 0.5f, 0f)
            };
        }

        if (sceneName == "Level3")
        {
            return new WaveData[]
            {
                new WaveData("Swarm", 15, 0.12f, 2f),
                new WaveData("Fast", 12, 0.25f, 2f),
                new WaveData("Tank", 5, 0.7f, 3f),
                new WaveData("Boss", 2, 1.0f, 0f)
            };
        }

        return new WaveData[]
        {
            new WaveData("Basic", 5, 0.5f, 0f)
        };
    }

    IEnumerator SpawnSequence(WaveData[] waves)
    {
        foreach (WaveData wave in waves)
        {
            yield return StartCoroutine(SpawnWave(wave));
            yield return new WaitForSeconds(wave.waitAfterWave);
        }
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        GameObject prefab = GetEnemyPrefab(wave.enemyType);

        for (int i = 0; i < wave.count; i++)
        {
            GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.waypoints = waypoints;
            }

            GameManager.Instance.RegisterEnemySpawn();

            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }

    GameObject GetEnemyPrefab(string enemyType)
    {
        switch (enemyType)
        {
            case "Basic":
                return basicEnemyPrefab;

            case "Fast":
                return fastEnemyPrefab;

            case "Swarm":
                return swarmEnemyPrefab;

            case "Tank":
                return tankEnemyPrefab;

            case "Boss":
                return bossEnemyPrefab;

            default:
                return basicEnemyPrefab;
        }
    }
}