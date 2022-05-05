using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EW_Bullet : MonoBehaviour
{
    public Animator anim;
    Vector2 playerPos;
    Vector2 pos;
    public float speed;
    float strengh;
    bool touch;

    void Awake()
    {
        Vector2.MoveTowards(this.transform.position, playerPos, speed);

        playerPos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y + 0.4f);

        strengh = GameObject.Find("EpicWorm").GetComponent<MoveManager>().Strengh;
        touch = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(touch == false) {
        this.transform.position = Vector2.MoveTowards(this.transform.position, playerPos, speed * Time.deltaTime);
        }
        pos = new Vector2(this.transform.position.x, this.transform.position.y);
        
        if(pos == playerPos){
            Destroy(gameObject, 0.3f);
            anim.SetTrigger("touch");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player") {
            Destroy(gameObject, 0.3f);
            anim.SetTrigger("touch");
            touch = true;
        }
        if(collision.gameObject.tag == "Player"){
            Player.TakeDamage(strengh);
        }
    }
}