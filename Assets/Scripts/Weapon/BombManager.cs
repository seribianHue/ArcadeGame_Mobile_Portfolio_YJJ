using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [Header("공격 시간간격"), SerializeField]
    float atkTime = 2f;
    float atkTimer = 0f;

    [SerializeField]
    GameObject bomb;

    [SerializeField]
    Transform player;

    [Header("공격 범위"), SerializeField]
    float range = 7f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject curbomb = Instantiate(bomb, transform.position, transform.rotation);
    }
}
