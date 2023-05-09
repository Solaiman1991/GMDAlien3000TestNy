using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{


    [SerializeField] private float swaySpeed = 1f;
    [SerializeField] private float swayAmount = 1f;

    private Vector3 initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.rotation = Quaternion.Euler(initialRotation + new Vector3(0, 0, sway));
    }
}