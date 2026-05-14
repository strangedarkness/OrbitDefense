using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildSlotGenerator : MonoBehaviour
{
    public GameObject buildSlotPrefab;

    void Start()
    {
        Vector3[] positions = GetBuildSlotPositions();

        foreach (Vector3 pos in positions)
        {
            Instantiate(buildSlotPrefab, pos, Quaternion.identity);
        }
    }

    Vector3[] GetBuildSlotPositions()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Level1")
        {
            return new Vector3[]
            {
                new Vector3(-5f, 0f, 5f),
                new Vector3(-5f, 0f, -5f),
                new Vector3(5f, 0f, 5f),
                new Vector3(5f, 0f, -5f),
            };
        }

        if (sceneName == "Level2")
        {
            return new Vector3[]
            {
                new Vector3(-2.5f, 0f, 2.5f),
                new Vector3(-7.5f, 0f, 2.5f),
                new Vector3(2.5f, 0f, 2.5f),
                new Vector3(7.5f, 0f, 2.5f),
                new Vector3(-2.5f, 0f, -2.5f),
                new Vector3(-7.5f, 0f, -2.5f),
                new Vector3(2.5f, 0f, -2.5f),
                new Vector3(7.5f, 0f, -2.5f),
            };
        }

        if (sceneName == "Level3")
        {
            return new Vector3[]
            {
                new Vector3(-2.5f, 0f, 2.5f),
                new Vector3(-7.5f, 0f, 2.5f),
                new Vector3(2.5f, 0f, 2.5f),
                new Vector3(7.5f, 0f, 2.5f),
                new Vector3(-2.5f, 0f, -2.5f),
                new Vector3(-7.5f, 0f, -2.5f),
                new Vector3(2.5f, 0f, -2.5f),
                new Vector3(7.5f, 0f, -2.5f),
                new Vector3(-2.5f, 0f, -7.5f),
                new Vector3(-7.5f, 0f, -7.5f),
                new Vector3(2.5f, 0f, -7.5f),
                new Vector3(7.5f, 0f, -7.5f),
            };
        }

        return new Vector3[0];
    }
}