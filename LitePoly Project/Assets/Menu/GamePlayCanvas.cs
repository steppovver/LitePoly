using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GamePlayCanvas : MonoBehaviour
{
    // SINGLETON
    private static GamePlayCanvas _instance;

    public static GamePlayCanvas Instance { get { return _instance; } }

    [HideInInspector] public PrisonCanvas PrisonCanvas;
    [HideInInspector] public TownBuyCanvas TownBuyCanvas;

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

        PrisonCanvas = GetComponent<PrisonCanvas>();
        TownBuyCanvas = GetComponent<TownBuyCanvas>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// 

}
