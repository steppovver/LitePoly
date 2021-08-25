using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    int _money = 1000000;

    public int Money { get { return _money; } }

    public void addPlayerMoney(int addMount)
    {
        _money += addMount;
    }
}
