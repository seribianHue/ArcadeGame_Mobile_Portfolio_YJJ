using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyAttack _enemyAttack;
    EnemyHealth _enemyHealth;
    EnemyMove _enemyMove;

    Animator _animator;

    private void Awake()
    {
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMove = GetComponent<EnemyMove>();

        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
