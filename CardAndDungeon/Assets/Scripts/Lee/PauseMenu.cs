using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject go_BaseUI;
    // Start is called before the first frame update
    void Start()
    {
        go_BaseUI.SetActive(false);
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
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        Application.Quit();
    }
}
