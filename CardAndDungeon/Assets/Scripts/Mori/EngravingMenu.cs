using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngravingMenu : MonoBehaviour
{
    [SerializeField]
    GameObject engravingMenu;

    public GameObject unlockContent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if( Input.touchCount > 0 && engravingMenu.activeSelf == true)
        {
           StartCoroutine(UnlockText());
        }
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
        unlockContent.SetActive(true);
        yield return new WaitForSeconds(2f);
        unlockContent.SetActive(false);
        yield return new WaitForSeconds(2f);
    }

}
