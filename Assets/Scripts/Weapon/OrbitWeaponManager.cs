using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeaponManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] _orbitWeapon1_6;

    int _orbitLevel = -1;

    [Header("회전 속도")]
    public float orbitSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OrbitLevelUp()
    {
        _orbitLevel++;
        if(_orbitLevel < 6)
        {
            SetAllWeaponOff();
            _orbitWeapon1_6[_orbitLevel].SetActive(true);
        }
        else if(_orbitLevel < 12)
        {
            orbitSpeed += 50;
            OrbitWeapon[] orbits = _orbitWeapon1_6[_orbitWeapon1_6.Length - 1].GetComponentsInChildren<OrbitWeapon>();
            foreach(OrbitWeapon orbit in orbits)
            {
                orbit.OrbitSpeedUpdate(orbitSpeed);
            }
        }
        else
        {
            return;
        }
    }

    void SetAllWeaponOff()
    {
        foreach(GameObject weapon in _orbitWeapon1_6) { weapon.SetActive(false); }
    }
}
