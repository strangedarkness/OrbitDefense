using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 6f;

    private int currentWaypoint = 0;
    private bool reachedEnd = false;

    void Update()
    {
        if (reachedEnd) return;
        if (waypoints == null || waypoints.Length == 0) return;
        if (currentWaypoint >= waypoints.Length)
        {
            ReachEnd();
            return;
        }

        Transform target = waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypoint++;

            // 刚好走完最后一个点，立刻触发终点
            if (currentWaypoint >= waypoints.Length)
            {
                ReachEnd();
            }
        }
    }

    private void ReachEnd()
    {
        if (reachedEnd) return;
        reachedEnd = true;

        // 到基地：扣血 + alive-1
        if (GameManager.Instance != null)
        {
            GameManager.Instance.BaseTakeDamage(1);
            GameManager.Instance.RegisterEnemyDeath();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null when enemy reached end!");
        }

        Destroy(gameObject);
    }
}