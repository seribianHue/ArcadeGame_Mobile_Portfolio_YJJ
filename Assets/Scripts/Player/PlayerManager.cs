using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHp;
    [SerializeField] GunWeapon _gunWeapon;
    [SerializeField] OrbitWeaponManager _orbitWeaponManager;

    public void RestoreHP()
    {
        _playerHp.RestoreHP(10);
    }

    public void GunLevelUp()
    {
        _gunWeapon.GunLevelUp();
    }

    public void OrbitLevelUp()
    {
        _orbitWeaponManager.OrbitLevelUp();
    }
}
