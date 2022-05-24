using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public GameObject A, B;
    public static float num;
    // Start is called before the first frame update
    void Start()
    {
        num = Random.Range(0,2);
        if(num == 0)
            A.SetActive(true);
        else if(num == 1)
            B.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
