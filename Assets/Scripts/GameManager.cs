using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject soundButton;
    public GameObject musicButton;

    private int soundNumber = 0;
    private int musicNumber = 0;

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

        soundButton.transform.Rotate(0.0f, 0.0f, 0.0f);
        musicButton.transform.Rotate(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        General();
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

    public void OnClickMusic()
    {
        musicButton.transform.Rotate(0.0f, 0.0f, musicButton.transform.rotation.z + 90.0f);

        musicNumber++;

        if (musicNumber == 1)
            audioSourceMusic.volume = 0.75f;
        else if (musicNumber == 2)
            audioSourceMusic.volume = 0.25f;
        else if (musicNumber == 3)
            audioSourceMusic.volume = 0.0f;
        else
        {
            audioSourceMusic.volume = 1.0f;
            musicNumber = 0;
        }
    }

    public void OnClickSound()
    {
        soundButton.transform.Rotate(0.0f, 0.0f, soundButton.transform.rotation.z + 90.0f);

        soundNumber++;

        if (soundNumber == 1)
            audioSourceSound.volume = 0.75f;
        else if (soundNumber == 2)
            audioSourceSound.volume = 0.25f;
        else if (soundNumber == 3)
            audioSourceSound.volume = 0.0f;
        else
        {
            audioSourceSound.volume = 1.0f;
            soundNumber = 0;
        }
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
