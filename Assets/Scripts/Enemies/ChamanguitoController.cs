using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamanguitoController : MonoBehaviour
{

    private const float angle_offset = -90;
    // Start is called before the first frame update

    public float speed = 0.3f;
    private Vector3 moonPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private float moonRadius = 2.7f;
    private float currentObjectRadius = 0.2f;
    private bool arriveToTarget = false;

    private bool isDead = false;
    private bool isWalking = false;
    private bool isAttack = false;

    private EnemyController enemyController;
    private Animator animator;
    private float randomTimeWaitIdle;

    [Space(10)]
    [Header("Idle Wait")]
    [Range(0.0f, 10.0f)]
    public float minIdleTime = 0.0f;
    [Range(0.0f, 20.0f)]
    public float maxIdleTime = 0.0f;

    private void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Set the sprite direction
        Vector3 dir = transform.position - moonPosition;

        randomTimeWaitIdle = Random.Range(minIdleTime, maxIdleTime);

        GameObject moon = GameObject.FindGameObjectWithTag("Player");

        if (moon == null)
        {
            Debug.LogError("Could not get moon radius and position. The 'Moon' object does not exist.");
            return;
        }
        // Set the enemy target position
        moonPosition = moon.transform.position;
        PlayerController playerController = moon.GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("Could not get player controller to obtain the moon radius.");
            return;
        }

        //Set the moon radius. Necessary for walk arround it.
        moonRadius = playerController.getMoonRadius();

        SpriteRenderer objSprite = gameObject.GetComponent<SpriteRenderer>();

        if (objSprite != null)
        {
            currentObjectRadius = objSprite.bounds.extents.y / 2.0f;
        }

        targetPosition = new Vector3(moonPosition.x, moonPosition.y + moonRadius + currentObjectRadius, moonPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.HasLanded && !isWalking)
        {
            animator.SetBool("isJumping", true);

            Jump(Vector3.zero);
        }

        if (!arriveToTarget && isWalking && !isAttack)
        {
            animator.SetBool("isWalking", true);
            Move();
        }

        if (isAttack)
        {
            animator.SetBool("isAttack", true);
        }
    }

    float CalculoWeyler()
    {
        float currentSpeed = speed * Time.deltaTime;
        Vector3 dirMoonToPlayer = transform.position - moonPosition;
        float currentAngle = Mathf.Atan2(dirMoonToPlayer.y, dirMoonToPlayer.x);

        if (transform.position.x < 0.0f)
            currentAngle -= currentSpeed;
        else
            currentAngle += currentSpeed;

        return currentAngle;
    }

    void Jump(Vector3 destination)
    {
        float distanceFromTarget = Vector3.Distance(transform.position, destination);

        if (distanceFromTarget <= 2.5f)
        {
            animator.SetBool("isIdle", true);

            randomTimeWaitIdle -= 0.1f;

            if (randomTimeWaitIdle <= 0.0f)
                isWalking = true;
        }
        else
        {
            float current_speed = Time.deltaTime * 0.3f;
            Vector3 normalized_dir = (destination - transform.position).normalized;
            transform.position = transform.position + normalized_dir * current_speed;
        }
    }

    void Move()
    {
        float currentAngle = CalculoWeyler();

        float totalRadius =  moonRadius + currentObjectRadius; //Vector3.Distance(transform.position, moonPosition);
        transform.position = new Vector3(totalRadius * Mathf.Cos(currentAngle), totalRadius * Mathf.Sin(currentAngle), transform.position.z);

        Vector3 dir = transform.position - moonPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angle_offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceFromTarget <= 0.001f)
        {
            arriveToTarget = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);

        if (collision.transform.tag == "Hand" && enemyController.HasLanded)
        {
            isDead = true;
            GetComponent<PolygonCollider2D>().enabled = true;
        }
        
        if (collision.transform.tag == "ZoneAttack")
        {
            isAttack = true;
        }
    }
}
