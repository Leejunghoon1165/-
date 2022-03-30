using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float speed;

    Rigidbody2D rigid;
    Animator anim;
    Vector2 moveVec;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    //�÷��̾� �̵� 
    void PlayerMove()
    {
        float x = joy.Horizontal;
        float y = joy.Vertical;

        moveVec = new Vector2(x, y) * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        //�̵� �ִϸ��̼�  �� �÷��̾� �¿� ���� 
        if (joy.Horizontal < 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetBool("Run", true);
        }
        if (joy.Horizontal > 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            anim.SetBool("Run", true);
        }
        else if (joy.Horizontal == 0)
            anim.SetBool("Run", false);

        if (moveVec.sqrMagnitude == 0)
            return;
    }
    void Start()
    {

    }

    void Update()
    {

    }

}
