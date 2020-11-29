using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isDead = false;
    public ChamanguitoController chamanguito;
    private NaveCollision naveCollision;

    // CONSTANTS
    private const float angle_offset = -90;
    private const float MAX_LANDING_ANGLE = 180.0f;

    // Global params
    private float max_speed;
    private float ship_radius;
    public float speed = 3.0f;
    private bool is_ready_for_landing = false;

    //Moon info
    public Vector3 target_pos = new Vector3(0.0f, 0.0f, 0.0f); // The moon pos
    private float moon_radius = 2.7f;

    // Landing params
    public float min_distance_for_landing = 4.0f;  // The min distance between the enemy and the moon to start landing
    private float current_land_rotation = 0.0f;
    private float land_speed;

    // Enemy Components
    public GameObject fireVFX1;
    public GameObject fireVFX2;

    public GameObject smokeVFX1;
    public GameObject smokeVFX2;

    private ParticleSystem FireComponent1;
    private ParticleSystem FireComponent2;

    public GameObject nave;

    private bool hasLanded = false;
    private float currentDeadSeconds = 0.0f; 
    public bool HasLanded { get => hasLanded; set => hasLanded = value; }

    private float halfScreenWidth;
    private Vector3 scaleChange;

    private void Awake()
    {
        FireComponent1 = fireVFX1.GetComponent<ParticleSystem>();
        FireComponent2 = fireVFX2.GetComponent<ParticleSystem>();

        halfScreenWidth = Screen.width / 2.0f;
        scaleChange = transform.localScale;

        naveCollision = nave.GetComponent<NaveCollision>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x <= 0)
        {
            transform.localScale = new Vector3(-transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }

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
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLanded || chamanguito.IsDead)
        {
            DestroyAfterDead();
            return;
        }
        else if (!hasLanded && naveCollision.IsNaveDestroyed)
        {
            chamanguito.IsDead = true;
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

            ParticleSystem.MainModule mainFire1 = FireComponent1.main;
            ParticleSystem.MainModule mainFire2 = FireComponent2.main;

            mainFire1.startLifetime = 1.0f - speed_percent;
            mainFire2.startLifetime = 1.0f - speed_percent;

        } else if (land_preparation_percent > REDUCE_SPEED_PHASE && land_preparation_percent <= TURN_ANIMATION_PHASE)
        {
            fireVFX1.gameObject.SetActive(false);
            fireVFX2.gameObject.SetActive(false);

            float rotation_percent = (land_preparation_percent - REDUCE_SPEED_PHASE) / (TURN_ANIMATION_PHASE - REDUCE_SPEED_PHASE);
            float current_rotation = (MAX_LANDING_ANGLE * rotation_percent);
            float actual_rotation_to_increase = current_rotation - current_land_rotation;
            current_land_rotation += actual_rotation_to_increase;
            transform.Rotate(Vector3.forward, actual_rotation_to_increase);

        } else if (!is_ready_for_landing)
        {
            is_ready_for_landing = true;

            smokeVFX1.gameObject.SetActive(true);
            smokeVFX2.gameObject.SetActive(true);
        }
 
        
        
    }

    void land()
    {
        hasLanded = true;

        smokeVFX1.gameObject.SetActive(false);
        smokeVFX2.gameObject.SetActive(false);
    }

    void DestroyAfterDead()
    {
        
        if (!chamanguito.IsDead)
        {
            return;
        }

        float maxSeconds = 5.0f;
        currentDeadSeconds += Time.deltaTime;

        foreach (SpriteRenderer component in GetComponentsInChildren<SpriteRenderer>())
        {
            float currentAlpha = 1.0f - (currentDeadSeconds / maxSeconds);
            component.color = new Color(component.color.r, component.color.g, component.color.b, currentAlpha);
        }
        if (currentDeadSeconds >= maxSeconds)
        {
            Destroy(gameObject);
        }
        
    }
}
