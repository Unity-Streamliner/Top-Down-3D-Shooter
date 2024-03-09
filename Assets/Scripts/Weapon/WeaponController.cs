using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform[] _weapons;
    private Transform _currentWeapon;

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
                _weapons[i].gameObject.SetActive(true);
            }
            else
            {
                _weapons[i].gameObject.SetActive(false);
            }
        }
    }
}


