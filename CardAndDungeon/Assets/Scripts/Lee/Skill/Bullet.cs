using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("DestroytBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.y ==0)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
           
        }
        
    }
    void DestroytBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<MoveManager>().TakeDamage(100);
        }
        if (collision.gameObject.tag == "BackGround" || collision.gameObject.tag == "BackGround")
        {
            Destroy(gameObject);
        }
    }

}


