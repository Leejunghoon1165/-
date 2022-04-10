using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2SPM : MonoBehaviour
{
    public GameObject Worm;

    public Transform spawnpoint1;

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
        spawn = true;
    }
}
