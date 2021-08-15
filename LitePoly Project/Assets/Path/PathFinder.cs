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

    public Vector3 getNextStepPosition()
    {
        CurrentStep++;
        return PathStep[CurrentStep % PathStep.Length].transform.position;
    }
}
