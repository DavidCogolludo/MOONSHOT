using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCollision : MonoBehaviour
{
    public List<GameObject> children;
    public ParticleSystem explosion;

    private bool isNaveDestroyed = false;
    public bool IsNaveDestroyed { get => isNaveDestroyed; set => isNaveDestroyed = value; }


    private GameObject trash;


    private void Awake()
    {
        trash = GameObject.FindGameObjectWithTag("Trash");
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

            foreach (GameObject child in children)
            {
                child.GetComponent<PolygonCollider2D>().enabled = true;
                child.transform.SetParent(trash.transform);
            }
        }
    }
}
