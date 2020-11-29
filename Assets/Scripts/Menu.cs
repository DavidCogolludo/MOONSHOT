﻿using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    //public GameObject title;
    public GameManager gameManager;

    private Animator animator;

    void Start()
    {
        animator = menu.gameObject.GetComponent<Animator>();
        animator.SetBool("pause", true);
        Debug.Log("Pene");
    }

    public void Play()
    {
        //title.SetActive(false);
        animator.SetBool("pause", false);
        gameManager.StartGame();
    }

    public void Pause()
    {
        //title.SetActive(true);
        menu.SetActive(true);
        animator.SetBool("pause", true);
        
    }
    bool pausedfromButton = false;
    public void PauseFromButton()
    {
        pausedfromButton = true;
       

    }

    public void Exit()
    {
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
