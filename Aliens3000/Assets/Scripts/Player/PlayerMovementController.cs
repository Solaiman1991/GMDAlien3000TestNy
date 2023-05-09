using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityValue;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _rotationSpeed;

    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private Transform _cameraTransform;
    AudioManager audioManager ;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        MovePlayer();
        JumpPlayer();
    }
    
    
    
    public void MovePlayer()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector2 input = GetComponent<PlayerInput>().actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * _cameraTransform.right.normalized + move.z * _cameraTransform.forward.normalized;
        move.y = 0f;

        _controller.Move(move * Time.deltaTime * _playerSpeed);

        if (move != Vector3.zero)
        {
            Vector3 forwardDirection = _cameraTransform.forward;
            forwardDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

           

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            if (isRunning)
            {
                if (!audioManager.IsSoundPlaying("PlayerRun"))
                {
                    audioManager.StopSound("PlayerWalk"); 
                    audioManager.PlaySound("PlayerRun");
                    audioManager.PlaySound("PlayerBreathing");
                }
            }
            else
            {
                if (!audioManager.IsSoundPlaying("PlayerWalk"))
                {
                    audioManager.StopSound("PlayerRun"); 
                    audioManager.PlaySound("PlayerWalk");
                    audioManager.PlaySound("PlayerBreathing");

                }
            }
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    public void JumpPlayer()
    {
        if (GetComponent<PlayerInput>().actions["Jump"].triggered && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
    }

    private void LateUpdate()
    {
        _cameraTransform.rotation = Quaternion.Euler(0, _transform.rotation.eulerAngles.y, 0);
    }
}

