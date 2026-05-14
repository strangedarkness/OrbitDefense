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

    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public Transform[] waypoints;

    [Header("Prepare Time")]
    public TMP_Text prepareText;
    public float prepareTime = 10f;

    public class EnemySpawnData
    {
        public string enemyType;
        public int count;

        public EnemySpawnData(string enemyType, int count)
        {
            this.enemyType = enemyType;
            this.count = count;
        }
    }

    public class WaveData
    {
        public EnemySpawnData[] enemies;
        public float spawnDelay;
        public float waitAfterWave;

        public WaveData(float spawnDelay, float waitAfterWave, EnemySpawnData[] enemies)
        {
            this.spawnDelay = spawnDelay;
            this.waitAfterWave = waitAfterWave;
            this.enemies = enemies;
        }
    }

    void Start()
    {
        WaveData[] waves = GetLevelWaves();

        int total = 0;
        foreach (WaveData wave in waves)
        {
            foreach (EnemySpawnData enemy in wave.enemies)
            {
                total += enemy.count;
            }
        }

        GameManager.Instance.SetTotalEnemiesToSpawn(total);

        StartCoroutine(BeginLevel(waves));
    }

    IEnumerator BeginLevel(WaveData[] waves)
    {
        float timer = prepareTime;

        while (timer > 0)
        {
            if (prepareText != null)
            {
                prepareText.text = "Prepare Time: " + Mathf.Ceil(timer);
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        if (prepareText != null)
        {
            prepareText.gameObject.SetActive(false);
        }

        yield return StartCoroutine(SpawnSequence(waves));
    }

    IEnumerator SpawnSequence(WaveData[] waves)
    {
        for (int i = 0; i < waves.Length; i++)
        {
            yield return StartCoroutine(SpawnWave(waves[i]));

            if (i < waves.Length - 1)
            {
                yield return new WaitForSeconds(waves[i].waitAfterWave);
            }
        }
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        foreach (EnemySpawnData enemyData in wave.enemies)
        {
            for (int i = 0; i < enemyData.count; i++)
            {
                SpawnEnemy(enemyData.enemyType);
                yield return new WaitForSeconds(wave.spawnDelay);
            }
        }
    }

    void SpawnEnemy(string enemyType)
    {
        GameObject prefab = GetEnemyPrefab(enemyType);

        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.waypoints = waypoints;
        }

        GameManager.Instance.RegisterEnemySpawn();
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

    WaveData[] GetLevelWaves()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Level1")
        {
            return new WaveData[]
            {
                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Basic", 5)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Fast", 3)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Swarm", 12)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Tank", 5)
                }),

                new WaveData(0.1f, 0f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Basic", 5),
		    new EnemySpawnData("Swarm", 10),
                    new EnemySpawnData("Tank", 5)
                })
            };
        }

        if (sceneName == "Level2")
        {
            return new WaveData[]
            {
                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Basic", 6),
                    new EnemySpawnData("Fast", 6)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Basic", 5),
                    new EnemySpawnData("Swarm", 8)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Fast", 6),
                    new EnemySpawnData("Swarm", 8)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Basic", 5),
                    new EnemySpawnData("Tank", 5)
                }),

                new WaveData(0.1f, 0f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Boss", 1),
                    new EnemySpawnData("Basic", 10),
                    new EnemySpawnData("Fast", 10)
                })
            };
        }

        if (sceneName == "Level3")
        {
            return new WaveData[]
            {
                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Basic", 15),
                    new EnemySpawnData("Fast", 5),
                    new EnemySpawnData("Swarm", 5)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Fast", 8),
                    new EnemySpawnData("Swarm", 28)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Basic", 15),
                    new EnemySpawnData("Tank", 10)
                }),

                new WaveData(0.1f, 5f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Swarm", 30),
                    new EnemySpawnData("Tank", 15)
                }),

                new WaveData(0.1f, 0f, new EnemySpawnData[]
                {
                    new EnemySpawnData("Boss", 3),
                    new EnemySpawnData("Tank", 15),
                    new EnemySpawnData("Swarm", 30)
                })
            };
        }

        return new WaveData[]
        {
            new WaveData(0.1f, 0f, new EnemySpawnData[]
            {
                new EnemySpawnData("Basic", 10)
            })
        };
    }
}