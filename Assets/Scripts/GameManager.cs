using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    float _gameTime;
    public float _GameTime {  get { return _gameTime; } }

    float _stageTime;

    int _stage;
    public int _Stage { get { return _stage; } }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _gameTime += Time.deltaTime;
        _stageTime += Time.deltaTime;
        if(_stageTime > 120)
        {
            _stageTime = 0.0f;
            _stage++;
        }
    }
}
