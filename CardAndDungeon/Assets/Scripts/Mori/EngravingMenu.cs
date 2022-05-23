using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngravingMenu : MonoBehaviour
{
    [SerializeField]
    GameObject engravingMenu;
    public GameObject Message;
    float time;
    bool message;

    public GameObject unlockContent;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        message = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if( Input.touchCount > 0 && engravingMenu.activeSelf == true)
        {
           StartCoroutine(UnlockText());
        }
        /*
        if(message == true) {
            time += Time.deltaTime;
            if(time >= 0 && time <= 2.5f) {
                Message.SetActive(true);
            }
            else {
                Message.SetActive(false);
            }
            
            if(time >= 3.5f) {
                time = 0;
                message = false;
            }
        }   
        */

    }

    public void engravingUI_ON()
    {
        engravingMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void engravingUI_OFF()
    {
        engravingMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    IEnumerator UnlockText()
    {
        if(message == true) {
            message = false;
            time += Time.deltaTime;
            if(time >= 0 && time <= 2.5f) {
                Message.SetActive(true);
            }
            else {
                Message.SetActive(false);
            }
            
            if(time >= 3.5f) {
                time = 0;
                message = false;
            }
        }
        yield return null;
        /*
        unlockContent.SetActive(true);
        yield return new WaitForSeconds(2f);
        unlockContent.SetActive(false);
        yield return new WaitForSeconds(2f);
        */
    }



}
