using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    [Header("ÃÑ¾Ë ÇÁ¸®Æé"), SerializeField]
    GameObject bullet;

    [Header("¹ß»ç ºóµµ"), SerializeField]
    float fireTime = 3f;
    float curTimer;
    // Start is called before the first frame update
    void Start()
    {
        curTimer = fireTime;
    }

    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;

        if (curTimer >= fireTime)
        {
            Vector3 rot = transform.rotation.eulerAngles;

            Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, 25f, 0)));
            Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, -25f, 0)));
            Instantiate(bullet, transform.position, transform.rotation);
            curTimer = 0f;
        }
    }
}
