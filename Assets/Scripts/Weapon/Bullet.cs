using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("공격력"), SerializeField]
    int atk = 5;
    [Header("총알 속력"), SerializeField]
    int speed = 5;

    [Header("총알 생존시간"), SerializeField]
    float surviveTime = 5f;
    float curTimer;



    // Start is called before the first frame update
    void Start()
    {
        //gameObject.transform.
    }

    // Update is called once per frame
    void Update()
    {
        curTimer += Time.deltaTime;

        if (curTimer <= surviveTime)
        {
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(atk);
            Destroy(gameObject);
        }
    }

}
