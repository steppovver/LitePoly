using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerMoney playerMoney;

    public int numberOfDouble = 0;
    public bool isInPrison = false;

    public List<TownStep> myOwnTownSteps = new List<TownStep>();

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMoney = GetComponent<PlayerMoney>();
    }
}
