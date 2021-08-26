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

    DiceRoller _diceRoller;

    [SerializeField] private int _amountOfDice;
    [SerializeField] private int _numberOfPlayers;

    private int _numberOfMoves = 0;
    private int _indexOfActivePlayer = -1;
    private bool _isOneMoreAttempt = false;

    [SerializeField] private List<GameObject> _playersPrefabs;
    public PlayerMovement[] players;

    public PlayerMovement _activePlayer; // debug public

    void PlayersInit()
    {
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            Instantiate(_playersPrefabs[i], transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        players = GetComponentsInChildren<PlayerMovement>();

        for (int i = 0; i < players.Length; i++)
        {
            PlayerMovement temp = players[i];
            int randomIndex = Random.Range(i, players.Length);
            players[i] = players[randomIndex];
            players[randomIndex] = temp;
        }
        foreach (var item in players)
        {
            item.GetComponent<PlayerMoney>().InitMoney();
        }

        PassTheMoveToNextPlayer();

        _diceRoller = DiceRoller.Instance;

        _diceRoller.OnThrowDiceOneMoreTime.AddListener(OneMoreAttempt);
    }

    public void NewPLayerTurn()
    {
        _numberOfMoves++;


        _diceRoller.SetUpDicesAndRoll(_amountOfDice, _activePlayer);
    }

    void OneMoreAttempt()
    {
        print("OneMoreTurn");
        _isOneMoreAttempt = true;
    }

    public void PassTheMoveToNextPlayer()
    {
        if (!_isOneMoreAttempt)
        {
            print("pass the move");
            _indexOfActivePlayer = (_indexOfActivePlayer + 1) % _numberOfPlayers;
        }
        _isOneMoreAttempt = false;
        _activePlayer = players[_indexOfActivePlayer];

        RollADiceButton.Instance.SetColor(_activePlayer.GetComponent<Renderer>().material.color);
    }
}
