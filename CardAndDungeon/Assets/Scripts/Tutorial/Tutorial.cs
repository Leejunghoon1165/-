using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject a, b, c;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OnSingleTouch();
        /*
        if(Input.touchCount > 0 ){
            count += 1;
        }
        */
        if(count == 1){
            a.SetActive(true);
            b.SetActive(false);
            c.SetActive(false);
        }
        if(count == 2){
            a.SetActive(false);
            b.SetActive(true);
            c.SetActive(false);
        }
        if(count == 3){
            a.SetActive(false);
            b.SetActive(false);
            c.SetActive(true);
        }

        /*
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
        */


    }

    void OnSingleTouch()
    {
        if(Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Ended) {
                count += 1;
            }
        }

    }
}
