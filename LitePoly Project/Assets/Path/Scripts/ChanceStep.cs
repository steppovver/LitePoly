using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceStep : Step
{
    public override void Init()
    {
        ChanceDeck.Instance.DealNewChanceCard(OnAllScriptsDone);
    }
}
