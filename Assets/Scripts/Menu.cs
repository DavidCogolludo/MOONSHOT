using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    //public GameObject title;
    public GameManager gameManager;

    public GameObject soundButton;
    public GameObject musicButton;
    public GameObject soundNumButton;
    public GameObject musicNumButton;

    private GameObject pauseButton;

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

        soundButton.SetActive(true);
        musicButton.SetActive(true);

        soundNumButton.SetActive(true);
        musicNumButton.SetActive(true);

        pauseButton.SetActive(false);
    }

    public void Play()
    {
        soundManager.PlayOneShot(sound, 1.0f);
        //title.SetActive(false);
        soundButton.SetActive(false);
        musicButton.SetActive(false);

        soundNumButton.SetActive(false);
        musicNumButton.SetActive(false);

        pauseButton.SetActive(true);

        animator.SetBool("pause", false);
        gameManager.StartGame();

    }

    public void Pause()
    {
        soundManager.PlayOneShot(sound, 1.0f);
        //title.SetActive(true);
        menu.SetActive(true);
        soundButton.SetActive(true);
        musicButton.SetActive(true);
        soundNumButton.SetActive(true);
        musicNumButton.SetActive(true);

        pauseButton.SetActive(false);

        animator.SetBool("pause", true);
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

            if (gameManager.IsPlayerDead)
                SceneManager.LoadScene("DaniScene");
        }
    }
    
}
