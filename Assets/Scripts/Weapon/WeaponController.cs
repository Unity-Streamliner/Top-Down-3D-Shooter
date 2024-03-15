using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform[] _weapons;
    private Transform _currentWeapon;

    [Header("Left Hand IK")]
    [SerializeField] private Transform _leftHand;

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
}


