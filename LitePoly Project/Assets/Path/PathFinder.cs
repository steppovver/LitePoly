using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{

    Step[] PathStep;

    public int CurrentStep { get; private set; }

    public PathFinder()
    {
        PathStep = InitPath.Instance.PathStep;
        CurrentStep = 0;
    }

    public Vector3 getVectorByIndex(int index)
    {
        index = index % PathStep.Length;
        return PathStep[index].transform.position;
    }

    public Vector3 getNextStepPosition(PlayerMovement playerMovement)
    {
        Step previusStep = PathStep[CurrentStep % PathStep.Length];
        if (previusStep.playerMovementList.Contains(playerMovement))
        {
            previusStep.playerMovementList.Remove(playerMovement);
        }

        if (previusStep.playerMovementList.Count == 1)
        {
            previusStep.MoveOverPlayersForAnotherPlayer(-1); // move from corner to center
        }

        CurrentStep++;

        Step nextStep = PathStep[CurrentStep % PathStep.Length];

        if (nextStep.playerMovementList.Count > 0)
        {
            playerMovement.isAlone = false;
            if (nextStep.playerMovementList.Count == 1)
            {
                nextStep.MoveOverPlayersForAnotherPlayer(1); // move from center to corner
            }
        }
        else
        {
            playerMovement.isAlone = true;
        }

        nextStep.playerMovementList.Add(playerMovement);


        return nextStep.transform.position;
    }
}
