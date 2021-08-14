using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollADiceButton : MonoBehaviour
{
    public void LetPlayerMove()
    {
        PlayerHandler.Instance.NewPLayerTurn();
    }
}
