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
        sliderHp.value = curHp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        sliderHp.value = curHp;

        if(curHp <= 0)
        {
            curHp = 0;
            PlayerManager.Instance.Dead();
        }
    }

    public void RestoreHP(int hp)
    {
        curHp += hp;
        sliderHp.value = curHp;
    }
}
