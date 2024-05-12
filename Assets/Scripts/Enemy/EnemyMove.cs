using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Transform playerTrf;
    Transform myTrf;

    NavMeshAgent navMesh;

    Rigidbody rig;

    [Header("이동 속도"), SerializeField]
    float speed = 2f;

    [Header("피격시 이동 속도"), SerializeField]
    float dmgSpeed = 1f;
    [Header("피격시 시간"), SerializeField]
    float dmgTime = 0.5f;


    private void Awake()
    {
        myTrf = transform;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTrf = player.transform;

        navMesh = GetComponent<NavMeshAgent>();

        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMesh.enabled = true;
        navMesh.speed = speed;
    }

    private void Update()
    {
        if (navMesh.enabled)
            navMesh.SetDestination(playerTrf.position);
    }

    public IEnumerator GetDamaged_Move()
    {
        navMesh.speed = dmgSpeed;
        yield return new WaitForSeconds(dmgTime);
        navMesh.speed = speed;
    }

}
