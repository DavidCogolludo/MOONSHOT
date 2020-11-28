using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;

    [Space(10)]
    [Header("Player")]
    public bool isPlayerDead = false;

    [Space(10)]
    [Header("Audio")]
    [Range(0.0f, 1.0f)]
    public float volumenMusic = 0.0f;
    [Range(0.0f, 1.0f)]
    public float volumenSound = 0.0f;

    [Space(10)]
    [Header("Extra")]
    public bool runInBackground = false;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSourceMusic.Play(0);
        volumenMusic = audioSourceMusic.volume;
        volumenSound = audioSourceSound.volume;
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
}
