using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketOrbit : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3(0, 0, 0);
    public float vel = 20;
    public float distanceFromTarget = 4;
    public float timeToDown = 2;
    public int orbitDirection = -1;
    public float velIncr = 0.01f;
    public float downStep = 0.2f;
    private bool end = false;
    void Start()
    {
        StartCoroutine("OrbitDown");
    }


    IEnumerator OrbitDown()
    {
        while(!end)
        {
            distanceFromTarget-= downStep;
            if (distanceFromTarget <= 1)
                Destroy(transform.gameObject);
            vel += velIncr;
            yield return new WaitForSeconds(timeToDown);
        }
    }
    private void Update()
    {
        transform.RotateAround(targetPosition, Vector3.forward*orbitDirection, vel * Time.deltaTime);
        Vector3 orbitDesiredPosition = (transform.position - targetPosition).normalized * distanceFromTarget + targetPosition;
        transform.position = Vector3.Slerp(transform.position, orbitDesiredPosition, Time.deltaTime * 4);
    }


}
