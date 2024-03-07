using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private PlayerActions _playerActions;
    private Vector2 _aimInput;

    [Header("Aim info")]
    [SerializeField] private LayerMask _aimLayerMask;
    private Vector3 _lookingDirection;
    [SerializeField] private Transform _aim;

    private Animator _animator;

    private void Awake()
    {
        _playerActions = new PlayerActions();

        _playerActions.Character.Aim.performed += context =>
            _aimInput = context.ReadValue<Vector2>();
        _playerActions.Character.Aim.canceled += context =>
            _aimInput = Vector2.zero;

        _playerActions.Character.Fire.performed += context => Shoot();
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Aiming();
    }

    private void Aiming()
    {
        Ray ray = Camera.main.ScreenPointToRay(_aimInput);
        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _aimLayerMask))
        {
            _lookingDirection = hitInfo.point - transform.position; 
            _lookingDirection.y = 0f;
            _lookingDirection.Normalize();

            transform.forward = _lookingDirection;

            _aim.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
    }

    private void Shoot()
    {
        Debug.Log("Shooting");
        _animator.SetTrigger("Fire");
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
