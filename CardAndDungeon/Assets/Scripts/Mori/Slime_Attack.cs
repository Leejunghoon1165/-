using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack : MonoBehaviour
{

    public Animator anim;
    public Transform pos;
    public Vector2 boxSize;
    float strengh;

    void Awake()
    {
        anim = GetComponent<Animator>();
        strengh = this.gameObject.GetComponent<MoveManager>().Strengh;
    }

    void Update()
    {

        if(this.gameObject.GetComponent<Spawn>().mob_num == GameObject.Find("Main Camera").GetComponent<TestCamera>().MapNum)
        {
            float dist = this.gameObject.GetComponent<MoveManager>().dist;
            float AttackRange = this.gameObject.GetComponent<MoveManager>().AttackRange;

            if(dist <= AttackRange){
                anim.SetTrigger("Attack");
                Attack();
            }
        }

    }


    void Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if(collider.gameObject.tag=="Player"){
                //Player.TakeDamage(strengh);
                Debug.Log(strengh);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}
