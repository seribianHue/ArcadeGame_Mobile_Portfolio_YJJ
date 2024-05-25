using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    [Header("공격 시간간격"), SerializeField]
    float fireTime = 10f;
    float curTimer = 0f;

    [SerializeField]
    GameObject bomb;

    int _bombLevel = 0;

    private void Start()
    {
    }

    private void Update()
    {
        if(_bombLevel > 0)
        {
            curTimer += Time.deltaTime;

            if (curTimer >= fireTime)
            {
                GameObject curbomb = Instantiate(bomb, transform.position, transform.rotation);
                curTimer = 0f;
            }
        }
    }

    public void BombLevelUp()
    {
        _bombLevel++;
        if (_bombLevel >= 2)
        {
            if (fireTime < 3f)
            {
                return;
            }
            else
            {
                fireTime -= 0.1f;
            }
        }
        else
        {
            GameObject curbomb = Instantiate(bomb, transform.position, transform.rotation);

        }
    }
}
