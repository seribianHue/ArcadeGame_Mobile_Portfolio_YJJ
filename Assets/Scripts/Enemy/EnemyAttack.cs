using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("적 공격력"), SerializeField]
    int atk = 10;

    [Header("적 공격 시간간격")]
    public float atkTime = 0.1f;
    float atkTimer = 0f;

    GameObject player;

    PlayerHealth playerHealth;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponentInParent<PlayerHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            Attack();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == player)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkTime)
            {
                Attack();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        atkTimer = 0;
    }

/*    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            Attack();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkTime)
            {
                Attack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        atkTimer = 0;
    }*/

    void Attack()
    {
        if (playerHealth == null)
            return;

        atkTimer = 0f;

        if((playerHealth.CurHp > 0))
        {
            playerHealth.TakeDamage(atk);
        }
    }
}
