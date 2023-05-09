using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShortRange : MonoBehaviour
{
    [SerializeField] private float aggroRange = 10f;

    private NavMeshAgent enemy;
    private ShortRangeEnemyAnimationController _shortRangeEnemyAnimation;
    public Transform PlayerTarget;
    [SerializeField] private float damagePerHit = 5.0f;
    public float minDistanceToPlayer = 3.0f;
    public float health = 10.0f;
    private float damageTimer;
    [SerializeField] private float damageRate = 1f;
    public AudioManager audioManager;
    private bool isDead = false;

    public MissionManager missionManager; // Add this line

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        enemy.autoBraking = false;
        enemy.autoRepath = true;
        enemy.stoppingDistance = minDistanceToPlayer;
        _shortRangeEnemyAnimation = GetComponentInChildren<ShortRangeEnemyAnimationController>();
    }

    void Update()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (PlayerTarget == null)
        {
            Debug.LogWarning("PlayerTarget is not assigned.");
            return;
        }

        if (playerStats == null || playerStats.isPlayerDead)
        {
            enemy.velocity = Vector3.zero;
            enemy.enabled = false;
            _shortRangeEnemyAnimation.SetAttacking(false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(PlayerTarget.position, transform.position);

        if (distanceToPlayer <= aggroRange)
        {
            if (distanceToPlayer > minDistanceToPlayer)
            {
                enemy.SetDestination(PlayerTarget.position);
                _shortRangeEnemyAnimation.SetAttacking(false);
            }
            else
            {
                enemy.velocity = Vector3.zero;
                _shortRangeEnemyAnimation.SetAttacking(true);
            }
        }
        else
        {
            enemy.velocity = Vector3.zero;
            _shortRangeEnemyAnimation.SetAttacking(false);
        }

        if (_shortRangeEnemyAnimation.IsAttacking())
        {
            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
            }
            else
            {
                AttackPlayer();
                damageTimer = 1f / damageRate;
            }
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            audioManager.PlaySound("AlienDead");

        }
    }

    void AttackPlayer()
    {
        if (damageTimer <= 0)
        {
            PlayerStats player = PlayerTarget.GetComponent<PlayerStats>();
            if (player != null)
            {
                player.TakeDamage(damagePerHit);
                damageTimer = 1f / damageRate;
                audioManager.PlaySound("AlienAttack1");
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider other)
            {
                if (other.gameObject.CompareTag("Bullet"))
                {
                    Debug.Log("Enemy hit by bullet");
                    BulletBehavior bulletBehavior = other.GetComponent<BulletBehavior>();
                    if (bulletBehavior != null)
                    {
                        health -= bulletBehavior.damagePerHit;
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
                GetComponent<NavMeshAgent>().enabled = false;

                health = 0f;

                missionManager.EnemyKilled(); // Make sure this line is present

                StartCoroutine(DestroyEnemyAfterDelay(30f));
            }

            private IEnumerator DestroyEnemyAfterDelay(float delay)
            {
                yield return new WaitForSeconds(delay);
                Destroy(gameObject);
            }
        }
    

