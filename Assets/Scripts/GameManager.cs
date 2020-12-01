using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;

    [Space(10)]
    [Header("Player")]
    private bool isPlayerDead = false;
    public bool IsPlayerDead { get => isPlayerDead; set => isPlayerDead = value; }

    [Space(10)]
    [Header("Audio")]
    [Range(0.0f, 1.0f)]
    public float volumenMusic = 0.0f;
    [Range(0.0f, 1.0f)]
    public float volumenSound = 0.0f;

    [Space(10)]
    [Header("Extra")]
    public bool runInBackground = false;
    public bool startGameDebug = false;

    [Space(10)]
    [Header("Components")]
    public PlayerController playerComponent;
    public EnemySpawn enemiesController;


    // Start is called before the first frame update
    void Start()
    {
        volumenMusic = audioSourceMusic.volume;
        volumenSound = audioSourceSound.volume;

        Time.timeScale = 0;

        playerComponent.enabled = false;
        enemiesController.enabled = false;

        if (startGameDebug)
            StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        General();

        Music();

        Sound();
    }

    void General()
    {
        if (runInBackground)
            Application.runInBackground = true;
        else
            Application.runInBackground = false;
    }

    void Music()
    {
        audioSourceMusic.volume = volumenMusic;
    }

    void Sound()
    {
        audioSourceSound.volume = volumenSound;
    }

    public void StartGame()
    {
        Time.timeScale = 1;

        playerComponent.enabled = true;
        enemiesController.enabled = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

        playerComponent.enabled = false;
        enemiesController.enabled = false;
    }
}
