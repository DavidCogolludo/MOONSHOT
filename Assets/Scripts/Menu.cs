using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    private Animator animator;
    void Start()
    {
        animator = menu.gameObject.GetComponent<Animator>();
        animator.SetBool("pause", true);
    }

   public void play()
   {
        animator.SetBool("pause", false);
    }

    public void Pause()
    {
        menu.SetActive(true);
        animator.SetBool("pause", true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(); //Esto igual no se debe llamar desde este update si no desde un input manager/Game Manager
        }
    }
}
