using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack : MonoBehaviour
{

    public Animator anim;
    public Transform pos;
    public Vector2 boxSize;
    int strengh;

    void Awake()
    {
        anim = GetComponent<Animator>();
        strengh = GameObject.Find("Slime").GetComponent<MoveManager>().Strengh;
    }

    void Update()
    {
        float dist = GameObject.Find("Slime").GetComponent<MoveManager>().dist;
        float AttackRange = GameObject.Find("Slime").GetComponent<MoveManager>().AttackRange;

        if(dist <= AttackRange){
            anim.SetTrigger("Attack");
            Attack();
        }
    }


    void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            /*
            if(collider.gameObject.tag=="Player")
                collider.GetComponent<Player>().TakeDamage(Strengh);
            */
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}
