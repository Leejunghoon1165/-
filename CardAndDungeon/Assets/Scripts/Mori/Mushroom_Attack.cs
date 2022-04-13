using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_Attack : MonoBehaviour
{
    public Animator anim;
    float time;
    bool bombCount;
    void Awake()
    {
        anim = GetComponent<Animator>();
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

            anim.SetTrigger("Attack");
            Destroy(gameObject, 1.5f);
        }
    }
}
