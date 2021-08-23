using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScriptOnSteps : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var activePlayer = PlayerHandler.Instance._activePlayer;
            int myIndex = 1;
            activePlayer.StartMoving(-1, 5);
        }
    }
}
