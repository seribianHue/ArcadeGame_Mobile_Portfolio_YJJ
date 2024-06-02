using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("적 체력"), SerializeField]
    int initHp = 100;
    [SerializeField]
    int curHp;
    public int CurHp => curHp;

    [SerializeField]
    EnemyMove enemyMove;

    Renderer color;

    [Header("죽음 표현"), SerializeField]
    float deathTime = 5f;

    bool isDead;

    [Header("Item"), SerializeField]
    GameObject[] _itemList;

    [Header("Death Mat"), SerializeField]
    Material[] _deathMatList;
    
    [Header("Damaged Mat"), SerializeField]
    Material _damageMat;

    Material _originalMat;

    private void Awake()
    {
        color = gameObject.GetComponent<Renderer>();
        enemyMove = transform.GetComponentInParent<EnemyMove>();
        curHp = initHp;
        isDead = false;
    }

    private void Start()
    {
        _originalMat = GetComponent<MeshRenderer>().material;
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            //dead
            enemyMove.enabled = false;
            isDead = true;
            //임시
            //Destroy(gameObject);
            //임시
            
            StartCoroutine(Dead());
            DropItem();
        }
        else
        {
            //damaged
            StartCoroutine(enemyMove.GetDamaged_Move());
            StartCoroutine(CRT_Damaged());
        }
    }

    public IEnumerator Dead()
    {
        print(gameObject.name + "Dead");

        GetComponent<Collider>().enabled = false;

        Material mat = GetComponent<MeshRenderer>().material;
        Color enemyColor = mat.color;
        int ranIndex = Random.Range(0, 2);
        mat = Instantiate(_deathMatList[ranIndex]);
        mat.color = enemyColor;
        GetComponent<Renderer>().material = mat;

        float time = 0f;
        while(time < 0.5f)
        {
            time += Time.deltaTime;
            if(ranIndex == 0)
            {
                mat.SetFloat("_Cut", Mathf.Lerp(0, 1, time / 0.5f));

            }
            else
            {
                mat.SetFloat("_DisappearPart", Mathf.Lerp(0, 1, time / 0.5f));

            }
            yield return null;

        }

        Destroy(gameObject);

/*        color.material.color = Color.Lerp(color.material.color, Color.gray, 0.1f);
        yield return new WaitForSeconds(deathTime);*/
    }

    IEnumerator CRT_Damaged()
    {
        Color enemyColor = _originalMat.color;
        Texture texture = _originalMat.GetTexture("_MainTex");

        Material newmat = Instantiate(_damageMat);
        newmat.color = enemyColor;
        newmat.SetTexture("_MainTex", texture);
        GetComponent<Renderer>().material = newmat;

        float time = 0f;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            yield return null;

        }
        GetComponent<Renderer>().material = _originalMat;
        yield return null;
    }

    void DropItem()
    {
        if (CommonMath.ProbabilityMethod(70))
        {
            Instantiate(_itemList[Random.Range(0, _itemList.Length)], transform.position, transform.rotation);
        }
    }

    private void Update()
    {
/*        if ((curHp <= 0) && (!isDead))
        {
            enemyMove.enabled = false;
            isDead = true;
            StartCoroutine(Dead());
        }*/
    }
}
