using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PathFinder pathFinder;

    [SerializeField] private float _delayBetweenSteps;
    [SerializeField] private float _animDuration;


    private Vector3 playerOffset;

    private void Start()
    {
        pathFinder = new PathFinder();
        playerOffset = transform.transform.position - pathFinder.getVectorByIndex(0);
    }

    public void StartMoving(int amountSteps)
    {
        StartCoroutine(MovementCoroutine(amountSteps));
    }

    private IEnumerator MovementCoroutine(int amountSteps)
    {
        int endOfTurnIndex = pathFinder.CurrentStep + amountSteps;
        while (pathFinder.CurrentStep != endOfTurnIndex)
        {
            Vector3 nextStep = pathFinder.getNextStepPosition();
            yield return StartCoroutine(MoveToNextStep(nextStep));
            yield return new WaitForSeconds(_delayBetweenSteps);
        }
    }

    private IEnumerator MoveToNextStep(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        float t = 0;

        float animDuration = _animDuration;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(startPosition, target + playerOffset, t);
            t += Time.deltaTime / animDuration;
            yield return null;
        }
    }

}
