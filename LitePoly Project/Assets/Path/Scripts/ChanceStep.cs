using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ChanceStep : Step
{
    public override void DoOnPlayerStop(Player player)
    {
        ChanceDeck.Instance.DealNewChanceCard(OnAllScriptsDone);
    }
}
