using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 10f;
    private float hp;

    private void Awake()
    {
        hp = maxHP;
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0f)
        {
            GameManager.Instance.RegisterEnemyDeath();	
            Destroy(gameObject);
        }
    }
}