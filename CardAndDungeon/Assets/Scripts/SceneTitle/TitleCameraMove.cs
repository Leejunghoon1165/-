using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraMove : MonoBehaviour
{

    public GameObject title;
    public GameObject start;
    public GameObject team;

    public Transform endpos;
    public Transform camerapos;
    public Transform startpos;
    int speed = 15;

    bool spark;

    float time;

    // Start is called before the first frame update
    void Awake()
    {
        time = 0;  
        spark = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 4.5 && spark == false){
            StartCoroutine(Start());
        }

        else if(time > 3) {
            team.SetActive(true);
        }
        else if(time > 1.5) {
            title.SetActive(true);
        }
        camerapos.transform.position = Vector3.MoveTowards(camerapos.transform.position, endpos.position, Time.deltaTime * speed);

        if(camerapos.transform.position.x == endpos.position.x )
            camerapos.transform.position = startpos.position;
    }

    IEnumerator Start()
    {
        if(time > 4.5)
        {
            spark = true;
            start.SetActive(true);
            yield return new WaitForSeconds(.5f);
            start.SetActive(false);
            yield return new WaitForSeconds(.4f);
            spark = false;
        }
    }

}
