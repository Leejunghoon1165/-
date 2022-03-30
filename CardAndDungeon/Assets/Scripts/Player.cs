using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float speed;

    public GameObject attackMotion;
    Rigidbody2D rigid;
    Animator anim;
    Vector2 moveVec;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();
        attackMotion.SetActive(false);

    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    //플레이어 이동 
    void PlayerMove()
    {
        float x = joy.Horizontal;
        float y = joy.Vertical;

        moveVec = new Vector2(x, y) * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + moveVec);

        //이동 애니메이션  및 플레이어 좌우 반전 
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

    private float curTime;
    public float coolTime = 0.5f;
    public Transform pos;
    public Vector2 boxSize;
    
    public void Attack()
    {

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            Debug.Log(collider.tag);
        }
        anim.SetTrigger("Attk");
        StartCoroutine(CountAttack());

    }
    IEnumerator CountAttack()
    {
        attackMotion.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackMotion.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    void Start()
    {

    }

    void Update()
    {

    }

}
