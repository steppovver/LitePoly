using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    PathFinder pathFinder;

    [SerializeField] private float _delayBetweenSteps;
    [SerializeField] private float _animDuration;

    [SerializeField] private Vector3 playerOffset;
    [SerializeField] private Vector3 playerOffset2;

    public bool isAlone = false;

    private void Start()
    {
        pathFinder = new PathFinder();
        transform.position = playerOffset2 + playerOffset + pathFinder.getVectorByIndex(0);
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
            Vector3 nextStep = pathFinder.getNextStepPosition(this);
            yield return StartCoroutine(MoveToNextStep(nextStep));
            yield return new WaitForSeconds(_delayBetweenSteps);
        }
        RollADiceButton.Instance.myButton.interactable = true;
    }

    private IEnumerator MoveToNextStep(Vector3 target)
    {
        Vector3 startPosition = transform.position;

        if (!isAlone)
        {
            target += playerOffset2;
        }
        target += playerOffset;

        float t = 0;
        float animDuration = _animDuration;

        while (t < 1)
        {
            Vector3 parabolicPos = Parabola(startPosition, target, 1, t);

            transform.position = parabolicPos;

            t += Time.deltaTime / animDuration;
            yield return null;
        }
        transform.position = target;
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public IEnumerator MoveOverForAnotherPlayer(int whichWayToTurn)
    {
        Vector3 startPosition = transform.position;

        Vector3 target = transform.position + playerOffset2 * whichWayToTurn;

        float t = 0;
        float animDuration = _animDuration / 2;

        while (t < 1)
        {
            Vector3 middlePos = Vector3.Lerp(startPosition, target, t);

            transform.position = middlePos;

            t += Time.deltaTime / animDuration;
            yield return null;
        }
        transform.position = target;
    }

}
