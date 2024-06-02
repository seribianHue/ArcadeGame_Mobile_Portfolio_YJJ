using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    [Header("ÃÑ¾Ë ÇÁ¸®Æé"), SerializeField]
    GameObject bullet;

    [Header("¹ß»ç ºóµµ"), SerializeField]
    float fireTime = 2f;
    float curTimer;

    int _gunLevel = 0;

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

            if(_gunLevel == 0)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, 25f, 0)));
                Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, -25f, 0)));
                Instantiate(bullet, transform.position, transform.rotation);
                curTimer = 0f;

            }
            else
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, 25f, 0)));
                Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, -25f, 0)));
                Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, 15f, 0)));
                Instantiate(bullet, transform.position, Quaternion.Euler(rot + new Vector3(0, -15f, 0))); 
                Instantiate(bullet, transform.position, transform.rotation);
                curTimer = 0f;
            }

        }
    }

    public void GunLevelUp()
    {
        _gunLevel++;
        if(_gunLevel >= 2)
        {
            if(fireTime < 0.1f)
            {
                return;
            }
            else
            {
                fireTime -= 0.2f;
            }
        }
    }
}
