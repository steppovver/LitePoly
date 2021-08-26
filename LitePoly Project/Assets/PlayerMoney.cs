using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int _money = 1000000;
    private int MoneyCheck;

    public int Money { get { return _money; } }

    public void InitMoney()
    {
        MoneyGUIUpdater.Instance.InitMoneyOfPlayer(this);
        MoneyCheck = _money;
    }

    public void addPlayerMoney(int addMount)
    {
        _money = MoneyCheck + addMount;
        MoneyCheck = _money;
        MoneyGUIUpdater.Instance.MoneyGUIUpdate(this);
    }
}
