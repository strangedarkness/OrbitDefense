using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 20;
    public float spawnDelay = 1f;

    public Transform spawnPoint;
    public Transform[] waypoints;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            movement.waypoints = waypoints;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}