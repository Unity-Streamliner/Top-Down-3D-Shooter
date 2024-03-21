using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform[] _weapons;
    private Transform _currentWeapon;
    Animator _animator;

    [Header("Left Hand IK")]
    [SerializeField] private Transform _leftHand;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    } 

    private void Start()
    {
        _currentWeapon = _weapons[0];
        _currentWeapon.gameObject.SetActive(true);
    }

    public void SwitchWeapon(int weaponIndex)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (i == weaponIndex)
            {
                _currentWeapon = _weapons[i];
                _currentWeapon.gameObject.SetActive(true);
                AttachWeaponToHand();
                SwitchAnimationLayer(i == 3 ? 2 : i == 4 ? 3 : 1);
            }
            else
            {
                _weapons[i].gameObject.SetActive(false);
            }
        }
    }

    private void AttachWeaponToHand()
    {
        Transform targetTransform = _currentWeapon.GetComponentInChildren<LeftHandTargetTransform>().transform;
        _leftHand.SetLocalPositionAndRotation(targetTransform.localPosition, targetTransform.localRotation);
    }

    private void SwitchAnimationLayer(int layerIndex)
    {
        for (int i = 1; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }
        print("Layer index: " + layerIndex);
        _animator.SetLayerWeight(layerIndex, 1);
    }
}


