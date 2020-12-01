using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCollision : MonoBehaviour
{
    public List<GameObject> children;
    public EnemyController enemyController;
    public ParticleSystem explosion;
    public AudioClip sound;

    private AudioSource soundManager;

    private bool isNaveDestroyed = false;
    public bool IsNaveDestroyed { get => isNaveDestroyed; set => isNaveDestroyed = value; }

    private GameObject trash;

    [Space(10)]
    [Header("Particles")]
    public ParticleSystem fire1;
    public ParticleSystem fire2;
    public ParticleSystem smoke1;
    public ParticleSystem smoke2;


    private void Awake()
    {
        trash = GameObject.FindGameObjectWithTag("Trash");
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            children.Add(child.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Hand")
        {
            explosion.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            isNaveDestroyed = true;
            soundManager.PlayOneShot(sound, 1.0f);

            var main1 = fire1.main;
            var main2 = fire2.main;
            var main3 = smoke1.main;
            var main4 = smoke2.main;

            main1.loop = false;
            main2.loop = false;
            main3.loop = false;
            main4.loop = false;

            foreach (GameObject child in children)
            {
                child.GetComponent<PolygonCollider2D>().enabled = true;
            }
        }

        if (collision.transform.tag == "AlertOnStart")
        {
            enemyController.AlertOnStart();
        }

        if (collision.transform.tag == "AlertOnEnter")
        {
            enemyController.AlertOnEnter();
        }

        if (collision.transform.tag == "AlertDissapear")
        {
            enemyController.AlertDissapear();
        }
    }
}
