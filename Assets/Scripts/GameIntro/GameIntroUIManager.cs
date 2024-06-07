using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameIntroUIManager : MonoBehaviour
{

    [SerializeField] GameObject _optionObj;
    public void BTN_OptionOpen()
    { _optionObj.SetActive(true); }
    public void BTN_OptionClose()
    { _optionObj.SetActive(false); }

    public GameData _gameData;
    [SerializeField] Slider _soundSlider;
    [SerializeField] TextMeshProUGUI _soundValueText;
    [SerializeField] AudioSource _audio;
    public void Slider_setText(Single value)
    {
        _soundValueText.text = value.ToString();
        _gameData._musicVolume = Convert.ToInt32(value);
        _audio.volume = _gameData._musicVolume/100f;
    }

    [SerializeField] TextMeshProUGUI _rankcontextText;
    [SerializeField] RankingManager _rankmanager;
    private void Start()
    {
        _soundSlider.value = _gameData._musicVolume;
        _soundValueText.text = _gameData._musicVolume.ToString();
        _audio.volume = _gameData._musicVolume / 100f;

        _rankcontextText.text = _rankmanager.ConvertRankToString();
    }

    [SerializeField] GameObject _rankObj;
    public void BTN_RankOpen()
    { _rankObj.SetActive(true); }
    public void BTN_RankClose()
    { _rankObj.SetActive(false); }

    public void BTN_GameStart()
    {
        SceneManager.LoadScene(1);
    }
}
