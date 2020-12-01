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

            foreach (GameObject child in children)
            {
                child.GetComponent<PolygonCollider2D>().enabled = true;
                child.transform.SetParent(trash.transform);
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
