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
    public float min_distance_for_landing = 10.0f;

    // Maybe we have to change this offset depending the position/rotation of the sprite to rotate it properly.
    private float angle_offset = -90;
    private float current_land_rotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = target_pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angle_offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float moon_radius = 2.7f;
        target_pos = new Vector3((float)(moon_radius * Mathf.Cos(angle)), (float)(moon_radius * Mathf.Sin(angle)), 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float current_speed = Time.deltaTime * speed;
        Vector3 new_velocity_vector = (target_pos - transform.position) * current_speed;
        transform.position += new_velocity_vector;

        float distance_from_target = Vector3.Distance(transform.position, target_pos);

        // If we reach the minimum distance between the object and the target -> Start landing
        if (distance_from_target <= min_distance_for_landing)
        {
            land(distance_from_target);
        }
    }

    void land(float distance_from_target)
    {
        //Debug.Log(distance_from_target);
        if (distance_from_target <= 0 || current_land_rotation >= 180.0f)
        {
            return;
        }

        float max_rotation = 180.0f;

        float percent_to_land = ((min_distance_for_landing - distance_from_target)/ min_distance_for_landing) * 100.0f;
        float current_rotation = (max_rotation * percent_to_land)/100.0f;
        float actual_rotation_to_increase = current_rotation - current_land_rotation;
        transform.Rotate(Vector3.forward, actual_rotation_to_increase);
        current_land_rotation += actual_rotation_to_increase;

    }
}
