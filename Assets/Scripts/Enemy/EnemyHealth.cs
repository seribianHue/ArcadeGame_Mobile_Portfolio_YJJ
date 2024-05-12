using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("적 체력"), SerializeField]
    int initHp = 100;
    [SerializeField]
    int curHp;
    public int CurHp => curHp;

    [SerializeField]
    EnemyMove enemyMove;

    Renderer color;

    [Header("죽음 표현"), SerializeField]
    float deathTime = 5f;

    bool isDead;

    private void Awake()
    {
        color = gameObject.GetComponent<Renderer>();
        enemyMove = transform.GetComponentInParent<EnemyMove>();
        curHp = initHp;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        StartCoroutine(enemyMove.GetDamaged_Move());
    }

    public IEnumerator Dead()
    {
        color.material.color = Color.Lerp(color.material.color, Color.gray, 0.1f);
        yield return new WaitForSeconds(deathTime);
    }

    private void Update()
    {
        if ((curHp <= 0) && (!isDead))
        {
            enemyMove.enabled = false;
            isDead = true;
            StartCoroutine(Dead());
        }
    }
}
