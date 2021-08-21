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
/// <summary>
/// 
/// </summary>

    DiceRoller rollingDice;

    [SerializeField] private int _amountOfDice;
    [SerializeField] private int _numberOfPlayers;

    private int _numberOfMoves = 0;
    private int _indexOfActivePlayer = 0;
    private bool _isOneMoreAttempt = false;

    [SerializeField] private List<GameObject> playersPrefabs;
    public PlayerMovement[] Players;

    PlayerMovement _activePlayer;

    void PlayersInit()
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            Instantiate(playersPrefabs[i], transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Players = GetComponentsInChildren<PlayerMovement>();

        for (int i = 0; i < Players.Length; i++)
        {
            PlayerMovement temp = Players[i];
            int randomIndex = Random.Range(i, Players.Length);
            Players[i] = Players[randomIndex];
            Players[randomIndex] = temp;
        }

        PassTheMoveToNextPlayer();

        rollingDice = DiceRoller.Instance;

        rollingDice.OnTrowDiceOneMoreTime.AddListener(OneMoreAttempt);
    }

    public void NewPLayerTurn()
    {
        _numberOfMoves++;


        rollingDice.SetUpDicesAndRoll(_amountOfDice, _activePlayer);
    }

    void OneMoreAttempt()
    {
        print("OneMoreTurn");
        _isOneMoreAttempt = true;
    }

    public void PassTheMoveToNextPlayer()
    {
        print("try to pass");
        if (!_isOneMoreAttempt)
        {
            print("pass the move");
            _indexOfActivePlayer = (_indexOfActivePlayer + 1) % _numberOfPlayers;
        }
        _isOneMoreAttempt = false;
        _activePlayer = Players[_indexOfActivePlayer];

        RollADiceButton.Instance.SetColor(_activePlayer.GetComponent<Renderer>().material.color);
    }
}
