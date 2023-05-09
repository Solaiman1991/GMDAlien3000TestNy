using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 target;
    public GameObject collisionExplosion;
    public float speed;
    public string bulletTag = "EnemyBullet";
    public float damagePerHit = 20.0f;

    void Update()
    {
        float step = speed * Time.deltaTime;

        if (target != null)
        {
            if (transform.position == target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    public void setTarget(Vector3 newTarget)
    {
        target = newTarget;
        transform.LookAt(target);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(bulletTag))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerStats>().TakeDamage(damagePerHit);
            explode();
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