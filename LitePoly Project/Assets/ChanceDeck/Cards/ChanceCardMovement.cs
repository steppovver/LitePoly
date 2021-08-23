using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceCardMovement : MonoBehaviour
{
    float _animDuration = 1f;
    float _animDurationFirst = 1f;
    float _distanceFromCamera = 3f;

    bool _IsNearCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        ChanceDeck parent = GetComponentInParent<ChanceDeck>();
        Vector3 parentBoundSize = parent.GetComponent<MeshRenderer>().bounds.size;
        transform.position = parent.transform.position + new Vector3(0, parentBoundSize.y/2, 0);

        StartCoroutine(MoveUp());
    }

    private void Update()
    {
        if (_IsNearCamera)
        {
            transform.position = GetPositionInFrontOfCamera();
            transform.rotation = GetRotationInFrontOfCamera();
        }
    }

    private IEnumerator MoveUp()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPos = transform.position + new Vector3(0, 1.5f, 0);

        float t = 0;
        float animDuration = _animDurationFirst;

        while (t < 1)
        {
            Vector3 midPosition = Vector3.Lerp(startPosition, targetPos, t * t);
            transform.position = midPosition;
            t += Time.deltaTime / animDuration;
            yield return null;
        }
        transform.position = targetPos;

        yield return StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Vector3 targetPos = GetPositionInFrontOfCamera();
        Quaternion targetRot = GetRotationInFrontOfCamera();

        float t = 0;
        float animDuration = _animDuration;

        while (t < 1)
        {
            Vector3 midPosition = Vector3.Lerp(startPosition, targetPos, t * t);
            Quaternion midRotation = Quaternion.Lerp(startRotation, targetRot, t);

            transform.position = midPosition;
            transform.rotation = midRotation;

            t += Time.deltaTime / animDuration;
            targetPos = GetPositionInFrontOfCamera();
            targetRot = GetRotationInFrontOfCamera();
            yield return null;
        }
        transform.position = targetPos;
        transform.rotation = targetRot;
        _IsNearCamera = true;

        Destroy(gameObject, 2f);

        //TO DO кнопка подтверждения после которой начинается работать карточка

        ChanceCard chanceCard = GetComponent<ChanceCard>();
        chanceCard.DoSmth();
    }

    private Vector3 GetPositionInFrontOfCamera()
    {
        return Camera.main.transform.position + Camera.main.transform.forward * _distanceFromCamera;
    }

    private static Quaternion GetRotationInFrontOfCamera()
    {
        return Quaternion.LookRotation(Camera.main.transform.up) * Quaternion.Euler(180, 0, 0);
    }
}
