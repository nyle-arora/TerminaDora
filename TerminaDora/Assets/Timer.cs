using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Timer : MonoBehaviour
{
    private static float time;
    private static float minutes;
    private static float seconds;
    
    Text timer;
    // Start is called before the first frame update
    void Start()
    {
        time = 181;
        timer = GetComponent<Text>();
        timer.text = "3:00";
        Debug.Log("3 minutes");
    }

    // Update is called once per frame
    void Update()
    {
        time = time - Time.deltaTime;

        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);

        minutes.ToString();
        seconds.ToString();

        if (time <= 1)
        {
            timer.text = "";
            GameOverScript.endGame();
        }
        
        if (seconds != 0)
        {
            timer.text = string.Concat(minutes, ":", seconds);
        }
        else
        {
            timer.text = string.Concat(minutes, ":00");
        }
        if (seconds < 10)
        {
            timer.text = string.Concat(minutes, ":0", seconds);
        }
        
    }
}