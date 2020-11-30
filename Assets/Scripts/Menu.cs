using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    //public GameObject title;
    public GameManager gameManager;

    private Animator animator;
    bool pausedfromButton = false;


    void Start()
    {
        animator = menu.gameObject.GetComponent<Animator>();
        animator.SetBool("pause", true);
    }

    public void Play()
    {
        //title.SetActive(false);
        if (gameManager.IsPlayerDead)
            SceneManager.LoadScene("DaniScene");

        animator.SetBool("pause", false);
        gameManager.StartGame();
    }

    public void Pause()
    {
        //title.SetActive(true);
        menu.SetActive(true);
        animator.SetBool("pause", true);
        
    }
    
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
