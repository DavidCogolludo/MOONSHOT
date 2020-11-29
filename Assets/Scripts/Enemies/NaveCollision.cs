using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveCollision : MonoBehaviour
{
    public List<GameObject> children;

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
            GetComponent<BoxCollider2D>().enabled = false;

            foreach (GameObject child in children)
                child.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
}
