using UnityEngine;

public class InOutFromCar : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private KeyCode enterExitKey = KeyCode.E;
    [SerializeField] private GameObject car = null;
    [SerializeField] private float distanceThreshold = 3f;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera carFollowingCamera;

    private bool playerInsideCar = false;
    private CarController carController;
    private AudioManager audioManager;

    void Start()
    {
        carController = car.GetComponent<CarController>();
        carController.enabled = false;
        carFollowingCamera.enabled = false;
        audioManager = FindObjectOfType<AudioManager>();

    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(enterExitKey) && IsPlayerCloseEnough())
        {
            if (playerInsideCar)
            {
                GetOutOfCar();
            }
            else
            {
                GetInsideCar();
            }
        }
    }

 
    
    private void GetInsideCar()
    {
        player.SetActive(false);
        playerInsideCar = true;
        carController.enabled = true;
        carFollowingCamera.enabled = true;
        audioManager.PlaySound("AirCraftEngine", true);
    }

    void GetOutOfCar()
    {
        player.SetActive(true);
        player.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left);
        playerInsideCar = false;
        carController.enabled = false;
        carFollowingCamera.enabled = false;
        audioManager.StopSound("AirCraftEngine");

    }

    bool IsPlayerCloseEnough()
    {
        float distance = Vector3.Distance(player.transform.position, car.transform.position);
        return distance <= distanceThreshold;
    }
}

