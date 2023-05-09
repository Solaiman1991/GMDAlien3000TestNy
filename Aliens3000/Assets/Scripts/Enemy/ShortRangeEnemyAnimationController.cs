
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShortRangeEnemyAnimationController : MonoBehaviour
{
    Animator animator;
    EnemyShortRange _enemyShortRange;
    NavMeshAgent navMeshAgent;
    bool hasDied = false;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _enemyShortRange = GetComponent<EnemyShortRange>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        bool isDead = _enemyShortRange.health <= 0f;
    
        animator.SetBool("isDead", isDead);
    
        if (isDead && !hasDied)
        {
            navMeshAgent.isStopped = true;
            StartCoroutine(DieAfterDelay(30f));
            hasDied = true;
        }
    }
    
    private IEnumerator DieAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _enemyShortRange.Die();
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetAttacking(true);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetAttacking(false);
        }
    }
    
    public void SetAttacking(bool attacking)
    {
        animator.SetBool("isAttacking", attacking);
    }
    
    public bool IsAttacking()
    {
        return animator.GetBool("isAttacking");
    }

}