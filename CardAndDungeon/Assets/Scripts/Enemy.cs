using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float Hp = 100;
    public void TakeDamage(float damage)
    {
        Hp = Hp - damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
