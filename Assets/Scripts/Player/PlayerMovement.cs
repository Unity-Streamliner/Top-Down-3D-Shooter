using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerActions _playerActions;
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector3 _movementDirection;
    private float _gravityForce = 9.81f;

    private float _verticalVelocity;

    [Header("Movement info")]
    [SerializeField]
    private float walkSpeed;

    private Animator _animator;

    private void Awake()
    {
        _playerActions = new PlayerActions();

        _playerActions.Character.Movement.performed += context => 
            _moveInput = context.ReadValue<Vector2>();
        _playerActions.Character.Movement.canceled += context =>
            _moveInput = Vector2.zero;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ApplyMovement();
        Animations();
    }

    private void Animations()
    {
        float xVelocity = Vector3.Dot(_movementDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(_movementDirection.normalized, transform.forward);

        _animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        _animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);
    }

    private void ApplyMovement()
    {
        _movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        ApplyGravity();
        if (_movementDirection.magnitude > 0)
        {
            _characterController.Move(Time.deltaTime * walkSpeed * _movementDirection);
        }
    }

    private void ApplyGravity()
    {
        if(!_characterController.isGrounded)
        {
            _verticalVelocity -= _gravityForce * Time.deltaTime;
            _movementDirection.y = _verticalVelocity;
        }
        else 
        {
            _verticalVelocity = -.5f;
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
