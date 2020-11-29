using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public float secondsBeforeDestroy = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        foreach (SpriteRenderer component in GetComponentsInChildren<SpriteRenderer>())
        {
            float currentSeconds = (secondsBeforeDestroy - (component.color.a * secondsBeforeDestroy) + Time.deltaTime);
            float currentAlpha = 1.0f - (currentSeconds / secondsBeforeDestroy);
            component.color = new Color(component.color.r, component.color.g, component.color.b, currentAlpha);

            if (currentSeconds >= secondsBeforeDestroy)
            {
                Destroy(component.gameObject);
            }
        }
    }
}
