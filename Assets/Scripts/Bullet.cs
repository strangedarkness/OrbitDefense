using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 18f;
    private float damage = 5f;
    private float lifeTime = 3f;

    private Transform target;

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position);
        float distThisFrame = speed * Time.deltaTime;

        // 命中判定（足够近就算打到）
        if (dir.magnitude <= distThisFrame)
        {
            HitTarget();
            return;
        }

        transform.position += dir.normalized * distThisFrame;
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        var hp = target.GetComponent<EnemyHealth>();
        if (hp != null) hp.TakeDamage(damage);

        Destroy(gameObject);
    }
}