using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [Header("회전 속도"), SerializeField]
    float orbitSpeed = 10;

    [Header("공격력"), SerializeField]
    int atk = 6;

    void Orbit()
    {
        transform.RotateAround(player.transform.position, Vector3.down, orbitSpeed * Time.deltaTime);
    }

    void Update()
    {
        Orbit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyGetDamage"))
        {
            other.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(atk);
        }
    }
}
