using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Step : MonoBehaviour
{
    public List<PlayerMovement> playerMovementList;

    public UnityEvent OnAllScriptsDone;

    private void Start()
    {
        if (OnAllScriptsDone == null)
            OnAllScriptsDone = new UnityEvent();

        OnAllScriptsDone.AddListener(AllScriptsDone);
    }

    void AllScriptsDone()
    {
        RollADiceButton.Instance.myButton.interactable = true;
        PlayerHandler.Instance.PassTheMoveToNextPlayer();
    }

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
        OnAllScriptsDone.Invoke();
    }
}
