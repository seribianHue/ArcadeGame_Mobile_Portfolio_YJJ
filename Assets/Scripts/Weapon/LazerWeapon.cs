using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazerWeapon : MonoBehaviour
{
    [Header("공격력"), SerializeField]
    int atk = 100;

    [Header("리치"), SerializeField]
    float maxDistanceRay = 15f;
    
    [Header("쿨타임"), SerializeField]
    float coolTime = 3f;
    float curTimer;


    [Header("버튼"), SerializeField]
    Button lazerBtn;
    Image lazerBtnImage;

    LineRenderer _lineRenderer;
    
    RaycastHit[] hits;

    int _lazerLevel = 0;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;  
        lazerBtnImage = lazerBtn.GetComponentInParent<Image>();
        lazerBtn.interactable = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        curTimer = 3f;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(_lazerLevel > 0)
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
                lazerBtnImage.color = new Color(255f, 255f, 255f, 50f);//좀 흐릿하게 만들고 싶었지만 왜인지 실패
                lazerBtnImage.fillAmount = curTimer / coolTime;
            }
        }


    }

    public void LazerON()
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistanceRay);

        StartCoroutine(LineRendererLast());

        Debug.DrawRay(transform.position, transform.forward * maxDistanceRay, Color.red, 0.3f);//숫자는 기즈모 떠있는 시간
        hits = Physics.RaycastAll(transform.position, transform.forward, maxDistanceRay);

        int hitEnemys = 0;
        for (int i = 0; i < hits.Length; ++i)
        {
            //print(hits[i].collider.name);
            if (hits[i].transform.CompareTag("Enemy"))
            {
                hits[i].transform.GetComponent<EnemyHealth>().TakeDamage(atk/++hitEnemys);
            }
        }
        curTimer = 0f;
    }

    IEnumerator LineRendererLast()
    {
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        _lineRenderer.enabled = false;
        yield return null;
    }

    public void LazerLevelUp()
    {
        _lazerLevel++;
        if (_lazerLevel >= 2)
        {
            if (coolTime < 1f)
            {
                return;
            }
            else
            {
                coolTime -= 0.1f;
            }
        }
        else
        {
            lazerBtn.interactable = true;
        }
    }
}
