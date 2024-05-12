using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    //BombAttack bombAttack;

    [Header("공격력"), SerializeField]
    int atk = 50;
    [Header("공격 범위"), SerializeField]
    float atkRange = 7f;

    [Header("공격 타겟 범위"), SerializeField]
    float targetRange = 7f;
    [Header("날아가는 시간"), SerializeField]
    float floatTime = 0.01f;

    bool isExplode;

    Vector3 targetPos;

    private void Start()
    {
        isExplode = false;

        //Find Target
        Collider[] colls = Physics.OverlapSphere(transform.position, targetRange);
        if (colls.Length != 0)
        {
            Vector3 tmptarget = colls[0].transform.position;
            //플레이어와 길이 계산후 가장 가까운거 고른다 임시로 0번째를 한다
            targetPos = new Vector3(tmptarget.x, 0, tmptarget.z);
        }
        else
        {
            //플레이어가 바라보고있는 방향으로 최대 길이로 == 길이 7
            Vector3 tmptarget = transform.position + (Vector3.forward * targetRange);
            targetPos = new Vector3(tmptarget.x, 0, tmptarget.z);
        }
    }

    private void Update()
    {
        if (!isExplode)
        {
            //time은 거리에 따라 다르게
            transform.position = Vector3.Slerp(transform.position, targetPos, floatTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Collider[] colls = Physics.OverlapSphere(transform.position, atkRange);
            for(int i = 0; i < colls.Length; ++i)
            {
                if (colls[i].gameObject.CompareTag("Enemy"))
                {
                    colls[i].gameObject.GetComponent<EnemyHealth>().TakeDamage(atk);
                }
                else
                    continue;
            }
            isExplode = true;
        }

    }


}
