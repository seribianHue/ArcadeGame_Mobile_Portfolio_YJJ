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

    public bool isDead;

    [Header("Item"), SerializeField]
    GameObject[] _itemList;

    [Header("Death Mat"), SerializeField]
    Material[] _deathMatList;
    
    [Header("Damaged Mat"), SerializeField]
    Material _damageMat;

    Material _originalMat;

    Animator _animator;

    private void Awake()
    {
        //color = gameObject.GetComponent<Renderer>();
        enemyMove = transform.GetComponentInParent<EnemyMove>();
        curHp = initHp;
        isDead = false;
    }

    private void Start()
    {
        _originalMat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (!isDead)
        {
            if (curHp <= 0)
            {
                //dead
                enemyMove.enabled = false;
                isDead = true;
                GetComponent<Collider>().enabled = false;

                Destroy(GetComponent<EnemyAttack>());
                Destroy(GetComponent<EnemyMove>());
                _animator.SetBool("isDead", true);

                //임시
                //Destroy(gameObject);
                //임시
                StopAllCoroutines();
                StartCoroutine(Dead());
                DropItem();
            }
            else
            {
                //damaged
                StopAllCoroutines();
                enemyMove.Damaged();
                StartCoroutine(CRT_Damaged());
            }
        }
 
    }

    public IEnumerator Dead()
    {
        print(gameObject.name + "Dead");


/*        _animator.ResetTrigger("Attack");
        _animator.ResetTrigger("Hit");*/

/*        Destroy(GetComponent<EnemyAttack>());
        Destroy(GetComponent<EnemyMove>());*/

        Material mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        Texture texture = _originalMat.GetTexture("_MainTex");
        int ranIndex = Random.Range(0, 2);
        mat = Instantiate(_deathMatList[ranIndex]);
        mat.SetTexture("_MainTex", texture);
        GetComponentInChildren<SkinnedMeshRenderer>().material = mat;

        float time = 0f;
        while(time < 1f)
        {
            time += Time.deltaTime;
            if(ranIndex == 0)
            {
                mat.SetFloat("_Cut", Mathf.Lerp(0, 1, time / 1f));

            }
            else
            {
                mat.SetFloat("_DisappearPart", Mathf.Lerp(-2, 2, time / 1f));

            }
            yield return null;

        }

        Destroy(gameObject);
        yield return null;
/*        color.material.color = Color.Lerp(color.material.color, Color.gray, 0.1f);
        yield return new WaitForSeconds(deathTime);*/
    }

    IEnumerator CRT_Damaged()
    {

        Color enemyColor = _originalMat.color;
        Texture texture = _originalMat.GetTexture("_MainTex");

        _animator.SetTrigger("Hit");

        Material newmat = Instantiate(_damageMat);
        newmat.color = enemyColor;
        newmat.SetTexture("_MainTex", texture);
        GetComponentInChildren<SkinnedMeshRenderer>().material = newmat;

        float time = 0f;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            yield return null;

        }
        GetComponentInChildren<SkinnedMeshRenderer>().material = _originalMat;
        yield return null;
    }

    void DropItem()
    {
        if (CommonMath.ProbabilityMethod(70))
        {
            Instantiate(_itemList[Random.Range(0, _itemList.Length)], transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
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
