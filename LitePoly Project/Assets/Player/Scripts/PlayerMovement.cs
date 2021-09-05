using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[System.Serializable]
public class Event : UnityEvent<Player> { }

public class PlayerMovement : MonoBehaviour
{
    public Event OnCurrentPlayerStop;

    PathFinder _pathFinder;

    [SerializeField] private float _delayBetweenSteps;
    [SerializeField] private float _animDuration;

    [SerializeField] private Vector3 _playerOffset;
    [SerializeField] private Vector3 _playerOffset2;

    public bool isAlone = false;
    public bool isMoving = false;

    public int numberOfTryToEscape = 3;

    public Player player;


    private void Start()
    {
        player = GetComponent<Player>();

        if (OnCurrentPlayerStop == null)
            OnCurrentPlayerStop = new Event();

        _pathFinder = new PathFinder();
        transform.position = _playerOffset2 + _playerOffset + _pathFinder.getVectorByIndex(0);
    }

    public void StartMoving(int amountSteps, int targetIndex = 0) // targetIndex for Debug 
    {
        isMoving = true;
        StopAllCoroutines();
        StartCoroutine(MovementCoroutine(amountSteps, targetIndex));
    }

    private IEnumerator MovementCoroutine(int amountSteps, int targetIndex) // targetIndex for Debug 
    {
        int endOfTurnIndex = _pathFinder.AddSteps(amountSteps);
        if (amountSteps < 0)
        {
            endOfTurnIndex = targetIndex;
        }

        while (_pathFinder.CurrentStep != endOfTurnIndex)
        {
            Vector3 nextStep = _pathFinder.getNextStepPosition(this);
            yield return StartCoroutine(MoveToNextStep(nextStep));
            yield return new WaitForSeconds(_delayBetweenSteps);
        }


        // when player stoped
        isMoving = false;
        OnCurrentPlayerStop.Invoke(player);
    }

    private IEnumerator MoveToNextStep(Vector3 target)
    {
        Vector3 startPosition = transform.position;

        if (!isAlone)
        {
            target += _playerOffset2;
        }
        target += _playerOffset;

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

        Vector3 target = transform.position + _playerOffset2 * whichWayToTurn;

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

    public IEnumerator MoveToPrison()
    {
        Vector3 startPosition = transform.position;
        PrisonStep prison = _pathFinder.GetPrisonStepPosition(this);
        Vector3 target = prison.transform.position;

        if (!isAlone)
        {
            target += _playerOffset2;
        }
        target += _playerOffset;

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

        numberOfTryToEscape = 3;
        player.isInPrison = true;
        prison.IfPlayerStopped(player);
    }
}
