using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    public int GetRandomValue(int numberOfDices)
    {
        int _randomValue = 0;
        for (int i = 0; i < numberOfDices; i++)
        {
            _randomValue += Random.Range(1, 7);
        }
        return _randomValue;
    }
}
