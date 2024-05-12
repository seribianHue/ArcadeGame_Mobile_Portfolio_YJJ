using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("[ 공격 시간 간격 ]"), SerializeField]
    float _attackTime = 1;

    [Header("[ 공격력 ]"), SerializeField]
    public int _attack = 10;


}
