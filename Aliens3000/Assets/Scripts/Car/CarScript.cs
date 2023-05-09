using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField] private float hoverHeight = 2.0f;
    public LayerMask groundLayer;
    [SerializeField] private float hoverForceMultiplier = 1.0f;
    [SerializeField] private float liftForce = 50.0f;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void FixedUpdate()
    {
        HoverStabilization();
    }

    private void HoverStabilization()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, hoverHeight, groundLayer))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * liftForce * hoverForceMultiplier;
            rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }
}