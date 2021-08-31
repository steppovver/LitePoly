using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStep : Step
{
    public override void DoOnPlayerStop()
    {
        StartCoroutine(playerMovementList[playerMovementList.Count - 1].MoveToPrison());
    }
}
