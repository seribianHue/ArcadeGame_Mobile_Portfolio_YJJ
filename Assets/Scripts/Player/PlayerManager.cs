using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHp;
    [SerializeField] GunWeapon _gunWeapon;
    [SerializeField] OrbitWeaponManager _orbitWeaponManager;
    [SerializeField] BombWeapon _bombWeapon;
    [SerializeField] LazerWeapon _lazerWeapon;

    [Header("Upgrade Mat"), SerializeField]
    Material _upgradeMat;

    Material _originalMat;

    private void Start()
    {
        _originalMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    public void RestoreHP()
    {
        _playerHp.RestoreHP(10);
    }

    public void GunLevelUp()
    {
        _gunWeapon.GunLevelUp();
        StartCoroutine(CRT_Upgrade());
    }

    public void OrbitLevelUp()
    {
        _orbitWeaponManager.OrbitLevelUp();
        StartCoroutine(CRT_Upgrade());
    }

    public void BombLevelUp()
    {
        _bombWeapon.BombLevelUp();
        StartCoroutine(CRT_Upgrade());
    }

    public void LazerLevelUp()
    {
        _lazerWeapon.LazerLevelUp();
        StartCoroutine(CRT_Upgrade());
    }

    IEnumerator CRT_Upgrade()
    {
        SkinnedMeshRenderer[] mesh = GetComponentsInChildren<SkinnedMeshRenderer>();
        
        Color enemyColor = _originalMat.color;
        Texture texture = _originalMat.GetTexture("_MainTex");

        Material newmat = Instantiate(_upgradeMat);
        newmat.color = enemyColor;
        newmat.SetTexture("_MainTex", texture);

        foreach (SkinnedMeshRenderer meshRenderer in mesh) { meshRenderer.material = newmat; }

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            yield return null;

        }
        
        foreach (SkinnedMeshRenderer meshRenderer in mesh) { meshRenderer.material = _originalMat; }

        yield return null;
    }
}
