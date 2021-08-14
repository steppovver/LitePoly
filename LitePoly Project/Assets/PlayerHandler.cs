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

        PlayersInit();
    }


    RollingDice rollingDice;

    [SerializeField] private int _amountOfDice;
    [SerializeField] private int _numberOfPlayers;

    private int numberOfMoves = 0;

    [SerializeField] private List<GameObject> playersPrefabs;
    public PlayerMovement[] Players;

    // Start is called before the first frame update
    void Start()
    {
        Players = GetComponentsInChildren<PlayerMovement>();
        rollingDice = FindObjectOfType(typeof(RollingDice)) as RollingDice;
    }

    void PlayersInit()
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            Instantiate(playersPrefabs[i], transform);
        }
    }

    public void NewPLayerTurn()
    {
        numberOfMoves++;
        rollingDice.SetUpDicesAndRoll(_amountOfDice, Players[numberOfMoves % _numberOfPlayers]);
    }
}
