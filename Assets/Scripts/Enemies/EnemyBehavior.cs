using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 0.5f;
    public Vector3 target_pos = new Vector3(0.0f, 0.0f, 0.0f); // The moon pos

    // Maybe we have to change this offset depending the position/rotation of the sprite to rotate it properly.
    private float angle_offset = -90;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = target_pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angle_offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        float current_speed = Time.deltaTime * speed;
        Vector3 new_velocity_vector = (target_pos - transform.position) * current_speed;
        transform.position += new_velocity_vector;
    }

}
