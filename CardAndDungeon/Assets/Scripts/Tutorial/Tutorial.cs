using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject a, b, c;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.touchCount == 1){
            Debug.Log("첫번째");
            a.SetActive(true);
            b.SetActive(false);
            c.SetActive(false);
        }
        if( Input.touchCount == 2){
            Debug.Log("두번째");
            a.SetActive(false);
            b.SetActive(true);
            c.SetActive(false);
        }
        if( Input.touchCount == 3){
            Debug.Log("세번째");
            a.SetActive(false);
            b.SetActive(false);
            c.SetActive(true);
        }
    }
}
