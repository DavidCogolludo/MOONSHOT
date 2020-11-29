using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text textTimer;
    float time = 0;
    bool pausedTimer = false;
    void Start()
    {
        textTimer = gameObject.GetComponent<Text>();
        textTimer.text = "00:00";
        
    }
    
    void Update()
    {
        if (!pausedTimer)
        {
            time += Time.deltaTime;
            int minutes = (int)time / 60;
            float seconds = (int)time % 60;

            textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds); //codifica el formato tiempo
        }

    }


    public void StopTimer()
    {
        pausedTimer = true;
    }
    public void resetTimer()
    {
        time = 0;
    }
    public void playTimer()
    {
        pausedTimer = false;
    }

   
}
