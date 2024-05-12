using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("HP"), SerializeField]
    int hp = 100;
    int curHp = 100;

    [SerializeField]
    Slider sliderHp;

    private void Awake()
    {
        sliderHp.maxValue = hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderHp.value = curHp;

    }
}
