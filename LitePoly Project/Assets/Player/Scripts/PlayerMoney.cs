using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    int _money = 1000000;
    int MoneyCheck;

    public int Money { get { return _money; } }

    public Player player { get; private set; }

    public void InitMoney()
    {
        player = GetComponent<Player>();
        MoneyGUIUpdater.Instance.InitMoneyOfPlayer(this);
        MoneyCheck = _money;
    }


    public int AddPlayerMoney(int addMount)
    {
        while (-addMount > _money)
        {
            if (player.myOwnTownSteps.Count > 0)
            {
                player.myOwnTownSteps[0].SellStep();
            }
            else
            {
                PlayerHandler.Instance.PlayerLose(player);
                _money = -1;
                MoneyGUIUpdater.Instance.MoneyGUIUpdate(this);
                return MoneyCheck;
            }
        }

        _money = MoneyCheck + addMount;
        MoneyCheck = _money;
        MoneyGUIUpdater.Instance.MoneyGUIUpdate(this);
        return -addMount;
    }
}
