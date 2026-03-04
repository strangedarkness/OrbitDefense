using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 30;
    public float spawnDelay = 0.8f;

    public Transform spawnPoint;
    public Transform[] waypoints;

    void Start()
    {
        // 告诉 GameManager 这一关总共有多少怪
        GameManager.Instance.SetTotalEnemiesToSpawn(enemyCount);

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // 设置敌人路径
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.waypoints = waypoints;
            }

            // 更新怪物生成数量
            GameManager.Instance.RegisterEnemySpawn();

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}