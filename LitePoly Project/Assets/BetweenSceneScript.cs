using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenSceneScript
{
    public int amountOfPlayers;

    private static BetweenSceneScript _instance;

    public static BetweenSceneScript Instance { get { return _instance; } }


    public BetweenSceneScript()
    {
        if (_instance != null && _instance != this)
        {
            return;
        }
        else
        {
            _instance = this;
        }
    }
}
