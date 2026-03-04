using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [Header("Targeting")]
    public float range = 6f;
    public string enemyTag = "Enemy";

    [Header("Fire")]
    public float fireRate = 1.0f; // 每秒几发
    public Transform firePoint;
    public Bullet bulletPrefab;

    private float fireTimer;
    private Transform target;

    private void Start()
    {
        // 如果你没手动指定 firePoint，就用塔自身位置
        if (firePoint == null) firePoint = transform;

        // 每 0.2 秒找一次最近敌人
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

        // 可选：让塔朝向敌人（俯视塔防也可以不转）
        // transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

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
    }

    // 在 Scene 里可视化攻击范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}