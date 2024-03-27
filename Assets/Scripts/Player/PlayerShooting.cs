using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerShooting : MonoBehaviour
{
    private Player _player;
    private Vector2 _aimInput;
    private WeaponActions _weaponActions;

    [Header("Aim info")]
    [SerializeField] private LayerMask _aimLayerMask;
    private Vector3 _lookingDirection;
    [SerializeField] private Transform _aim;

    WeaponController _weaponController;

    private Animator _animator;

    [Header("Rig")]
    [SerializeField] private float _rigIncreaseStep;
    private bool _rigShouldBeIncreased;
    private Rig _rig;

    private void Awake()
    {
        _weaponActions = new WeaponActions();
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rig = GetComponentInChildren<Rig>();
        _player = GetComponent<Player>();
        _weaponController = GetComponentInChildren<WeaponController>();
        AssignInputActions();
    }

    private void Update()
    {
        Aiming();
        CheckReturnRigWeightToOne();
    }

    private void CheckReturnRigWeightToOne()
    {
        if(_rigShouldBeIncreased)
        {
            _rig.weight += _rigIncreaseStep * Time.deltaTime;
            if(_rig.weight >= 1f)
            {
                _rigShouldBeIncreased = false;
            }
        }
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

            _aim.position = new Vector3(hitInfo.point.x, transform.position.y + 1, hitInfo.point.z);
        }
    }

    public void ReturnRigWeightToOne() => _rigShouldBeIncreased = true;

    private void Shoot()
    {
        Debug.Log("Shooting");
        _animator.SetTrigger("Fire");
    }

    private void Reload()
    {
        Debug.Log("Reloading");
        _animator.SetTrigger("Reload");
        _rig.weight = 0f;
    }

    private void ChangeWeapon(int weaponIndex)
    {
        _weaponController.SwitchWeapon(weaponIndex);
    }

    private void AssignInputActions()
    {
        PlayerActions playerActions = _player.actions;

        playerActions.Character.Aim.performed += context =>
            _aimInput = context.ReadValue<Vector2>();
        playerActions.Character.Aim.canceled += context =>
            _aimInput = Vector2.zero;

        playerActions.Character.Fire.performed += context => Shoot();
        playerActions.Character.Reload.performed += context => Reload();

        _weaponActions.Weapon.Pistol.performed += context => ChangeWeapon(0);
        _weaponActions.Weapon.Revolver.performed += context => ChangeWeapon(1);
        _weaponActions.Weapon.Rifle.performed += context => ChangeWeapon(2);
        _weaponActions.Weapon.Shotgun.performed += context => ChangeWeapon(3);
        _weaponActions.Weapon.Sniper.performed += context => ChangeWeapon(4);
    }

    private void OnEnable()
    {
       _weaponActions.Enable();
    }

    private void OnDisable()
    {
        _weaponActions.Disable();
    }
    
}
