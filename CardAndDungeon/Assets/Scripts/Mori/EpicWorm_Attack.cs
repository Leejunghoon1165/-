using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicWorm_Attack : MonoBehaviour
{
    public Animator anim;
    public GameObject bullet;
    public Transform bulletPos;
    Vector2 BulletPos;
    Vector2 TargetPos;

    float count;
    void Awake()
    {   
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        float dist = GameObject.Find("EpicWorm").GetComponent<MoveManager>().dist;
        float AttackRange = GameObject.Find("EpicWorm").GetComponent<MoveManager>().AttackRange;

        if(dist <= AttackRange)
        {
            if(GameObject.Find("EpicWorm").GetComponent<MoveManager>().longRange == false)  {
                StartCoroutine(Attack());
            }
        }
        else
            count = 0;
    }

    IEnumerator Attack()
    {

        
        GameObject.Find("EpicWorm").GetComponent<MoveManager>().longRange = true;
        if(count != 0) {
            GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        }
        anim.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.8f);
        count += 1;
        GameObject.Find("EpicWorm").GetComponent<MoveManager>().longRange = false;
    }
}
