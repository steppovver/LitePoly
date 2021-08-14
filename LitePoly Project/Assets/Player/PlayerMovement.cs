using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PathFinder pathFinder;

    [SerializeField] private float _delayBetweenSteps;
    [SerializeField] private float _animDuration;

    [SerializeField] private Vector3 playerOffset;
    [SerializeField] private Vector3 playerOffset2;

    private void Start()
    {
        pathFinder = new PathFinder();
        transform.transform.position = playerOffset2 + playerOffset + pathFinder.getVectorByIndex(0);
    }

    public void StartMoving(int amountSteps)
    {
        StopAllCoroutines();
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
        RollADiceButton.Instance.myButton.interactable = true;
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
