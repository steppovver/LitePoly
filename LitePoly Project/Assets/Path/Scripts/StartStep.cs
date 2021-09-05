using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStep : Step
{
    public override void WhenPlayerGoingThrow(PlayerMovement playerMovement)
    {
        base.WhenPlayerGoingThrow(playerMovement);
        print("start");
        playerMovement.player.numberOfLap++;
        playerMovement.player.playerMoney.AddPlayerMoney(200000);
    }
}
