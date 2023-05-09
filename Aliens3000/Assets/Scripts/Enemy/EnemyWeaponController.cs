using System.Collections;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private float shootRate;
    public GameObject shotPrefab;
    private float shootRateTimeStamp;

    public void ShootAtTarget(Vector3 target)
    {
        if (Time.time > shootRateTimeStamp)
        {
            Vector3 direction = (target - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject bullet = Instantiate(shotPrefab, transform.position, rotation);
            bullet.GetComponent<EnemyBullet>().setTarget(target);
            Destroy(bullet, 2f);

            shootRateTimeStamp = Time.time + shootRate;
        }
    }
}