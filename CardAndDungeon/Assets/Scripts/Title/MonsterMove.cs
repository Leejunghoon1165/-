using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator anim;
    bool IdleMove;
    int widthMove;
    int highMove;
    // Start is called before the first frame update
    void Start()
    {
        IdleMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        Idle();
    }

    void Idle()
    {
        if(IdleMove == true)
        {
            StartCoroutine(IdleMoving());
            rigid.velocity = new Vector2(widthMove, highMove);
        }
        if(widthMove != 0 || highMove != 0)
            anim.SetTrigger("Walk");
        else
            anim.SetTrigger("Idle");
    }

    //Idle상태일 때 자동적으로 이동하는 모습 구현을 위한 코루틴함수
    IEnumerator IdleMoving() {
        IdleMove = false;
        widthMove = Random.Range(-1, 2);
        highMove = Random.Range(-1, 2);
        yield return new WaitForSeconds(3f);
        IdleMove = true;
    }
}
