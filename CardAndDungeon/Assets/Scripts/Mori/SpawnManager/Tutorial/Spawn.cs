using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public int mob_num;
    int map_num;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(mob_num == map_num)
            this.gameObject.SetActive(true);
        else
            {
                this.gameObject.SetActive(false);
                this.gameObject.transform.position = spawnPoint.position;
            }
    }
}
