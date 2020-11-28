using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonWalker : MonoBehaviour
{

    private const float angle_offset = -90;
    // Start is called before the first frame update

    public float speed = 0.3f;
    private Vector3 moonPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 targetPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private float moonRadius = 2.7f;
    private float currentObjectRadius = 0.0f;
    private bool arriveToTarget = false;

    void Start()
    {
        // Set the sprite direction
        Vector3 dir = transform.position - moonPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angle_offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
        if (arriveToTarget)
        {
            return;
        }
        Move();
    }

    void Move()
    {
        float currentSpeed = speed * Time.deltaTime;
        Vector3 dirMoonToPlayer = transform.position - moonPosition;
        float currentAngle = Mathf.Atan2(dirMoonToPlayer.y, dirMoonToPlayer.x);

        if (transform.position.x < 0.0f)
        {
            currentAngle -= currentSpeed;
        }
        else
        {
            currentAngle += currentSpeed;
        }

        float totalRadius =  moonRadius + currentObjectRadius; //Vector3.Distance(transform.position, moonPosition);
        transform.position = new Vector3(totalRadius * Mathf.Cos(currentAngle), totalRadius * Mathf.Sin(currentAngle), 0.0f);

        Vector3 dir = transform.position - moonPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + angle_offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float distanceFromTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceFromTarget <= 0.001f)
        {
            arriveToTarget = true;
        }
    }
}
