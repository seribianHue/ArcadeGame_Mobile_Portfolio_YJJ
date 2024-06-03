using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Transform _playerTrf;
    Transform _myTrf;

    NavMeshAgent navMesh;

    Rigidbody rig;

    [Header("이동 속도"), SerializeField]
    float _speed = 2f;

    [Header("피격시 이동 속도"), SerializeField]
    float _dmgSpeed = 0.5f;
    float _originalSpeed;
    [Header("피격시 시간"), SerializeField]
    float _dmgTime = 0.5f;

    EnemyHealth _enemyHealth;


    private void Awake()
    {
        _myTrf = transform;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerTrf = player.transform;
        
        _enemyHealth = GetComponent<EnemyHealth>();
        //rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _originalSpeed = _speed;
    }

    private void Update()
    {
        if(!_enemyHealth.isDead)
            Move();     
    }

    void Move()
    {
        _myTrf.LookAt(_playerTrf);
        _myTrf.position += transform.forward * _speed * Time.deltaTime;
    }

    public IEnumerator GetDamaged_Move()
    {
        _myTrf.position -= transform.forward * 0.5f;
        _speed = _dmgSpeed;
        yield return new WaitForSeconds(_dmgTime);
        _speed = _originalSpeed;
        yield return null;
    }

}
