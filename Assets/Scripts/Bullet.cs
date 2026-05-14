using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 18f;
    private float lifeTime = 3f;

    public float damage = 5f;

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
        EnemyHealth hp = target.GetComponent<EnemyHealth>();

        if (hp != null)
        {
            hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}