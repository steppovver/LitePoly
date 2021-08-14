using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    // SINGLETON
    private static PlayerHandler _instance;

    public static PlayerHandler Instance { get { return _instance; } }


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


    RollingDice rollingDice;

    [SerializeField] private int _amountOfDice;

    public PlayerMovement[] Players;

    // Start is called before the first frame update
    void Start()
    {
        Players = GetComponentsInChildren<PlayerMovement>();
        rollingDice = FindObjectOfType(typeof(RollingDice)) as RollingDice;

    }

    public void NewPLayerTurn()
    {
        rollingDice.SetUpDicesAndRoll(_amountOfDice, Players[0]);
    }
}
