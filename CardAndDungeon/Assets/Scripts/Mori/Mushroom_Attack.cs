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
    int strengh;
    void Awake()
    {
        anim = GetComponent<Animator>();
        strengh = GameObject.Find("MushRoom").GetComponent<MoveManager>().Strengh;
        bombCount = false;
    }
    
    void Update()
    {
        float dist = GameObject.Find("MushRoom").GetComponent<MoveManager>().dist;
        float AttackRange = GameObject.Find("MushRoom").GetComponent<MoveManager>().AttackRange;

        if(dist <= AttackRange)
            bombCount = true;

        if(bombCount == true)
            time += Time.deltaTime;
        Bomb();
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
