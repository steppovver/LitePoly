using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStep : Step
{
    public override void WhenPlayerGoingTrow(PlayerMovement playerMovement)
    {
        base.WhenPlayerGoingTrow(playerMovement);
        print("start");
        playerMovement.GetComponent<PlayerMoney>().addPlayerMoney(200000);
    }
}
