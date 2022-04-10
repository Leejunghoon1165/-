using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Transform target;
    public float speed;

    public Transform CameraLimit;
    public Transform[] Limit;

   
    float height;
    float width;

    //public Vector2 center;
    //public Vector2 size;
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(center, size);
    //}
    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize -0.5f;
        width = height * Screen.width / Screen.height;

        ChangeLimit(0);
    }
    public void ChangeLimit(int x)
    {
        CameraLimit = Limit[x];
    }
    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

        float lx = CameraLimit.localScale.x *0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + CameraLimit.position.x, lx + CameraLimit.position.x);

        float ly = CameraLimit.localScale.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + CameraLimit.position.y, ly + CameraLimit.position.y);
        transform.position = new Vector3(clampX, clampY, -10f);
        // transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}


