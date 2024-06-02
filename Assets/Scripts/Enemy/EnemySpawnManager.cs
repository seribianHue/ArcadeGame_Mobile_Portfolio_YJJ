using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] GameObject[] _enemyPrefabs;

    Transform _playerTrf;

    public float radius = 30f;

    public int _spawnNum = 1;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerTrf = player.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _spawnNum; i++)
        {
            SpawnEnemy();
        }
    }

    float _timer;
    public float _spawnTime = 5;
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _spawnTime)
        {
            _timer = 0;
            for(int i = 0; i < _spawnNum; i++)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        float angle = Random.Range(0, 361) * Mathf.Deg2Rad;
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        Vector3 newPos = new Vector3(_playerTrf.position.x + x, 0.5f, _playerTrf.position.z + z);
        if (CommonMath.ProbabilityMethod(10))
        {
            Instantiate(_enemyPrefabs[GameManager.Instance._Stage + 1], newPos, Quaternion.identity);
        }
        else
        {
            Instantiate(_enemyPrefabs[GameManager.Instance._Stage], newPos, Quaternion.identity);

        }
    }

    public void StageUp(int stage)
    {
        if((stage & 1) == 0)
            _spawnNum++;
        _spawnTime -= Mathf.Sqrt(stage) / 5;
    }
}
