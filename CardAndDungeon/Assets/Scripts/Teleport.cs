using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject toObject;
    public bool Teleportflag;
    public int mapNum;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            targetObject = collision.gameObject;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartCoroutine(TeleportCoroutine());
    }

    IEnumerator TeleportCoroutine()
    {
        yield return null;
        targetObject.transform.position = toObject.transform.position;

       
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
