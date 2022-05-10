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

    SpriteRenderer sprite;

    void Awake()
    {
        anim = GetComponent<Animator>();
        strengh = this.gameObject.GetComponent<MoveManager>().Strengh;
        bombCount = false;
        time = 0;
        sprite = gameObject.GetComponent<SpriteRenderer>();
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
        sprite.color = Color.green;
        if(time > .33f) {
            GameObject.Find("MushRoom").GetComponent<MoveManager>().longRange = true;
            anim.SetTrigger("Attack");
            if(time > 1.33f){
                Attack();
            }
            Destroy(gameObject, 1.5f);
        }
        Debug.Log("boom");
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