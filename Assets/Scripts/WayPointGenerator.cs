using UnityEngine;
using UnityEngine.SceneManagement;

public class WaypointGenerator : MonoBehaviour
{
    [Header("References")]
    public EnemySpawner enemySpawner;
    public Transform spawnPointModel;
    public Transform baseModel;

    void Awake()
    {
        LevelPathData data = GetLevelPathData();

        GameObject spawn = new GameObject("SpawnPoint");
        spawn.transform.position = data.spawnPoint;

        Transform[] generatedWaypoints = new Transform[data.waypoints.Length];

        for (int i = 0; i < data.waypoints.Length; i++)
        {
            GameObject wp = new GameObject("Waypoint_" + i);
            wp.transform.position = data.waypoints[i];
            generatedWaypoints[i] = wp.transform;
        }

        enemySpawner.spawnPoint = spawn.transform;
        enemySpawner.waypoints = generatedWaypoints;

        if (spawnPointModel != null)
        {
            spawnPointModel.position = data.spawnPoint;
        }

        if (baseModel != null)
        {
            baseModel.position = data.waypoints[data.waypoints.Length - 1];
        }
    }

    class LevelPathData
    {
        public Vector3 spawnPoint;
        public Vector3[] waypoints;

        public LevelPathData(Vector3 spawn, Vector3[] wps)
        {
            spawnPoint = spawn;
            waypoints = wps;
        }
    }

    LevelPathData GetLevelPathData()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Level1")
        {
            return new LevelPathData(
                new Vector3(-10f, 1f, 10f),
                new Vector3[]
                {
                    new Vector3(-10f, 1f, 0f),
                    new Vector3(10f, 1f, 0f),
                    new Vector3(10f, 1f, -10f)
                }
            );
        }

        if (sceneName == "Level2")
        {
            return new LevelPathData(
                new Vector3(-12f, 1f, 12f),
                new Vector3[]
                {
                    new Vector3(-9f, 1f, 9f),
                    new Vector3(-9f, 1f, 0f),
                    new Vector3(0f, 1f, 0f),
                    new Vector3(0f, 1f, -10f),
                    new Vector3(9f, 1f, -7f),
                    new Vector3(9f, 1f, 9f),
                    new Vector3(12f, 1f, 12f)
                }
            );
        }

        if (sceneName == "Level3")
        {
            return new LevelPathData(
                new Vector3(-12f, 1f, 12f),
                new Vector3[]
                {
                    new Vector3(-10f, 1f, 8f),
                    new Vector3(-9f, 1f, -1f),
                    new Vector3(-1f, 1f, -1f),
                    new Vector3(-1f, 1f, -10f),
                    new Vector3(6f, 1f, -8f),
                    new Vector3(7f, 1f, 1f),
		    new Vector3(14f, 1f, 2f),
                    new Vector3(12f, 1f, 12f)
                }
            );
        }
        return new LevelPathData(
            Vector3.zero,
            new Vector3[] { Vector3.zero }
        );
    }
}