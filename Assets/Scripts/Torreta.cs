using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject torreta;
    GameObject t;
    GameObject ChildGameObject1;
    public GameObject bullet;
    void Start()
    {
         t = Instantiate(torreta);
         ChildGameObject1 = t.transform.GetChild(0).gameObject;
        //t.AddComponent<SpriteRenderer>().sprite = torreta;
        Debug.Log(gameObject.transform.localScale.x);
        int r = Random.RandomRange(0, 360);
        t.transform.Rotate(Vector3.forward, r);
        t.transform.localPosition = t.transform.up * gameObject.transform.localScale.x;

        StartCoroutine("Shoot");

        //transform.LookAt(near);
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject b = Instantiate(bullet);
            b.transform.position = ChildGameObject1.transform.position;
            b.transform.position = new Vector3(b.transform.position.x,b.transform.position.y, 3);
            b.transform.localRotation = ChildGameObject1.transform.rotation;
            b.GetComponent<Rigidbody2D>().AddForce(b.transform.up * -500);
            Destroy(b, 4);
            yield return new WaitForSeconds(1);
        }
    }
    Transform GetClosestEnemy(RocketOrbit[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (RocketOrbit go in enemies)
        {
            float dist = Vector3.Distance(go.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = go.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    // Update is called once per frame
    void Update()
    {
        Transform near = GetClosestEnemy(GameObject.FindObjectsOfType<RocketOrbit>());

        ChildGameObject1.transform.LookAt(near, Vector3.forward);//rotation.SetLookRotation(near.position);
        
        
    }
}
