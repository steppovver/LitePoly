using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        DeletePlayerFromStep(PathStep[CurrentStep], playerMovement);

        CurrentStep++;
        CurrentStep = CurrentStep % PathStep.Length;

        Step nextStep = PathStep[CurrentStep];
        AddPlayerToStep(nextStep, playerMovement);
        return nextStep.transform.position;
    }

    private void DeletePlayerFromStep(Step previusStep, PlayerMovement playerMovement)
    {
        if (previusStep.playerMovementList.Contains(playerMovement))
        {
            previusStep.playerMovementList.Remove(playerMovement);
            playerMovement.OnCurrentPlayerStop.RemoveListener(previusStep.IfPlayerStopped);
        }

        if (previusStep.playerMovementList.Count == 1)
        {
            previusStep.MoveOverPlayersForAnotherPlayer(-1); // move from corner to center
        }
    }

    private void AddPlayerToStep(Step nextStep, PlayerMovement playerMovement)
    {
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


        nextStep.WhenPlayerGoingThrow(playerMovement);
        playerMovement.OnCurrentPlayerStop.AddListener(nextStep.IfPlayerStopped);
    }

    public int AddSteps(int amountSteps)
    {
        int targetStepIndex = CurrentStep + amountSteps;
        targetStepIndex = targetStepIndex % PathStep.Length;
        return targetStepIndex;
    }

    public PrisonStep GetPrisonStepPosition(PlayerMovement playerMovement)
    {
        DeletePlayerFromStep(PathStep[CurrentStep], playerMovement);
        PrisonStep prisonStep = InitPath.Instance.PrisonStep;
        CurrentStep = prisonStep.myIndex;
        AddPlayerToStep(prisonStep, playerMovement);
        return prisonStep;
    }
}
