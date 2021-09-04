using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStep : Step
{
    public override void DoOnPlayerStop(Player player)
    {
        StartCoroutine(playerMovementList[playerMovementList.Count - 1].MoveToPrison());
    }

    private void Update()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, Mathf.PingPong(Time.time, 1));
    }
}
