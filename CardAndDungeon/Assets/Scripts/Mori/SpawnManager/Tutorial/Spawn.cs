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
        
    }

    // Update is called once per frame
    void Update()
    {
        map_num = GameObject.Find("Main Camera").GetComponent<TestCamera>().MapNum;
        
        if(mob_num != map_num) {
            this.gameObject.transform.position = spawnPoint.position;
            this.gameObject.GetComponent<MoveManager>().cur_HP = this.gameObject.GetComponent<MoveManager>().max_HP;
        }
    }
}
