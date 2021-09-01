using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStep : Step
{
    public override void WhenPlayerGoingThrow(PlayerMovement playerMovement)
    {
        base.WhenPlayerGoingThrow(playerMovement);
        print("start");
        playerMovement.GetComponent<PlayerMoney>().AddPlayerMoney(200000);
    }
}
