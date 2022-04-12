using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack : MonoBehaviour
{

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float dist = GameObject.Find("Slime").GetComponent<MoveManager>().dist;
        float AttackRange = GameObject.Find("Slime").GetComponent<MoveManager>().AttackRange;

        if(dist <= AttackRange)
            anim.SetTrigger("Attack");
    }
}
