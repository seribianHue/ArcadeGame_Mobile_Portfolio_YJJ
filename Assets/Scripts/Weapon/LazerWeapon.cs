using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerWeapon : MonoBehaviour
{
    [Header("���ݷ�"), SerializeField]
    int atk = 100;

    [Header("��ġ"), SerializeField]
    float maxDistanceRay = 15f;
    
    [Header("��Ÿ��"), SerializeField]
    float coolTime = 3f;
    float curTimer;


    [Header("��ư"), SerializeField]
    Button lazerBtn;
    Image lazerBtnImage;

    RaycastHit[] hits;

    private void Awake()
    {
        lazerBtnImage = lazerBtn.GetComponentInParent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        curTimer = 3f;
    }
    
    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;

        if (curTimer >= coolTime)
        {
            lazerBtn.interactable = true;
            lazerBtnImage.color = new Color(255f, 255f, 255f, 255f);
            lazerBtnImage.fillAmount = 1;

        }
        else
        {
            lazerBtn.interactable = false;
            lazerBtnImage.color = new Color(255f, 255f, 255f, 50f);//�� �帴�ϰ� ����� �;����� ������ ����
            lazerBtnImage.fillAmount = curTimer / coolTime;
        }
    }

    public void LazerON()
    {
        Debug.DrawRay(transform.position, transform.forward * maxDistanceRay, Color.red, 0.3f);//���ڴ� ����� ���ִ� �ð�
        hits = Physics.RaycastAll(transform.position, transform.forward, maxDistanceRay);
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].transform.CompareTag("Enemy"))
            {
                hits[i].transform.GetComponent<EnemyHealth>().TakeDamage(atk);
            }
        }
        curTimer = 0f;
    }
}
