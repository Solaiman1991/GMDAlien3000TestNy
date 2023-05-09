using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class EnemyLongRange : MonoBehaviour
{
    


    
    [SerializeField] private float aggroRange = 10f;
    [SerializeField] private float shootRate;

    private NavMeshAgent enemy;
    private LongRangeEnemyAnimationController enemyAnimation;
    public Transform PlayerTarget;
    public GameObject shotPrefab;
    public Transform muzzleTransform;
    public float minDistanceToPlayer = 3f;
    public float health = 10.0f;
    private float shootRateTimeStamp;
    private RandomPatrol enemyPatrol; 
    private AudioManager audioManager;        
    public MissionManager missionManager;


    
    private bool isDead = false;
    private Animator animator;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        
        
        enemy = GetComponent<NavMeshAgent>();
        enemy.autoBraking = false;
        enemy.autoRepath = true;
        enemy.stoppingDistance = minDistanceToPlayer;
        enemyAnimation = GetComponentInChildren<LongRangeEnemyAnimationController>();
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("EnemyBullet"));
        
        enemyPatrol = GetComponent<RandomPatrol>(); 

        animator = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if (isDead) return;

        if (PlayerTarget == null)
        {
            Debug.LogWarning("PlayerTarget is not assigned.");
            return;
        }

        float distanceToPlayer = Vector3.Distance(PlayerTarget.position, transform.position);

        if (distanceToPlayer <= aggroRange)
        {
            if (enemyPatrol != null) enemyPatrol.enabled = false;

            enemy.SetDestination(PlayerTarget.position);
        }
        else
        {
            if (enemyPatrol != null) enemyPatrol.enabled = true;

            enemy.velocity = Vector3.zero;
        }

        if (distanceToPlayer <= aggroRange)
        {
            if (distanceToPlayer > minDistanceToPlayer && distanceToPlayer <= 5)
            {
                enemy.SetDestination(PlayerTarget.position);
                enemyAnimation.SetAttacking(false);
            }
            else if (distanceToPlayer <= minDistanceToPlayer)
            {
                enemyAnimation.SetAttacking(true);
            }
            else
            {
                enemy.SetDestination(PlayerTarget.position);
                enemyAnimation.SetAttacking(false);
            }
        }
        else
        {
            enemy.velocity = Vector3.zero;
        }

        Animator animator = enemyAnimation.GetComponentInChildren<Animator>();
        if (animator.GetBool("isAttacking"))
        {
            AimAtPlayer();
            ShootAtPlayer();
        }

        if (health <= 0)
        {
            Die();
        }
        
        if (enemy.hasPath)
        {
            animator.SetFloat("Speed", enemy.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void AimAtPlayer()
    {
        float heightOffset = 5f; 
        Vector3 targetDirection = (PlayerTarget.position + Vector3.up * heightOffset) - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
    }
    
    void ShootAtPlayer()
    {
        if (isDead)
        {
            return;
        }

        if (Time.time > shootRateTimeStamp && shotPrefab != null && muzzleTransform != null)
        {
            Vector3 direction = (PlayerTarget.position - muzzleTransform.position).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(direction) * muzzleTransform.localRotation;

            GameObject bullet = Instantiate(shotPrefab, muzzleTransform.position, bulletRotation);
            bullet.GetComponent<Rigidbody>().velocity = direction * 2000.0f * Time.deltaTime;
            bullet.GetComponent<Rigidbody>().useGravity = false;
            shootRateTimeStamp = Time.time + shootRate;

            EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();

            if (bulletScript != null)
            {
                bulletScript.setTarget(PlayerTarget.position);
            }

            if (!audioManager.IsSoundPlaying("EnemyLaserGun"))
            {
                audioManager.PlaySound("EnemyLaserGun");
            }
            
        }
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by bullet");
            EnemyBullet shotBehavior = other.GetComponent<EnemyBullet>();
            if (shotBehavior != null)
            {
                health -= shotBehavior.damagePerHit;
                Debug.Log("Enemy health: " + health);
            }
            else
            {
                Debug.LogWarning("Bullet does not have ShotBehavior component.");
            }
        }
    }

    public void Die()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().enabled = false;

        health = 0f;
        isDead = true;
        audioManager.PlaySound("AlienSoldierDead");
        missionManager.EnemyKilled();

        StartCoroutine(DestroyEnemyAfterDelay(30f));
    }
    
    private IEnumerator DestroyEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
