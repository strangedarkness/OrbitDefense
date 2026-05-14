using System.Collections;
using UnityEngine;

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

    void Start()
    {
        // 5 Basic + 5 Fast + 9 Swarm + 2 Tank + 1 Boss = 22
        GameManager.Instance.SetTotalEnemiesToSpawn(22);

        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        // 5 Basic enemies
        yield return StartCoroutine(SpawnWave(basicEnemyPrefab, 5, 0.1f));

        yield return new WaitForSeconds(2f);

        // 5 Fast enemies
        yield return StartCoroutine(SpawnWave(fastEnemyPrefab, 5, 0.1f));

        yield return new WaitForSeconds(3f);

        // 9 Swarm enemies
        yield return StartCoroutine(SpawnWave(swarmEnemyPrefab, 9, 0.1f));

        yield return new WaitForSeconds(5f);

        // 2 Tank enemies
        yield return StartCoroutine(SpawnWave(tankEnemyPrefab, 2, 0.5f));

        yield return new WaitForSeconds(2f);

        // 1 Boss enemy
        yield return StartCoroutine(SpawnWave(bossEnemyPrefab, 1, 0.1f));
    }

    IEnumerator SpawnWave(GameObject enemyPrefab, int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.waypoints = waypoints;
            }

            GameManager.Instance.RegisterEnemySpawn();

            yield return new WaitForSeconds(delay);
        }
    }
}