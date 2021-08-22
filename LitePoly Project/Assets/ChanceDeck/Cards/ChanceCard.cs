using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceCard : MonoBehaviour
{
    float _animDuration = 1f;
    float _distanceFromCamera = 3f;

    // Start is called before the first frame update
    void Start()
    {
        ChanceDeck parent = GetComponentInParent<ChanceDeck>();
        Vector3 parentBoundSize = parent.GetComponent<MeshRenderer>().bounds.size;
        transform.position = parent.transform.position + new Vector3(0, parentBoundSize.y/2, 0);

        StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPos = Camera.main.transform.position + Camera.main.transform.forward * _distanceFromCamera;

        float t = 0;
        float animDuration = _animDuration;

        while (t < 1)
        {
            Vector3 midPos = Vector3.Lerp(startPosition, targetPos, t*t);

            transform.position = midPos;

            t += Time.deltaTime / animDuration;
            yield return null;
        }
        transform.position = targetPos;
    }
}
