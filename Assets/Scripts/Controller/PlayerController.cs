using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 속도"), SerializeField]
    float moveSpeed = 5f;
    [Header("회전 속도"), SerializeField]
    float rotSpeed = 5f;

    [SerializeField]
    InputControllerBase controller;
    [SerializeField]
    JoyStickController joystick;

    [SerializeField]
    private Transform playerBodyMove;
    [SerializeField]
    private Transform playerBodyRotate;

    [SerializeField]
    Animator _anim;

    void Update() { if(!PlayerManager.Instance.isDead || !GameManager.Instance._isGamePause) Move(); }

    void Move()
    {
        //Vector2 moveInput = new Vector2(controller._inputDir.x + joystick._InputDir.x, controller._inputDir.y + joystick._InputDir.y);
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        moveInput.Normalize();

        float speed = moveInput.sqrMagnitude * moveSpeed;

        //_anim.SetFloat("Move", speed);

/*        if (_animCtrl != null)
            _animCtrl.Run(speed);*/

        if(speed > 0)
        {
            _anim.SetBool("isMove", true);
            Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);

            playerBodyRotate.position += moveDir * Time.deltaTime * moveSpeed;
            playerBodyRotate.localRotation = Quaternion.Lerp(playerBodyRotate.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * rotSpeed);
            
            playerBodyMove.position = playerBodyRotate.position;

        }
        else
        {
            _anim.SetBool("isMove", false);
        }
    }

}
