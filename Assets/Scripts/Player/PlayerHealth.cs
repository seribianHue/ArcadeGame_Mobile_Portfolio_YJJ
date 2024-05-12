using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("HP"), SerializeField]
    int hp = 100;
    int curHp = 100;

    public int CurHp => curHp;

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

    public void TakeDamage(int damage)
    {
        curHp -= damage;
    }
}
