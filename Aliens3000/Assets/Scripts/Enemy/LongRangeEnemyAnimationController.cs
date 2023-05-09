using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LongRangeEnemyAnimationController : MonoBehaviour
{
    Animator animator;
    EnemyLongRange _enemy;
    NavMeshAgent navMeshAgent;
    bool hasDied = false;
    float playerVisibilityDistance = 20.0f;
    float walkingSpeedThreshold = 3.0f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(true);

        if (animator == null)
        {
            Debug.LogError("No Animator component found in child GameObjects.");
        }

        _enemy = GetComponent<EnemyLongRange>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        bool isDead = _enemy.health <= 0f;
        animator.SetBool("isDead", isDead);

        if (isDead && !hasDied)
        {
            navMeshAgent.isStopped = true;
            StartCoroutine(DieAfterDelay(30f));
            hasDied = true;
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(_enemy.PlayerTarget.position, transform.position);
            float currentSpeed = navMeshAgent.velocity.magnitude;

            animator.SetFloat("Speed", currentSpeed);
        }
    }

    private IEnumerator DieAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    public void SetAttacking(bool attacking)
    {
        animator.SetBool("isAttacking", attacking);
    }
}
