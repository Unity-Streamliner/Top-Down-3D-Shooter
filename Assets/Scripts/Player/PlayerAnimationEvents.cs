using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerShooting _playerShooting;
    
    void Start()
    {
        _playerShooting = GetComponentInParent<PlayerShooting>();
    }

    public void ReloadIsOver()
    {
        _playerShooting.ReturnRigWeightToOne();
    }
}
