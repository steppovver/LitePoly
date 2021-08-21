using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    public List<PlayerMovement> playerMovementList;

    public void MoveOverPlayersForAnotherPlayer(int whichWayToTurn)
    {
        StartCoroutine(playerMovementList[0].MoveOverForAnotherPlayer(whichWayToTurn));
    }

    public void IfPlayerStopped(PlayerMovement playerMovement)
    {
        Init();
    }

    public virtual void Init()
    {
        print("You stand on plane");
    }
}
