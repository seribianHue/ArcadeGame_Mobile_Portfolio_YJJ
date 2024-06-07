using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    float _gameTime;
    public float _GameTime {  get { return _gameTime; } }

    float _stageTime;

    int _stage;
    public int _Stage { get { return _stage; } }

    bool _isGameOn;
    public bool _isGamePause;

    [SerializeField] EnemySpawnManager _enemySpawnManager;
    [SerializeField] InGameUIMananger _inGameUIMananger;
    [SerializeField] PlayerManager _playerManager;

    [SerializeField]
    TextMeshProUGUI _timerText;

    AudioSource _audio;

    public GameData _gameData;

    private void Awake()
    {

        instance = this;
        _stage = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _isGameOn = true;
        _isGamePause = false;
        _audio = GetComponent<AudioSource>();
        _audio.volume = _gameData._musicVolume/100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOn && !_isGamePause)
        {
            _gameTime += Time.deltaTime;
            _timerText.text = _gameTime.ToString("F2");

            _stageTime += Time.deltaTime;
            if(_stageTime > 60)
            {
                _stageTime = 0.0f;
                _stage++;
                _enemySpawnManager.StageUp(_stage);
            }
        }

    }

    public void SetAudioVolume(float volume)
    {
        _audio.volume = volume;
    }

    IEnumerator CRT_VolumeFadeOut()
    {
        float time = 0f;
        float initVolume = _audio.volume;
        while (time < 5f)
        {
            time += Time.deltaTime;
            _audio.volume = initVolume - ((initVolume/5f) * time);
            yield return null;

        }
        yield return null;
    }

    public void GamePause(bool pause)
    {
        _isGamePause = pause;
        _playerManager.GamePause(!pause);
    }

    public void GameOver()
    {
        _isGameOn = false;
        StartCoroutine(CRT_VolumeFadeOut());
        _enemySpawnManager.enabled = false;
        _inGameUIMananger.GameOverUI(true);
        _inGameUIMananger.SetGameTimeInRank(_gameTime);
    }
}
