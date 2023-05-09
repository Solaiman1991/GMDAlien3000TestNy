using UnityEngine;
using UnityEngine.AI;

public class RandomPatrol : MonoBehaviour
{
    [SerializeField] private float patrolRange = 20f;
    [SerializeField] private float patrolPointChangeTime = 5f;
    private float patrolTimer;
    private Vector3 currentPatrolPoint;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrolTimer = patrolPointChangeTime;
    }

    void Update()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0f)
        {
            SetRandomPatrolPoint();
            patrolTimer = patrolPointChangeTime;
        }

        if (navMeshAgent.enabled && navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(currentPatrolPoint);
        }
    }


    private void SetRandomPatrolPoint()
    {
        Vector3 randomPoint = transform.position + new Vector3(Random.Range(-patrolRange, patrolRange), 0, Random.Range(-patrolRange, patrolRange));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, patrolRange, NavMesh.AllAreas))
        {
            currentPatrolPoint = hit.position;
            Debug.Log("Patrol Point Set: " + currentPatrolPoint); 
            Debug.DrawLine(transform.position, currentPatrolPoint, Color.green, patrolPointChangeTime); 
        }
        else
        {
            Debug.LogWarning("Failed to set a valid patrol point."); 
        }
    }

}