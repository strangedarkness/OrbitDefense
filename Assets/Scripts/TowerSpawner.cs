using System.Collections;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [Header("Tower Prefabs")]
    public GameObject basicTowerPrefab;
    public GameObject rapidTowerPrefab;
    public GameObject heavyTowerPrefab;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // 5秒生成 Basic Tower
        yield return new WaitForSeconds(5f);
        SpawnTower(basicTowerPrefab, new Vector3(-5f, 0f, 5f));

        // 再等5秒
        yield return new WaitForSeconds(5f);
        SpawnTower(rapidTowerPrefab, new Vector3(5f, 0f, 5f));

        // 再等5秒
        yield return new WaitForSeconds(5f);
        SpawnTower(heavyTowerPrefab, new Vector3(5f, 0f, -5f));
    }

    private void SpawnTower(GameObject towerPrefab, Vector3 pos)
    {
        if (towerPrefab == null)
        {
            Debug.LogWarning("Tower prefab is missing!");
            return;
        }

        Instantiate(towerPrefab, pos, Quaternion.identity);
    }
}