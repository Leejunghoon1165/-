using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float setTime = 10.0f;
    [SerializeField] Text countDownText;
    // Start is called before the first frame update
    void Start()
    {
        countDownText.text = setTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (setTime > 0)
            setTime -= Time.deltaTime;
        else if (setTime <= 0)
            Time.timeScale = 0.0f;
        countDownText.text = Mathf.Round(setTime).ToString();
        
    }
}
