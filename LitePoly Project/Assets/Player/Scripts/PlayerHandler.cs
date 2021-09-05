using System.Linq;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private int _amountOfDice;
    [SerializeField] private int _numberOfPlayers;

    [SerializeField] private List<GameObject> _playersPrefabs;

    private int _numberOfMoves = 0;
    private int _indexOfActivePlayer = 0;
    private bool _isOneMoreAttempt = false;
    private DiceRoller _diceRoller;

    public Player[] players;

    [HideInInspector] public Player _activePlayer;

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

    void PlayersInit()
    {
        players = new Player[_numberOfPlayers];
        for (int i = 0; i < _numberOfPlayers; i++)
        {
            Player player = Instantiate(_playersPrefabs[i], transform).GetComponent<Player>();
            players[i] = player;
        }
    }

    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            Player temp = players[i];
            int randomIndex = Random.Range(i, players.Length);
            players[i] = players[randomIndex];
            players[randomIndex] = temp;
        }
        foreach (var item in players)
        {
            item.GetComponent<PlayerMoney>().InitMoney();
        }

        _activePlayer = players[_indexOfActivePlayer];
        RollADiceButton.Instance.SetColor(_activePlayer.GetComponent<Renderer>().material.color);

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
        if (_numberOfPlayers == players.Length)
        {
            if (!_isOneMoreAttempt || _activePlayer.isInPrison) // тут можно подумать если чувак встал на полицию с парой, может ему сразу дать шанс выйти из тюрьмы или нет хз
            {
                _activePlayer.numberOfDouble = 0;
                print("pass the move");
                _indexOfActivePlayer = (_indexOfActivePlayer + 1) % _numberOfPlayers;
            }
            _isOneMoreAttempt = false;
            _activePlayer = players[_indexOfActivePlayer];
        }
        else
        {
            _numberOfPlayers = players.Length;
            print("pass the move when smo lose");
            _indexOfActivePlayer = _indexOfActivePlayer % _numberOfPlayers;
            _activePlayer = players[_indexOfActivePlayer];
        }

        if (_activePlayer.isInPrison && _activePlayer.playerMovement.numberOfTryToEscape > 0)
        {

            GamePlayCanvas.Instance.PrisonCanvas.ShowPrisonCanvas(_activePlayer.playerMovement);
        }
        else
        {
            _activePlayer.isInPrison = false;
            RollADiceButton.Instance.myButton.gameObject.SetActive(true);
            RollADiceButton.Instance.SetColor(_activePlayer.GetComponent<Renderer>().material.color);
        }

    }

    public void PlayerLose(Player player)
    {
        print("you lose SUCKER");
        _isOneMoreAttempt = false;
        players = players.Where(val => val != player).ToArray();
        Destroy(player.gameObject, 1);
    }
}
