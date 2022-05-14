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

    void Update()
    {
        float dist = this.gameObject.GetComponent<MoveManager>().dist;
        float AttackRange = this.gameObject.GetComponent<MoveManager>().AttackRange;

        if(dist <= AttackRange)
        {
            if(this.gameObject.GetComponent<MoveManager>().longRange == false)  {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {    
        this.gameObject.GetComponent<MoveManager>().longRange = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.74f);
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
    
        this.gameObject.GetComponent<MoveManager>().longRange = false;
    }
}
