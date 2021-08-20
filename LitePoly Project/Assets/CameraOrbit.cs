using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform Target;
    public Transform Flor;

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
    float velocityXMove = 0.0f;
    float velocityYMove = 0.0f;
    float velocityZoom = 0.0f;

    Vector3 _position;
    Vector3 _sizeOfField;

    private void Start()
    {
        Target.position = Flor.position;

        GetSizeOfFieled();

        rotationXAxis = transform.rotation.eulerAngles.x;
        rotationYAxis = transform.rotation.eulerAngles.y;

        Target.position = Vector3.zero;
        Target.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void Update()
    {
        TwoTouchInput();
        OneTouchInput();

        Target.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    void GetSizeOfFieled()
    {
        _sizeOfField = Flor.GetComponent<MeshRenderer>().bounds.size;

        print(_sizeOfField);
    }

    void OneTouchInput()
    {
        if (Input.touchCount == 1)
        {
            velocityXMove += Input.GetTouch(0).deltaPosition.x * 0.001f;
            velocityYMove += Input.GetTouch(0).deltaPosition.y * 0.001f;
        }

        Target.Translate(-velocityXMove, 0, -velocityYMove, Space.Self);

        float newXPos = Mathf.Clamp(Target.position.x, Flor.position.x - _sizeOfField.x/2, Flor.position.x + _sizeOfField.x/2);
        float newZPos = Mathf.Clamp(Target.position.z, Flor.position.z - _sizeOfField.z/2, Flor.position.z + _sizeOfField.z/2);

        Target.position = new Vector3(newXPos, Target.position.y, newZPos);

        velocityXMove = Mathf.Lerp(velocityXMove, 0, Time.deltaTime * smoothTime);
        velocityYMove = Mathf.Lerp(velocityYMove, 0, Time.deltaTime * smoothTime);
    }

    void TwoTouchInput()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Vector3 curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
            Vector3 prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta position
            float touchDelta = curDist.magnitude - prevDist.magnitude;
            velocityZoom += zoomSpeed * touchDelta * 0.005f;


            if (touchDelta < 1000)
            {

                velocityX += xSpeed * Input.GetTouch(0).deltaPosition.x * 0.01f;
                velocityY += ySpeed * Input.GetTouch(0).deltaPosition.y * 0.01f / 10;
            }
            else
            {
                velocityZoom += zoomSpeed * touchDelta * 0.005f;
            }
        }

        CalculateRotation();

        distance = Mathf.Clamp(distance - velocityZoom * Time.deltaTime, 5f, 20f);
        distance = Mathf.Clamp(distance - velocityZoom * Time.deltaTime, 5f, 20f);


        velocityZoom = Mathf.Lerp(velocityZoom, 0, Time.deltaTime * smoothTime);
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
        _position = rotation * negDistance + Target.position;

        transform.rotation = rotation;
        transform.position = _position;
        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
    }
}
