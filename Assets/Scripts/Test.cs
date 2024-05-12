using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    GameObject moveObject;

    [SerializeField]
    Transform start;
    [SerializeField]
    Transform end;

    [SerializeField]
    float time = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dkfj());
    }

    // Update is called once per frame
    void Update()
    {
        //moveObject.transform.position = Vector3.Slerp(moveObject.transform.position, end.position, time);
    }

    IEnumerator dkfj()
    {
        while (true)
        {
            moveObject.transform.position = Vector3.Slerp(moveObject.transform.position, end.position, time);
            yield return null;
        }
    }
}
