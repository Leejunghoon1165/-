using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHorn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroytBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroytBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<MoveManager>().TakeDamage(10);
        }
        if (collision.gameObject.tag == "BackGround" || collision.gameObject.tag == "BackGround")
        {
            Destroy(gameObject);
        }
    }


}

