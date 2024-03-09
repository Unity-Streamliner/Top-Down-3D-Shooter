using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private Player _player;
    private Vector2 _aimInput;

    [Header("Aim info")]
    [SerializeField] private LayerMask _aimLayerMask;
    private Vector3 _lookingDirection;
    [SerializeField] private Transform _aim;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _player = GetComponent<Player>();
         AssignInputActions();
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

    private void AssignInputActions()
    {
        PlayerActions playerActions = _player.actions;

        playerActions.Character.Aim.performed += context =>
            _aimInput = context.ReadValue<Vector2>();
        playerActions.Character.Aim.canceled += context =>
            _aimInput = Vector2.zero;

        playerActions.Character.Fire.performed += context => Shoot();
    }
    
}
