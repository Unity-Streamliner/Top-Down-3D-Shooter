using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector3 _movementDirection;
    private float _gravityForce = 9.81f;

    private float _verticalVelocity;

    [Header("Movement info")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private float _speed;
    private bool _isRunning;

    private Animator _animator;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
        AssignInputActions();

        _speed = _walkSpeed;
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
        bool playRunAnimation = _isRunning && _moveInput.magnitude > 0;
        _animator.SetBool("isRunning", playRunAnimation);
    }

    private void ApplyMovement()
    {
        _movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        ApplyGravity();
        if (_movementDirection.magnitude > 0)
        {
            _characterController.Move(Time.deltaTime * _speed * _movementDirection);
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

    private void AssignInputActions()
    {
        PlayerActions playerActions = _player.actions;

        playerActions.Character.Movement.performed += context => 
            _moveInput = context.ReadValue<Vector2>();
        playerActions.Character.Movement.canceled += context =>
            _moveInput = Vector2.zero;

        playerActions.Character.Run.performed += context => {
            _speed = _runSpeed;
            _isRunning = true;
        };
        playerActions.Character.Run.canceled += context => {
            _speed = _walkSpeed;
            _isRunning = false;
        };
    }
}
