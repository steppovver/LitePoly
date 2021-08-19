using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform Target;

    public float distance = 20.0f;
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f;
    public float zoomSpeed = 1.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    float velocityZoom = 0.0f;

    Vector3 position;

    private void Start()
    {
        rotationXAxis = transform.rotation.eulerAngles.x;
        rotationYAxis = transform.rotation.eulerAngles.y;

        Target.position = Vector3.zero;
        Target.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void Update()
    {

            OneTouchInput();
            TwoTouchInput();

        if (Application.platform != RuntimePlatform.Android)
        {
            DragMouseOrbit();

        }


        Target.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void OneTouchInput()
    {
        if (Input.touchCount == 1)
        {
            velocityX += xSpeed * Input.GetTouch(0).deltaPosition.x * 0.02f;
            velocityY += ySpeed * Input.GetTouch(0).deltaPosition.y * 0.02f / 10;
        }

        CalculateRotation();
    }

    void TwoTouchInput()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Vector3 curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
            Vector3 prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta position
            float touchDelta = curDist.magnitude - prevDist.magnitude;
            velocityZoom += zoomSpeed * touchDelta * 0.005f;

            distance = Mathf.Clamp(distance - velocityZoom * Time.deltaTime, 5f, 20f);
            distance = Mathf.Clamp(distance - velocityZoom * Time.deltaTime, 5f, 20f);

            velocityZoom = Mathf.Lerp(velocityZoom, 0, Time.deltaTime * smoothTime);
        }
    }

    void DragMouseOrbit()
    {
        if (Input.GetMouseButton(0))
        {
            velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
            velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
        }

        CalculateRotation();
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    private void CalculateRotation()
    {
        rotationYAxis += velocityX;
        rotationXAxis -= velocityY;
        rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        position = rotation * negDistance + Target.position;

        transform.rotation = rotation;
        transform.position = position;
        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
    }
}
