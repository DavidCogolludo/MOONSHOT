using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text textTimer;
    float time = 0;
    bool pausedTimer = false;
    bool isSaveData = false;

    public Text record;

    private GameManager gameManager;

    void Start()
    {
        record.text = PlayerPrefs.GetString("record");
        textTimer = gameObject.GetComponent<Text>();
        textTimer.text = "00:00";
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        if (!pausedTimer && !gameManager.IsPlayerDead)
        {
            time += Time.deltaTime;
            int minutes = (int)time / 60;
            float seconds = (int)time % 60;

            textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds); //codifica el formato tiempo
        }

        if (gameManager.IsPlayerDead && !isSaveData)
        {
            float savedTime = PlayerPrefs.GetFloat("time");

            if (savedTime == 0.0f || time > savedTime)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                string timeText = string.Format("{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                PlayerPrefs.SetFloat("time", time);
                PlayerPrefs.SetString("record", "Record: " + timeText);
            }

            isSaveData = true;
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
