﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Step : MonoBehaviour
{
    public List<PlayerMovement> playerMovementList;

    public UnityEvent OnAllScriptsDone;

    public int myIndex; // for Debug public



    private void Start()
    {
        if (OnAllScriptsDone == null)
            OnAllScriptsDone = new UnityEvent();
        OnStart();
    }

    public virtual void OnStart()
    {
        OnAllScriptsDone.AddListener(AllScriptsDone);
    }

    void AllScriptsDone()
    {
        PlayerHandler.Instance.PassTheMoveToNextPlayer();
    }

    public void MoveOverPlayersForAnotherPlayer(int whichWayToTurn)
    {
        StartCoroutine(playerMovementList[0].MoveOverForAnotherPlayer(whichWayToTurn));
    }

    public void IfPlayerStopped(Player player)
    {
        DoOnPlayerStop(player);
    }

    public virtual void DoOnPlayerStop(Player player)
    {
        OnAllScriptsDone.Invoke();
    }

    private void OnMouseOver() // Debug Method
    {
        if (Input.GetMouseButtonDown(1) && Application.platform == RuntimePlatform.WindowsEditor)
        {
            Player activePlayer = PlayerHandler.Instance._activePlayer;
            activePlayer.playerMovement.StartMoving(-1, myIndex);
        }
    }

    public virtual void WhenPlayerGoingThrow(PlayerMovement playerMovement)
    {
        playerMovementList.Add(playerMovement);
    }
}
