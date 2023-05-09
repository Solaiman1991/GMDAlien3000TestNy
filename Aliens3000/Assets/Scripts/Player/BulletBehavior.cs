
using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour
{
    public Vector3 m_target;
    public GameObject collisionExplosion;
    public float speed;
    public string bulletTag = "Bullet";
    public string enemyBulletTag = "EnemyBullet";
    public float damagePerHit = 5.0f;

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (m_target != null)
        {
            if (transform.position == m_target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }
    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
        transform.LookAt(m_target);

        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, transform.up);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(bulletTag) || collision.collider.CompareTag(enemyBulletTag))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            EnemyLongRange component = collision.collider.GetComponent<EnemyLongRange>();
            if (component != null)
            {
                component.health -= damagePerHit;
                explode();
            }
            else
            {
                Debug.LogWarning("The enemy object does not have the LongRangeEnemy component attached.");
            }
        }
        else
        {
            explode();
        }
    }
    
    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }
    }
}


