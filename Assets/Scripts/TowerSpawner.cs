using System.Collections;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject towerPrefab;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // 5秒生成第一个塔
        yield return new WaitForSeconds(5f);
        SpawnTower(new Vector3(-5f, 0f, 5f));

        // 再等5秒 (10秒)
        yield return new WaitForSeconds(5f);
        SpawnTower(new Vector3(5f, 0f, 5f));

        // 再等5秒 (15秒)
        yield return new WaitForSeconds(5f);
        SpawnTower(new Vector3(5f, 0f, -5f));
    }

    private void SpawnTower(Vector3 pos)
    {
        Instantiate(towerPrefab, pos, Quaternion.identity);
    }
}