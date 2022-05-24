using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject cardList;
    public GameObject Message;
    float time;
    bool message;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        message = false;
    }

    // Update is called once per frame
    void Update()
    {
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

    public void NotMake()
    {
        message = true;
    }
}
