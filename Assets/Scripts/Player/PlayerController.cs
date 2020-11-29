using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public HandController leftHand;
    public HandController rightHand;

    public GameObject rightEye;
    public GameObject lefthEye;

    public GameObject rightEyeDead;
    public GameObject lefthEyeDead;

    private GameManager gameManager;

    float halfScreenWidth;

    private float moonRadius = 2.7f;

    void Awake()
    {
        setMoonRadius();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        halfScreenWidth = Screen.width / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsPlayerDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector2 mouseWorldPos2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

                if (mousePos.x <= halfScreenWidth)
                    leftHand.Attack(mouseWorldPos2D, 180.0f);
                else
                    rightHand.Attack(mouseWorldPos2D, 0.0f);
            }
        }
        else
        {
            rightEye.SetActive(false);
            lefthEye.SetActive(false);

            rightEyeDead.SetActive(true);
            lefthEyeDead.SetActive(true);

            //leftHand.Dead();
            //rightHand.Dead();
        }
    }

    void setMoonRadius()
    {
        Transform moonBody = transform.Find("moonBody");

        if (moonBody == null)
        {
            Debug.LogError("Could not set moon radius. The moon has not a child named 'moonBody'.");
            return;
        }

        SpriteRenderer moonSprite = moonBody.GetComponent<SpriteRenderer>();
        if (moonSprite == null)
        {
            Debug.LogError("Could not set moon radius. Could not get moon's body sprite");
            return;
        }

        moonRadius = moonSprite.bounds.size.x / 2.0f;
    }

    public float getMoonRadius()
    {
        return moonRadius;
    }
}
