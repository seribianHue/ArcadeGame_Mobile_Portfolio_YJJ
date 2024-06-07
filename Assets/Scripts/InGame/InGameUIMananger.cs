using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class InGameUIMananger : MonoBehaviour
{
    [SerializeField] GameObject _optionObj;
    public void BTN_OptionOpen()
    { _optionObj.SetActive(true); GameManager.Instance.GamePause(true); }
    public void BTN_OptionClose()
    { _optionObj.SetActive(false); GameManager.Instance.GamePause(false); }

    [SerializeField] Slider _soundSlider;
    [SerializeField] TextMeshProUGUI _soundValueText;
    public void Slider_setText(Single value)
    {
        _soundValueText.text = value.ToString();
        GameManager.Instance._gameData._musicVolume = Convert.ToInt32(value);
        GameManager.Instance.SetAudioVolume(Convert.ToInt32(value)/100f);
    }

    private void Start()
    {
        _soundSlider.value = GameManager.Instance._gameData._musicVolume;
        _soundValueText.text = GameManager.Instance._gameData._musicVolume.ToString();

    }
    public void BTN_GameReStart()
    {
        GameOverUI(false);
        SceneManager.LoadScene(0);
    }

    [SerializeField] GameObject GameEndObj;
    public void GameOverUI(bool onoff)
    {
        GameEndObj.SetActive(onoff);
    }

    [SerializeField] GameObject RankInfoObj;
    public void BTN_RankInfoUIOpen()
    {
        RankInfoObj.SetActive(true);
    }
    public void BTN_RankInfoUIClose()
    {
        RankInfoObj.SetActive(false);
    }
    [SerializeField] TextMeshProUGUI _rankTimetext;
    public void SetGameTimeInRank(float time)
    {
        _rankTimetext.text = time.ToString("F2");
    }


}
