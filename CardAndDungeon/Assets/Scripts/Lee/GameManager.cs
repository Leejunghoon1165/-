using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

            //for(int i =0; i<Input.touchCount; i++)
            //{
            //    tempTouchs = Input.GetTouch(i);
            //    if(tempTouchs.phase == TouchPhase.Began)
            //    {
            //        touch
            //    }
            //}
    }
}
