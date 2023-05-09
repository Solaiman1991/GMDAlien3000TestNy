using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float liftForce = 100.0f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float forwardThrust = 20f;
    [SerializeField] private float pitchSpeed = 50f;
    [SerializeField] private float rollSpeed = 50f;

    private Transform _cameraTransform;

    private Rigidbody rb;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveCar();
        RotateCar();
        LiftCar();
    }

    private void LiftCar()
    {
        bool lift = Input.GetKey(KeyCode.Space);
        bool descend = Input.GetKey(KeyCode.X);

        if (lift)
        {
            rb.AddForce(Vector3.up * liftForce);
        }
        else if (descend)
        {
            rb.AddForce(Vector3.down * liftForce);
        }
    }

    private void MoveCar()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movement = transform.forward * verticalInput * moveSpeed * Time.fixedDeltaTime;
        float rotation = horizontalInput * turnSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));
    }

    private void RotateCar()
    {
        float pitchInput = Input.GetAxis("Mouse Y");
        float rollInput = Input.GetAxis("Mouse X");

        float pitch = pitchInput * pitchSpeed * Time.fixedDeltaTime;
        float roll = rollInput * rollSpeed * Time.fixedDeltaTime;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(-pitch, 0, -roll));

        Quaternion targetRotation = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}