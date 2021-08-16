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


    [SerializeField] private GameObject dicePrefab;

    List<GameObject> dices = new List<GameObject>();

    public UnityEvent OnDicePair;

    private void Start()
    {
        if (OnDicePair == null)
            OnDicePair = new UnityEvent();
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
        while (dices.Count != numberOfDices)
        {
            if (dices.Count < numberOfDices)
            {
                GameObject newDice = Instantiate(
                                                dicePrefab,
                                                new Vector3(
                                                    transform.position.x + dices.Count,
                                                    transform.position.y,
                                                    transform.position.z),
                                                Quaternion.identity,
                                                transform
                                                );
                dices.Add(newDice);
            }
            else if (dices.Count > numberOfDices)
            {
                Destroy(dices[0]);
                dices.RemoveAt(0);
            }
        }
    }

    void RollTheDice()
    {
        int diceDistance = 0;
        foreach (var item in dices)
        {

            item.transform.position = transform.position + Vector3.forward * diceDistance;
            item.transform.rotation = Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            
            Rigidbody rb = item.GetComponent<Rigidbody>();
            Vector3 direction = new Vector3(0,0,0) - item.transform.position;
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(direction, ForceMode.Impulse);

            //Time.timeScale = 2f;

            diceDistance++;
        }
    }

    IEnumerator getDicesCount(PlayerMovement playerObj)
    {
        yield return new WaitForSeconds(1.0f);

        // wait for dices to stop
        bool isStop = false;
        while (!isStop)
        {
            isStop = IsEveryDiceStopped();
            yield return null;
        }

        playerObj.StartMoving(CalcualteSumOfDices());
    }

    bool IsEveryDiceStopped()
    {
        foreach (var item in dices)
        {
            if (item.GetComponent<Dice>().IsMoving()) return false;
        }
        //Time.timeScale = 1f;
        return true;
    }

    int CalcualteSumOfDices()
    {
        int countDices = 0;
        foreach (var item in dices)
        {
            int currentDiceNumber = item.GetComponent<Dice>().GetDiceCount();
            if (dices.Count == 2 && currentDiceNumber == countDices)
            {
                print("Выпала пара");
                OnDicePair.Invoke();
            }
            countDices += currentDiceNumber;
        }
        print(countDices);

        return countDices;
    }
}
