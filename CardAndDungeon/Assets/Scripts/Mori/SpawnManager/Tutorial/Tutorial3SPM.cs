using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3SPM : MonoBehaviour
{

    public GameObject Worm;

    public GameObject EpicWorm;
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public Transform spawnpoint3;

    bool spawn;

    // Start is called before the first frame update
    void Start()
    {
        spawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn == false)
            Spawn();      
    }

    void Spawn()
    {
        Instantiate(Worm, spawnpoint1);
        Instantiate(Worm, spawnpoint2);
        Instantiate(EpicWorm, spawnpoint3);
        spawn = true;
    }
}
