using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 10f;
    private float hp;

    public GameObject healthBarObject;
    private HealthBar healthBar;

    private void Awake()
    {
        hp = maxHP;

        if (healthBarObject != null)
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();

            if (healthBar != null)
            {
                healthBar.SetHealth(hp, maxHP);
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        hp = Mathf.Clamp(hp, 0f, maxHP);

        if (healthBar != null)
        {
            healthBar.SetHealth(hp, maxHP);
        }

        if (hp <= 0f)
        {
            GameManager.Instance.RegisterEnemyDeath();
            Destroy(gameObject);
        }
    }
}