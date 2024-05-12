using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    //BombAttack bombAttack;

    [Header("���ݷ�"), SerializeField]
    int atk = 50;
    [Header("���� ����"), SerializeField]
    float atkRange = 7f;

    [Header("���� Ÿ�� ����"), SerializeField]
    float targetRange = 7f;
    [Header("���ư��� �ð�"), SerializeField]
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
            //�÷��̾�� ���� ����� ���� ������ ���� �ӽ÷� 0��°�� �Ѵ�
            targetPos = new Vector3(tmptarget.x, 0, tmptarget.z);
        }
        else
        {
            //�÷��̾ �ٶ󺸰��ִ� �������� �ִ� ���̷� == ���� 7
            Vector3 tmptarget = transform.position + (Vector3.forward * targetRange);
            targetPos = new Vector3(tmptarget.x, 0, tmptarget.z);
        }
    }

    private void Update()
    {
        if (!isExplode)
        {
            //time�� �Ÿ��� ���� �ٸ���
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
