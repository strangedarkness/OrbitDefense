using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("Targeting")]
    public float range = 6f;
    public string enemyTag = "Enemy";

    [Header("Fire")]
    public float fireRate = 1.0f;
    public float damage = 5f;

    public Transform firePoint;
    public Bullet bulletPrefab;

    private float fireTimer;
    private Transform target;

    private void Start()
    {
        if (firePoint == null)
            firePoint = transform;

        InvokeRepeating(nameof(UpdateTarget), 0f, 0.2f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortest = Mathf.Infinity;
        GameObject nearest = null;

        foreach (var e in enemies)
        {
            float d = Vector3.Distance(transform.position, e.transform.position);

            if (d < shortest)
            {
                shortest = d;
                nearest = e;
            }
        }

        if (nearest != null && shortest <= range)
            target = nearest.transform;
        else
            target = null;
    }

    private void Update()
    {
        if (target == null) return;

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = 1f / fireRate;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null) return;

        Bullet b = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        b.SetTarget(target);

        // 把塔的伤害传给子弹
        b.damage = damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}