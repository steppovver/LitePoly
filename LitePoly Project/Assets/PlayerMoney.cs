using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int _money = 1000000;

    public int Money { get { return _money; } }

    private void Start()
    {
        MoneyGUIUpdater.Instance.InitPlayer(this);
    }

    public void addPlayerMoney(int addMount)
    {
        _money += addMount;
    }
}
