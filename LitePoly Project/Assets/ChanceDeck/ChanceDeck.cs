using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceDeck : MonoBehaviour
{
    // SINGLETON
    private static ChanceDeck _instance;

    public static ChanceDeck Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void GetNewCard()
    {
        print("congrat, go one more turn");
        DiceRoller.Instance.OnTrowDiceOneMoreTime.Invoke();
    }
}
