using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions _playerActions;
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector2 _aimInput;
    private Vector3 movementDirection;

    [Header("Movement info")]
    [SerializeField]
    private float walkSpeed;

    private void Awake()
    {
        _playerActions = new PlayerActions();

        _playerActions.Character.Movement.performed += context => 
            _moveInput = context.ReadValue<Vector2>();
        _playerActions.Character.Movement.canceled += context =>
            _moveInput = Vector2.zero;

        _playerActions.Character.Aim.performed += context =>
            _aimInput = context.ReadValue<Vector2>();
        _playerActions.Character.Aim.canceled += context =>
            _aimInput = Vector2.zero;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        if (movementDirection.magnitude > 0)
        {
            _characterController.Move(movementDirection * walkSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }
}
