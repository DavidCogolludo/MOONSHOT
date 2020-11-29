using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float maxSeconds = 5.0f;
        
        foreach (SpriteRenderer component in GetComponentsInChildren<SpriteRenderer>())
        {
            float currentSeconds = (maxSeconds - (component.color.a * maxSeconds) + Time.deltaTime);
            float currentAlpha = 1.0f - (currentSeconds / maxSeconds);
            component.color = new Color(component.color.r, component.color.g, component.color.b, currentAlpha);

            if (currentSeconds >= maxSeconds)
            {
                Destroy(component.gameObject);
            }
        }
    }
}
