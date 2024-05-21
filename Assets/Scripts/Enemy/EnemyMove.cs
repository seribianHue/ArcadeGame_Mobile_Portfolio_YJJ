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

    [Header("�̵� �ӵ�"), SerializeField]
    float _speed = 2f;

    [Header("�ǰݽ� �̵� �ӵ�"), SerializeField]
    float _dmgSpeed = 1f;
    [Header("�ǰݽ� �ð�"), SerializeField]
    float _dmgTime = 0.5f;


    private void Awake()
    {
        _myTrf = transform;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerTrf = player.transform;

        rig = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        _myTrf.LookAt(_playerTrf);
        _myTrf.position += transform.forward * _speed * Time.deltaTime;
    }

    public IEnumerator GetDamaged_Move()
    {
        float initSpeed = _speed;
        _speed = _dmgSpeed;
        yield return new WaitForSeconds(_dmgTime);
        _speed = initSpeed;
    }

}
