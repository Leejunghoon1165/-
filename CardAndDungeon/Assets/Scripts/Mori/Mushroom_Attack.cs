using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_Attack : MonoBehaviour
{
    public Animator anim;
    float time;
    bool bombCount;
    public Transform pos;
    public Vector2 boxSize;
    float strengh;

    void Awake()
    {
        anim = GetComponent<Animator>();
        strengh = this.gameObject.GetComponent<MoveManager>().Strengh;
        bombCount = false;
        time = 0;
    }
    
    void Update()
    {

        if(this.gameObject.GetComponent<Spawn>().mob_num == GameObject.Find("Main Camera").GetComponent<TestCamera>().MapNum)
        {
            float dist = this.gameObject.GetComponent<MoveManager>().dist;
            float AttackRange = this.gameObject.GetComponent<MoveManager>().AttackRange;

            if(dist <= AttackRange) {
                bombCount = true;
            }

            if(bombCount == true) {
                time += Time.deltaTime;
                Bomb();
            }
        }


    }

    void Bomb()
    {    
        if(time > 2f) {
            GameObject.Find("MushRoom").GetComponent<MoveManager>().longRange = true;
            Attack();
            anim.SetTrigger("Attack");
            Destroy(gameObject, 1.5f);
        }
    }

    void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if(collider.gameObject.tag=="Player") {
                Player.TakeDamage(strengh);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}