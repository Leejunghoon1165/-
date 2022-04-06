using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicWorm_Attack : MoveManager
{
    bool attack;
    bool damage;
    void Awake()
    {
        attack = false;
        damage = false;
    }

    void FixedUpdate()
    {
        if(dist < AttackRange)
        {
            attack = true;
            if(damage == false)
                StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        damage = true;
        yield return new WaitForSeconds(.33f);
        if(attack == true)
            //데이지함수 호출
        damage = false;
    }
}
