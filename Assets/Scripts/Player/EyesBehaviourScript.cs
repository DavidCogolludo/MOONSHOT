using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesBehaviourScript : MonoBehaviour
{

    private SpriteRenderer leftEye;
    private SpriteRenderer rightEye;
    private SpriteRenderer leftPupil;
    private SpriteRenderer rightPupil;

    private float eyeMaxMovementX = 0.0f;
    private float eyeMaxMovementY = 0.0f;

    private Transform leftPupilTransform;
    private Transform rightPupilTransform;

    private Vector3 leftPupilStartPos;
    private Vector3 rightPupilStartPos;

    // Start is called before the first frame update
    void Start()
    {
        Transform leftEyeTransform = transform.Find("moonBody").Find("leftEye");
        leftPupilTransform = leftEyeTransform.Find("leftPupil");
        Transform rightEyeTransform = transform.Find("moonBody").Find("rightEye");
        rightPupilTransform = rightEyeTransform.Find("rightPupil");

        leftPupilStartPos = leftEyeTransform.position;
        
        leftEye = leftEyeTransform.gameObject.GetComponent<SpriteRenderer>();
        leftPupil =leftPupilTransform.gameObject.GetComponent<SpriteRenderer>();

        rightPupilStartPos = rightEyeTransform.position;

        rightEye = rightEyeTransform.gameObject.GetComponent<SpriteRenderer>();
        rightPupil = rightPupilTransform.gameObject.GetComponent<SpriteRenderer>();

        float eyeRadiusX = leftEye.bounds.extents.x / 2.0f;
        float pupilRadiusX = leftPupil.bounds.extents.x / 2.0f;
        float eyeRadiusY = leftEye.bounds.extents.y / 2.0f;
        float pupilRadiusy = leftPupil.bounds.extents.y / 2.0f;

        eyeMaxMovementX = eyeRadiusX - pupilRadiusX;
        eyeMaxMovementY = eyeRadiusY - pupilRadiusy;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 leftEyeToMouse = mousePos - leftPupilStartPos;
        Vector3 rightEyeToMouse = mousePos - rightPupilStartPos;

        float leftAngle = Mathf.Atan2(leftEyeToMouse.y, leftEyeToMouse.x);
        float rightAngle = Mathf.Atan2(rightEyeToMouse.y, rightEyeToMouse.x);

        leftPupilTransform.position = new Vector3((eyeMaxMovementX * Mathf.Cos(leftAngle)) + leftPupilStartPos.x, (eyeMaxMovementY * Mathf.Sin(leftAngle)) + leftPupilStartPos.y, 0.0f);
        rightPupilTransform.position = new Vector3((eyeMaxMovementX * Mathf.Cos(rightAngle)) + rightPupilStartPos.x, (eyeMaxMovementY * Mathf.Sin(rightAngle)) + rightPupilStartPos.y, 0.0f);
    }
}
