using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sensetivity
{
    Horizontal,
    Vertical,
    Zoom,
    Move,
}

public class CameraOrbit : MonoBehaviour
{
    // SINGLETON
    private static CameraOrbit _instance;

    public static CameraOrbit Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public float horizontalRotateSens = 10.0f;
    public float verticalRotateSens = 10.0f;
    public float zoomSpeed = 1.0f;
    public float MoveSens = 1.0f;

    [SerializeField] private Transform Target;
    [SerializeField] private Transform Flor;

    float _yMinLimit = 10f;
    float _yMaxLimit = 90f;
    float _distance = 20.0f;
    float _distanceMin = 5f;
    float _distanceMax = 20f;
    float _smoothTime = 10f;

    float _rotationXAxis = 0.0f;
    float _rotationYAxis = 0.0f;
    float _velocityX = 0.0f;
    float _velocityY = 0.0f;
    float _velocityXMove = 0.0f;
    float _velocityYMove = 0.0f;
    float _velocityZoom = 0.0f;

    Vector3 _position;
    Vector3 _sizeOfField;

    private void Start()
    {
        Target.position = Flor.position;

        GetSizeOfFieled();

        _rotationXAxis = transform.rotation.eulerAngles.x;
        _rotationYAxis = transform.rotation.eulerAngles.y;

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
            _velocityXMove += Input.GetTouch(0).deltaPosition.x * 0.001f * MoveSens;
            _velocityYMove += Input.GetTouch(0).deltaPosition.y * 0.001f* MoveSens;
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
            float turnAngle = Angle(touchA.position, touchB.position);
            float PrevTurn = Angle(touchADirection, touchBDirection);

            float touchDelta = dstBtwTouchesPositions - dstBtwTouchesDirections;
            float turnAngleDelta = Mathf.DeltaAngle(PrevTurn, turnAngle);

            if (Mathf.Abs(touchDelta) < Screen.currentResolution.height/100)
            {
                if (Mathf.Abs(turnAngleDelta) > 1)
                {
                    _velocityX += horizontalRotateSens * turnAngleDelta * 0.01f;
                }
                else
                {
                    _velocityY += verticalRotateSens * Input.GetTouch(0).deltaPosition.y * 0.005f;

                }
            }
            else
            {
                _velocityZoom += zoomSpeed * touchDelta * 0.005f;
            }

        }

        CalculateRotation();
        ChangeDistance();
    }

    void DragMouseOrbit()
    {
        if (Input.GetMouseButton(2))
        {
            _velocityX += horizontalRotateSens * Input.GetAxis("Mouse X") * _distance * 0.02f;
            _velocityY += verticalRotateSens * Input.GetAxis("Mouse Y") * 0.02f;
        }

        CalculateRotation();
    }

    void DragMouseToMove()
    {
        if (Input.GetMouseButton(1))
        {
            _velocityXMove += Input.GetAxis("Mouse X") * 0.02f;
            _velocityYMove += Input.GetAxis("Mouse Y") * 0.02f;
        }

        MoveTarget();
    }

    void ScrollWheelToZoom()
    {
        _velocityZoom += zoomSpeed * Input.mouseScrollDelta.y * zoomSpeed;
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
        _rotationYAxis += _velocityX;
        _rotationXAxis -= _velocityY;
        _rotationXAxis = ClampAngle(_rotationXAxis, _yMinLimit, _yMaxLimit);

        Quaternion rotation = Quaternion.Euler(_rotationXAxis, _rotationYAxis, 0);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -_distance);
        _position = rotation * negDistance + Target.position;

        transform.rotation = rotation;
        transform.position = _position;
        _velocityX = Mathf.Lerp(_velocityX, 0, Time.deltaTime * _smoothTime);
        _velocityY = Mathf.Lerp(_velocityY, 0, Time.deltaTime * _smoothTime);
    }

    void MoveTarget()
    {
        Target.Translate(-_velocityXMove, 0, -_velocityYMove, Space.Self);

        float newXPos = Mathf.Clamp(Target.position.x, Flor.position.x - _sizeOfField.x / 2, Flor.position.x + _sizeOfField.x / 2);
        float newZPos = Mathf.Clamp(Target.position.z, Flor.position.z - _sizeOfField.z / 2, Flor.position.z + _sizeOfField.z / 2);

        Target.position = new Vector3(newXPos, Target.position.y, newZPos);

        _velocityXMove = Mathf.Lerp(_velocityXMove, 0, Time.deltaTime * _smoothTime);
        _velocityYMove = Mathf.Lerp(_velocityYMove, 0, Time.deltaTime * _smoothTime);
    }

    void ChangeDistance()
    {
        _distance = Mathf.Clamp(_distance - _velocityZoom * Time.deltaTime, _distanceMin, _distanceMax);

        _velocityZoom = Mathf.Lerp(_velocityZoom, 0, Time.deltaTime * _smoothTime);
    }

    float Angle (Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        Vector2 to = new Vector2(1, 0);

        float result = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
        {
            result = 360f - result;
        }
        return result;
    }

    public void ChangeSensetivity(Sensetivity sensName, float sensMultiplayer)
    {
        switch (sensName)
        {
            case Sensetivity.Horizontal:
                horizontalRotateSens *= sensMultiplayer;
                break;
            case Sensetivity.Vertical:
                verticalRotateSens *= sensMultiplayer;
                break;
            case Sensetivity.Zoom:
                zoomSpeed *= sensMultiplayer;
                break;
            case Sensetivity.Move:
                MoveSens *= sensMultiplayer;
                break;
        }
    }
}
