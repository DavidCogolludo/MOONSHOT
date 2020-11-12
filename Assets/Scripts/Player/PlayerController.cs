using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HandController leftHand;
    public HandController rightHand;

    float halfScreenWidth;

    // Start is called before the first frame update
    void Start()
    {
        halfScreenWidth = Screen.width / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            if (mousePos.x <= halfScreenWidth)
                leftHand.Attack(mouseWorldPos2D);
            else
                rightHand.Attack(mouseWorldPos2D);
        }
    }
}
