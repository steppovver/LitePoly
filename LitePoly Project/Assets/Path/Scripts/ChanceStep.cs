using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceStep : Step
{
    public override void Init()
    {
        print("You stand on chance");
        ChanceDeck.Instance.DealNewChanceCard();
    }
}
