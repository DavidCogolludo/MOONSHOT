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
    private float max_speed;
    private float ship_radius;
    public float speed = 3.0f;
    private bool is_ready_for_landing = false;
    private bool has_landed = false;

    //Moon info
    public Vector3 target_pos = new Vector3(0.0f, 0.0f, 0.0f); // The moon pos
    private float moon_radius = 2.7f;

    // Landing params
    public float min_distance_for_landing = 4.0f;  // The min distance between the enemy and the moon to start landing
    private float current_land_rotation = 0.0f;
    private float land_speed;

    // Enemy Components
    public Transform FireTransform;
    private ParticleSystem FireComponent;
    public Transform SmokeComponent;

    public GameObject nave;


    // Start is called before the first frame update
    void Start()
    {

        max_speed = speed;
        // Initialize land speed proportionaly of the current speed
        // For the moment we reduce a half of the current speed and we can change this if we want
        land_speed = speed / 3.0f;

        // Get the moon position and radius
        GameObject moon = GameObject.Find("Moon");
        if (moon == null)
        {
            Debug.LogError("Could not get moon radius and position. The 'Moon' object does not exist.");
            return;
        }
        // Set the enemy target position
        target_pos = moon.transform.position;
        PlayerController playerController = moon.GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("Could not get player controller to obtain the moon radius.");
            return;
        }

        //Set the moon radius. Necessary for landing.
        moon_radius = playerController.getMoonRadius();

        // Ste the sprite direction
        Vector3 dir = target_pos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angle_offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Set the fire component
        ship_radius = nave.transform.GetComponent<BoxCollider2D>().bounds.size.y / 2.0f;
        FireComponent = FireTransform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (has_landed)
        {
            return;
        }

        
        float distance_from_target = Vector3.Distance(transform.position, target_pos) - moon_radius - ship_radius;
        if (distance_from_target <= 0.0f)
        {
            land();
        }

        // If we reach the minimum distance between the object and the target -> Start landing
        if ((distance_from_target <= min_distance_for_landing) && !is_ready_for_landing)
        {
            PrepareForLanding(distance_from_target);
        }
        
        
        Move();
        

    }

    private void Move()
    {
        float current_speed = Time.deltaTime * speed;
        Vector3 normalized_dir = (target_pos - transform.position).normalized;
        transform.position = transform.position + normalized_dir * current_speed;
    }
    void PrepareForLanding(float distance_from_target)
    {

        float land_preparation_percent = ((min_distance_for_landing - distance_from_target) / min_distance_for_landing) * 100.0f;
        float REDUCE_SPEED_PHASE = 33.33f;
        float TURN_ANIMATION_PHASE = 66.66f;

        if (land_preparation_percent <= REDUCE_SPEED_PHASE)
        {
            float speed_percent = (land_preparation_percent / REDUCE_SPEED_PHASE);
            float speed_relation = (max_speed - land_speed) * speed_percent;
            speed = max_speed -speed_relation;
            var main_fire = FireComponent.main;
            main_fire.startLifetime = 1.0f - speed_percent;
        } else if (land_preparation_percent > REDUCE_SPEED_PHASE && land_preparation_percent <= TURN_ANIMATION_PHASE)
        {
            if (FireTransform.gameObject.activeSelf)
            {
                FireTransform.gameObject.SetActive(false);
            }
            float rotation_percent = (land_preparation_percent - REDUCE_SPEED_PHASE) / (TURN_ANIMATION_PHASE - REDUCE_SPEED_PHASE);
            float current_rotation = (MAX_LANDING_ANGLE * rotation_percent);
            float actual_rotation_to_increase = current_rotation - current_land_rotation;
            current_land_rotation += actual_rotation_to_increase;
            transform.Rotate(Vector3.forward, actual_rotation_to_increase);

        } else if (!is_ready_for_landing)
        {
            is_ready_for_landing = true;
            SmokeComponent.gameObject.SetActive(true);
        }
 
        
        
    }

    void land()
    {

        has_landed = true;
        SmokeComponent.gameObject.SetActive(false);


    }
}
