using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public VariableJoystick joy;
    public float speed;

    public GameObject attackMotion;
    Rigidbody2D rigid;
    Animator anim;
    Vector2 moveVec;

    [SerializeField]
    private Slider hpBar;
    static float maxHp = 100;
    static float curHp = 100;

    static float damage = 10;

    private float curTime;
    public float coolTime;
    public Transform pos;
    public Vector2 boxSize;
    public bool AttCheck;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        attackMotion.SetActive(false);
        hpBar.value = (float)curHp / (float)maxHp;

    }

    public void Update()
    {
        if(curTime <= 0)
        {
            if(AttCheck == true)
            {
                attackMotion.SetActive(true);
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.gameObject.tag == "Enemy")
                        collider.GetComponent<MoveManager>().TakeDamage(damage);

                }
                anim.SetTrigger("Attk");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {

        PlayerMove();
        HandleHp();
    }

    //�÷��̾� �̵� 
    void PlayerMove()
    {
        float x = joy.Horizontal;
        float y = joy.Vertical;

        //�÷��̾��� ü���� 0���� Ŭ���� ������ ����
        if (curHp > 0)
        {
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
        else
            return;
            //Die();

       
    }
    //���� ���
    public void Attack()
    {
        StartCoroutine(CountAttack());
        

    }
 
    IEnumerator CountAttack()
    {
        AttCheck = true;
        yield return new WaitForSeconds(0.5f);
        attackMotion.SetActive(false);
        AttCheck = false;

    }
    void Die()
    {
        anim.SetTrigger("Die");

    }
    //�÷��̾� ���� ���� �����ֱ�
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    private void HandleHp()
    {
        hpBar.value = Mathf.Lerp(hpBar.value, (float)curHp / (float)maxHp, Time.deltaTime * 10);
    }

    public static void TakeDamage(float damamge)
    {
        curHp = curHp - damage;
        Debug.Log("�ƾ�");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

   

}
