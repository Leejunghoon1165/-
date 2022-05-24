using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject go_BaseUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBaseUI()
    {
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OffBaseUI()
    {
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoHome()
    {
        Debug.Log("È¨À¸·Î");
    }
    public void GameOver()
    {
        Application.Quit();
    }
}
