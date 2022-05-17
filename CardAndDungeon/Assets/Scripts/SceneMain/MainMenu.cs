using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject cardList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OpenCardList()
    {
        cardList.SetActive(true);
    }

    public void CloseCardList()
    {
        cardList.SetActive(false);
    }

}
