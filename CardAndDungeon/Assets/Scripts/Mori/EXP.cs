using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{
    Vector2 playerpos, pos;
    public float MoveSpeed;
    bool go;
    // Start is called before the first frame update
    void Awake()
    {
        go = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerpos = new Vector2(GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y + 0.3f);

        pos = new Vector2(transform.position.x, transform.position.y);

        if(go == true) {
            transform.position = Vector2.MoveTowards(transform.position, playerpos, Time.deltaTime * MoveSpeed);
        }

        if(pos == playerpos) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player"))
        {
            go = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
    
}
