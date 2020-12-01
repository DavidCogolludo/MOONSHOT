﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    private GameObject pauseButton;

    //public GameObject title;
    public GameManager gameManager;

    private Animator animator;
    bool pausedfromButton = false;

    public AudioClip sound;

    private AudioSource soundManager;

    private void Awake()
    {
        pauseButton = transform.Find("PauseButton").gameObject;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }

    void Start()
    {
        animator = menu.gameObject.GetComponent<Animator>();
        animator.SetBool("pause", true);
    }

    public void Play()
    {
        soundManager.PlayOneShot(sound, 1.0f);
        //title.SetActive(false);
        if (gameManager.IsPlayerDead)
            SceneManager.LoadScene("DaniScene");

        animator.SetBool("pause", false);
        pauseButton.SetActive(true);
        gameManager.StartGame();
    }

    public void Pause()
    {
        soundManager.PlayOneShot(sound, 1.0f);
        //title.SetActive(true);
        menu.SetActive(true);
        animator.SetBool("pause", true);
        pauseButton.SetActive(false);
        
    }
    
    public void PauseFromButton()
    {
        pausedfromButton = true;
    }

    public void Exit()
    {
        soundManager.PlayOneShot(sound, 1.0f);
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)|| pausedfromButton)
        {            
            Pause();
            pausedfromButton = false;
        }
    }
    
}
