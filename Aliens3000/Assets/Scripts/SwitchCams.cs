
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchCams : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    
    private CinemachineVirtualCamera virtualCamera;
    private int priorityBoostAmount = 10;
    private InputAction aimAction;

    [SerializeField] private Canvas aimCanvas;
    [SerializeField] private Canvas thirdPersonCanvas;
    void Start()
    {
        aimCanvas.enabled = false;

    }

    
    void Update()
    {
        
    }
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = _playerInput.actions["Aim"];
    }


    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => StopAim();
    }

 
    private void OnDestroy()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => StopAim();
        
    }

    
    
    private void StartAim()
    {
        virtualCamera.Priority += priorityBoostAmount;
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;
    }

    private void StopAim()
    {
        virtualCamera.Priority -= priorityBoostAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
    }

   
}