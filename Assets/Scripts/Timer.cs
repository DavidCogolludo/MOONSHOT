using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public EnemySpawn enemySpawn;

    Text textTimer;
    bool pausedTimer = false;

    float time;

    public int thresholdSpawnSeconds;

    int handicapLevel;
    float handicapTime;
    float totalSeconds;
    float previousSecond;

    bool isSaveData = false;

    public Text record;

    private GameManager gameManager;

    void Start()
    {
        record.text = PlayerPrefs.GetString("record");
        textTimer = gameObject.GetComponent<Text>();
        textTimer.text = "00:00";

        time = 0f;

        handicapLevel = 1;
        handicapTime = 0f;
        totalSeconds = 0f;
        previousSecond = 0f;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        if (!pausedTimer && !gameManager.IsPlayerDead)
        {
            time += Time.deltaTime;
            int minutes = (int)time / 60;
            float seconds = (int)time % 60;

            if (seconds == 0f)
                previousSecond = 0f;

            if (seconds > previousSecond)
            {
                totalSeconds++;
                previousSecond = seconds;
            }

            if (totalSeconds - handicapTime >= thresholdSpawnSeconds)
            {
                handicapTime = totalSeconds;

                switch (handicapLevel)
                {
                    case 1:
                        if (enemySpawn.n_enemies < 25)
                            enemySpawn.n_enemies++;
                        break;
                    case 2:
                        if (enemySpawn.spawn_time_range > 4f)
                            enemySpawn.spawn_time_range--;
                        break;
                    case 3:
                        if (enemySpawn.speed < 8)
                            enemySpawn.speed++;
                        break;
                }

                handicapLevel++;

                if (handicapLevel > 3)
                    handicapLevel = 1;
            }

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
