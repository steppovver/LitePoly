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
    public float mouseZoomSpeed = 1.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 5f;
    public float distanceMax = 20f;
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

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            DragMouseOrbit();
            DragMouseToMove();
            ScrollWheelToZoom();
        }

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

        MoveTarget();
    }

    void TwoTouchInput()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            Touch touchA = Input.GetTouch(0);
            Touch touchB = Input.GetTouch(1);
            Vector2 touchADirection = touchA.position - touchA.deltaPosition;
            Vector2 touchBDirection = touchB.position - touchB.deltaPosition;

            float dstBtwTouchesPositions = Vector2.Distance(touchA.position, touchB.position);
            float dstBtwTouchesDirections = Vector2.Distance(touchADirection, touchBDirection);

            float touchDelta = dstBtwTouchesPositions - dstBtwTouchesDirections;
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

        ChangeDistance();
    }

    void DragMouseOrbit()
    {
        if (Input.GetMouseButton(2))
        {
            velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
            velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
        }

        CalculateRotation();
    }

    void DragMouseToMove()
    {
        if (Input.GetMouseButton(1))
        {
            velocityXMove += Input.GetAxis("Mouse X") * 0.02f;
            velocityYMove += Input.GetAxis("Mouse Y") * 0.02f;
        }

        MoveTarget();
    }

    void ScrollWheelToZoom()
    {
        velocityZoom += zoomSpeed * Input.mouseScrollDelta.y * mouseZoomSpeed;
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

    void MoveTarget()
    {
        Target.Translate(-velocityXMove, 0, -velocityYMove, Space.Self);

        float newXPos = Mathf.Clamp(Target.position.x, Flor.position.x - _sizeOfField.x / 2, Flor.position.x + _sizeOfField.x / 2);
        float newZPos = Mathf.Clamp(Target.position.z, Flor.position.z - _sizeOfField.z / 2, Flor.position.z + _sizeOfField.z / 2);

        Target.position = new Vector3(newXPos, Target.position.y, newZPos);

        velocityXMove = Mathf.Lerp(velocityXMove, 0, Time.deltaTime * smoothTime);
        velocityYMove = Mathf.Lerp(velocityYMove, 0, Time.deltaTime * smoothTime);
    }

    void ChangeDistance()
    {
        distance = Mathf.Clamp(distance - velocityZoom * Time.deltaTime, distanceMin, distanceMax);

        velocityZoom = Mathf.Lerp(velocityZoom, 0, Time.deltaTime * smoothTime);
    }
}
