using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
            Destroy(this.gameObject);
    }

    public void RemoveLife()
    {
        lives--;
    }
}
