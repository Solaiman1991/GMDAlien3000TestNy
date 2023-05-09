using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 10f; 
    [SerializeField] private float gravity = -9.81f;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("No Rigidbody component found on the bullet.");
            return;
        }

        _rigidbody.velocity = transform.up * speed;
    }

    private void FixedUpdate()
    {
        Vector3 gravityVector = new Vector3(0, gravity, 0) * Time.fixedDeltaTime;
        _rigidbody.velocity += gravityVector;
    }
}