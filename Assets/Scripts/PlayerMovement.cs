using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동 속도"), SerializeField]
    float moveSpeed = 5f;
    [Header("회전 속도"), SerializeField]
    float rotSpeed = 5f;

    [SerializeField]
    InputControllerBase controller;
    [SerializeField]
    JoyStickController joystick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyBoardMove();
    }

    void KeyBoardMove()
    {
        Vector3 newpos = transform.position + new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed, 0, Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        
        Vector3 newrot = transform.rotation.eulerAngles + new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed, 0);


        gameObject.transform.SetPositionAndRotation(newpos, Quaternion.LookRotation(newrot));


        //gameObject.transform.Translate(pos);

        //gameObject.transform.SetPositionAndRotation(Vector3 pos, Quaternion rot);

        //Input.GetAxis("Horizontal")
    }
}
