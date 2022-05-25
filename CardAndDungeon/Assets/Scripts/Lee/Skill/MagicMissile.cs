using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    public float speed;
    public GameObject target;
    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy");
        if (target == null)
            return;
       
          
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 10);
         //transform.Translate(transform.right * speed * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            collision.GetComponent<MoveManager>().TakeDamage(10);
            Invoke("DestroytBullet", 1);
        }
    }
    void DestroytBullet()
    {
        Destroy(gameObject);
    }
}
