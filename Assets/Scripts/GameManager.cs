using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Enemy Count")]
    public int totalEnemies;
    private int spawnedEnemies = 0;
    private int aliveEnemies = 0;

    [Header("Base Health")]
    public int baseMaxHP = 10;
    private int baseHP;

    [Header("UI")]
    public TMP_Text enemyText;
    public TMP_Text baseText;

    void Awake()
    {
        Instance = this;
        baseHP = baseMaxHP;
        UpdateUI();
    }

    public void SetTotalEnemiesToSpawn(int total)
    {
        totalEnemies = total;
        UpdateUI();
    }

    public void RegisterEnemySpawn()
    {
        spawnedEnemies++;
        aliveEnemies++;
        UpdateUI();
    }

    public void RegisterEnemyDeath()
    {
        aliveEnemies--;
        UpdateUI();

        if (spawnedEnemies >= totalEnemies && aliveEnemies <= 0)
        {
            Debug.Log("Level Complete!");
        }
    }

    public void BaseTakeDamage(int damage)
    {
        baseHP -= damage;

        if (baseHP <= 0)
        {
            baseHP = 0;
            Debug.Log("Game Over");
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (enemyText != null)
        {
            enemyText.text = "Enemies: " + spawnedEnemies + "/" + totalEnemies +
                             "   Alive: " + aliveEnemies;
        }

        if (baseText != null)
        {
            baseText.text = "Base HP: " + baseHP + "/" + baseMaxHP;
        }
    }
}