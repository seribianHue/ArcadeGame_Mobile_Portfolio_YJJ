using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [Header("ȸ�� �ӵ�")]
    float orbitSpeed = 10;

    [Header("���ݷ�"), SerializeField]
    int atk = 6;

    private void Awake()
    {
        orbitSpeed = gameObject.GetComponentInParent<OrbitWeaponManager>().orbitSpeed;
    }

    void Orbit()
    {
        transform.RotateAround(player.transform.position, Vector3.down, orbitSpeed * Time.deltaTime);
    }

    void Update()
    {
        Orbit();
    }

    public void OrbitSpeedUpdate(float speed)
    {
        orbitSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(atk);
        }
    }
}
