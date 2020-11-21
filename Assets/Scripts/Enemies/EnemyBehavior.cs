using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // CONSTANTS
    private const float angle_offset = -90;
    private const float MAX_LANDING_ANGLE = 180.0f;

    // Global params
    public float speed = 10.0f;
    private bool has_landed = false;

    //Moon info
    // TODO: Get the moon radius and position on start.
    public Vector3 target_pos = new Vector3(0.0f, 0.0f, 0.0f); // The moon pos
    public float moon_radius = 2.7f;

    // Landing params
    public float min_distance_for_landing = 10.0f;  // The min distance between the enemy and the moon to start landing
    private float current_land_rotation = 0.0f;
    private float land_speed;
    

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = target_pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angle_offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Initialize land speed proportionaly of the current speed
        land_speed = speed / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (has_landed)
        {
            return;
        }

        float current_speed = Time.deltaTime * speed;
        Vector3 normalized_dir = (target_pos - transform.position).normalized;
        transform.position = transform.position + normalized_dir * current_speed;

        float distance_from_target = Vector3.Distance(transform.position, target_pos) - moon_radius;
        // If we reach the minimum distance between the object and the target -> Start landing
        if (distance_from_target <= (min_distance_for_landing))
        {
            land(distance_from_target);
        }
    }

    void land(float distance_from_target)
    {
        //Debug.Log(distance_from_target);
        if (distance_from_target <= 0.01f || current_land_rotation >= MAX_LANDING_ANGLE)
        {
            has_landed = true;
            return;
        }

        if (speed != land_speed)
        {
            speed = land_speed;
        }

        float percent_to_land = ((min_distance_for_landing - distance_from_target)/ min_distance_for_landing) * 100.0f;
        float current_rotation = (MAX_LANDING_ANGLE * percent_to_land)/100.0f;
        float actual_rotation_to_increase = current_rotation - current_land_rotation;
        transform.Rotate(Vector3.forward, actual_rotation_to_increase);
        current_land_rotation += actual_rotation_to_increase;

    }
}
