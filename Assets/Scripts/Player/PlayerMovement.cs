using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions _playerActions;
    public Vector2 moveInput;
    public Vector2 aimInput;

    private void Awake()
    {
        _playerActions = new PlayerActions();

        _playerActions.Character.Movement.performed += context => 
            moveInput = context.ReadValue<Vector2>();
        _playerActions.Character.Movement.canceled += context =>
            moveInput = Vector2.zero;

        _playerActions.Character.Aim.performed += context =>
            aimInput = context.ReadValue<Vector2>();
        _playerActions.Character.Aim.canceled += context =>
            aimInput = Vector2.zero;
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
