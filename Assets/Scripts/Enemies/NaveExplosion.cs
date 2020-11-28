﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveExplosion : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Hand")
        {
            position = collision.transform.position;
            rigidbody2D.AddForce((transform.position - position) * 0.5f, ForceMode2D.Impulse);
        }
    }
}
