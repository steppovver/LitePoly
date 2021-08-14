using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;

    List<GameObject> dices = new List<GameObject>();

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
                dices.Add(Instantiate(
                    dicePrefab,
                    new Vector3(
                        transform.position.x + dices.Count,
                        transform.position.y,
                        transform.position.z),
                    Quaternion.identity,
                    transform));
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

            diceDistance++;
        }
    }

    IEnumerator getDicesCount(PlayerMovement playerObj)
    {
        int countDices = 0;
        yield return new WaitForSeconds(1.0f);

        // wait for dices to stop
        bool isStop = false;
        while (!isStop)
        {
            isStop = IsEveryDiceStopped();
            yield return null;
        }
        print("stop");


        foreach (var item in dices)
        {
            countDices += item.GetComponent<Dice>().GetDiceCount();
        }
        print(countDices);

        playerObj.StartMoving(countDices);
    }

    bool IsEveryDiceStopped()
    {
        foreach (var item in dices)
        {
            if (item.GetComponent<Dice>().IsMoving()) return false;
        }
        return true;
    }
}
