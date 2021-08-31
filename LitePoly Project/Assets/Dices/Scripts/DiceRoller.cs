using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DiceRoller : MonoBehaviour
{
    // SINGLETON
    private static DiceRoller _instance;

    public static DiceRoller Instance { get { return _instance; } }

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


    [SerializeField] private GameObject _dicePrefab;

    List<GameObject> _dices = new List<GameObject>();

    public UnityEvent OnThrowDiceOneMoreTime;

    private void Start()
    {
        if (OnThrowDiceOneMoreTime == null)
            OnThrowDiceOneMoreTime = new UnityEvent();
    }

    public void SetUpDicesAndRoll(int numberOfDices, PlayerMovement playerObj)
    {
        StopAllCoroutines();

        DiceInit(numberOfDices);

        RollTheDice();

        StartCoroutine(getDicesCount(playerObj));
    }



    void DiceInit(int numberOfDices)
    {
        while (_dices.Count != numberOfDices)
        {
            if (_dices.Count < numberOfDices)
            {
                GameObject newDice = Instantiate
                    (
                                                _dicePrefab,
                                                new Vector3(
                                                    transform.position.x + _dices.Count,
                                                    transform.position.y,
                                                    transform.position.z),
                                                Quaternion.identity,
                                                transform
                                                );
                _dices.Add(newDice);
            }
            else if (_dices.Count > numberOfDices)
            {
                Destroy(_dices[0]);
                _dices.RemoveAt(0);
            }
        }
    }

    void RollTheDice()
    {
        int diceDistance = 0;
        foreach (var item in _dices)
        {

            item.transform.position = transform.position + Vector3.forward * diceDistance;
            //item.transform.rotation = Quaternion.Euler(Random.Range(-2, 3) * 90, Random.Range(-2, 3) * 90, Random.Range(-2, 3) * 90);
            item.transform.rotation = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            
            Rigidbody rb = item.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(0,0,0) + Vector3.forward * diceDistance - item.transform.position;

            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(direction * 2, ForceMode.Impulse);
            rb.AddTorque(new Vector3(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized * 10, ForceMode.Impulse);

            Time.timeScale = 2f;

            diceDistance++;
        }
    }

    IEnumerator getDicesCount(PlayerMovement playerObj)
    {
        yield return new WaitForSeconds(1.0f);

        // wait for dices to stop
        bool isStop = false;
        int stopFrames = 0;
        while (!isStop && stopFrames < 15)
        {
            isStop = IsEveryDiceStopped();
            if (isStop)
            {
                stopFrames++;
            }
            else
            {
                stopFrames = 0;
            }
            yield return null;
        }

        bool IsDouble = false;
        int sumOfDice = CalcualteSumOfDices(playerObj, out IsDouble);

        if (sumOfDice == -1)
        {
            SetUpDicesAndRoll(_dices.Count, playerObj);
        }
        else if (!playerObj.isInPrison)
        {
            if (playerObj.numberOfDouble < 3)
            {
                if (IsDouble)
                {
                    print("Double dice");
                    OnThrowDiceOneMoreTime.Invoke();
                }
                playerObj.StartMoving(sumOfDice);
            }
            else
            {
                print("Go to prison cheater!!!");
                StartCoroutine(playerObj.MoveToPrison());
            }
        }
        else // if player in prison
        {
            if (IsDouble) // если выпала пара
            {
                print("Double dice");
                OnThrowDiceOneMoreTime.Invoke();
                playerObj.StartMoving(sumOfDice);
                playerObj.isInPrison = false;
            }
            else
            {
                PlayerHandler.Instance.PassTheMoveToNextPlayer();
            }
        }
    }

    bool IsEveryDiceStopped()
    {
        foreach (var item in _dices)
        {
            if (item.GetComponent<Dice>().IsMoving()) return false;
        }
        Time.timeScale = 1f;
        return true;
    }

    int CalcualteSumOfDices(PlayerMovement playerObj, out bool doubl)
    {
        doubl = false;
        int countDices = 0;
        foreach (var item in _dices)
        {
            int currentDiceNumber = item.GetComponent<Dice>().GetDiceCount();
            if (currentDiceNumber == -1)
            {
                return -1;
            }
            if (_dices.Count == 2 && currentDiceNumber == countDices)
            {
                doubl = true;
                playerObj.numberOfDouble++;
            }
            countDices += currentDiceNumber;
        }
        print(countDices);

        return countDices;
    }
}
