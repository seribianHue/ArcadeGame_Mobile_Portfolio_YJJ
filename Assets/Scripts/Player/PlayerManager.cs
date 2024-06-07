using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }

    [SerializeField] PlayerHealth _playerHp;
    [SerializeField] GunWeapon _gunWeapon;
    [SerializeField] OrbitWeaponManager _orbitWeaponManager;
    [SerializeField] BombWeapon _bombWeapon;
    [SerializeField] LazerWeapon _lazerWeapon;

    [Header("Upgrade Mat"), SerializeField]
    Material _upgradeMat;
    Material _originalMat;

    [SerializeField] TextMeshProUGUI _explainText;

    public bool isDead;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isDead = false;
        _originalMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    public void RestoreHP()
    {
        _playerHp.RestoreHP(10);
    }
    public void GunLevelUp()
    {
        _gunWeapon.GunLevelUp();
        StopAllCoroutines();
        StartCoroutine(CRT_Upgrade("Gun"));
    }
    public void OrbitLevelUp()
    {
        _orbitWeaponManager.OrbitLevelUp();
        StopAllCoroutines();
        StartCoroutine(CRT_Upgrade("Orbit"));
    }
    public void BombLevelUp()
    {
        _bombWeapon.BombLevelUp();
        StopAllCoroutines();
        StartCoroutine(CRT_Upgrade("Bomb"));
    }
    public void LazerLevelUp()
    {
        _lazerWeapon.LazerLevelUp();
        StopAllCoroutines();
        StartCoroutine(CRT_Upgrade("Lazer"));
    }

    IEnumerator CRT_Upgrade(string weapon)
    {
        _explainText.text = weapon + " Weapon!";

        SkinnedMeshRenderer[] mesh = GetComponentsInChildren<SkinnedMeshRenderer>();
        
        Color color = _originalMat.color;
        Texture texture = _originalMat.GetTexture("_MainTex");

        Material newmat = Instantiate(_upgradeMat);
        newmat.color = color;
        newmat.SetTexture("_MainTex", texture);

        foreach (SkinnedMeshRenderer meshRenderer in mesh) { meshRenderer.material = newmat; }

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            yield return null;

        }
        _explainText.text = "";
        foreach (SkinnedMeshRenderer meshRenderer in mesh) { meshRenderer.material = _originalMat; }

        yield return null;
    }

    public void GamePause(bool onoff)
    {
        _gunWeapon.enabled = onoff;
        _bombWeapon.enabled = onoff;
        _lazerWeapon.enabled = onoff;
        _orbitWeaponManager.SetOrbitWeapon(onoff);
    }

    [SerializeField] Material _deathMat;

    public void Dead()
    {
        isDead = true;

        _gunWeapon.enabled = false;
        _bombWeapon.enabled = false;
        _lazerWeapon.enabled = false;
        _orbitWeaponManager.SetAllWeaponOff();

        GameManager.Instance.GameOver();

        StopAllCoroutines();
        StartCoroutine(CRT_Dead());
    }
    public IEnumerator CRT_Dead()
    {
        SkinnedMeshRenderer[] mesh = GetComponentsInChildren<SkinnedMeshRenderer>();

        Color color = _originalMat.color;
        Texture texture = _originalMat.GetTexture("_MainTex");

        Material newmat = Instantiate(_deathMat);
        newmat.color = color;
        newmat.SetTexture("_MainTex", texture);

        foreach (SkinnedMeshRenderer meshRenderer in mesh) { meshRenderer.material = newmat; }

        float time = 0f;
        while (time < 5f)
        {
            time += Time.deltaTime;
            newmat.SetFloat("_DisappearPart", Mathf.Lerp(-2, 2, time / 5f));
            //GameManager.Instance.SetAudioVolume(( - time) / 5f);
            yield return null;

        }
        yield return null;
    }


}
