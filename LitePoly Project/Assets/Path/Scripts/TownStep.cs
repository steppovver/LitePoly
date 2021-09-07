using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TownStep : Step
{
    [SerializeField] GameObject[] housePrefabs;
    [SerializeField] public Player _tempPlayer { get; private set; }
    [SerializeField] private int _cost;

    GameObject _housePrefabInUse;
    Player townOwner;
    Color _defaultColor;
    float nalogCoef;
    TextMeshProUGUI costText;

    public int currentLevel;

    public int Cost { get { return _cost; } }
    public int CostOfTown { get; private set; }

    int[] _costsOfUpgrade = new int[5];
    public int[] CostsOfUpgrade { get { return (int[])_costsOfUpgrade.Clone(); } }


    public override void OnStart()
    {
        costText = GetComponentInChildren<TextMeshProUGUI>();
        base.OnStart();
        _defaultColor = GetComponent<Renderer>().material.color;

        _costsOfUpgrade[0] = _cost;
        _costsOfUpgrade[1] = (int)Mathf.Round(_cost * 0.25f);
        _costsOfUpgrade[2] = (int)Mathf.Round(_cost * 0.5f);
        _costsOfUpgrade[3] = (int)Mathf.Round(_cost * 0.75f);
        _costsOfUpgrade[4] = (int)Mathf.Round(_cost * 1.75f);

        costText.text = String.Format("{0:n0}", _cost);
    }

    public override void DoOnPlayerStop(Player player)
    {
        _tempPlayer = player;
        PlayerMoney playerMoney = _tempPlayer.playerMoney;

        if (townOwner is null)
        {
            if (playerMoney.Money > _cost)
            {
                GamePlayCanvas.Instance.TownBuyCanvas.ShowTownCanvas(player, this);
            }
            else
            {
                OnAllScriptsDone.Invoke();
            }
        }
        else
        {
            
            if (_tempPlayer != townOwner)
            {
                int nalog = countNalog();
                int gavedMoney = playerMoney.AddPlayerMoney(-nalog);
                townOwner.playerMoney.AddPlayerMoney(gavedMoney);

                if (_tempPlayer.playerMoney.Money < CostOfTown)
                {
                    OnAllScriptsDone.Invoke();
                }
                else
                {
                    GamePlayCanvas.Instance.TownBuyCanvas.ShowResellCanvas(player, this);
                }
            }
            else
            {
                GamePlayCanvas.Instance.TownUpgradeCanvas.ShowUpgradeTownCanvas(player, this);
            }
        }
    }

    public void SellStep()
    {
        if (townOwner != null)
        {
            currentLevel = 0;
            nalogCoef = 0.1f;

            townOwner.myOwnTownSteps.Remove(this);
            GetComponent<Renderer>().material.color = _defaultColor;
            if (_housePrefabInUse != null)
            {
                Destroy(_housePrefabInUse);
            }

            townOwner.playerMoney.AddPlayerMoney(CostOfTown);
            townOwner = null;
        }
    }

    public void BuyStep()
    {
        if (_tempPlayer != null)
        {
            townOwner = _tempPlayer;
            townOwner.myOwnTownSteps.Add(this);
            currentLevel = 0;
            nalogCoef = 0.1f;
            costText.text = String.Format("{0:n0}", _cost * nalogCoef);

            townOwner.playerMoney.AddPlayerMoney(-_cost);
            CostOfTown = _cost;
            GetComponent<Renderer>().material.color = Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f);
        }
        _tempPlayer = null;
    }

    public void ResellStep()
    {
        townOwner.myOwnTownSteps.Remove(this);
        townOwner.playerMoney.AddPlayerMoney(CostOfTown);
        townOwner = null;


        if (_tempPlayer != null)
        {
            _tempPlayer.playerMoney.AddPlayerMoney(-CostOfTown);

            townOwner = _tempPlayer;
            townOwner.myOwnTownSteps.Add(this);
            costText.text = String.Format("{0:n0}", _cost * nalogCoef);

            GetComponent<Renderer>().material.color = Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f);
        }

        _housePrefabInUse.GetComponent<HouseScript>().ChangeColor(Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f));
        GamePlayCanvas.Instance.TownUpgradeCanvas.ShowUpgradeTownCanvas(townOwner, this);
    }

    public void UpgradeTown(int level, Color color, int cost)
    {
        currentLevel = level;

        switch (level)
        {
            case 1:
                nalogCoef = 0.5f;
                break;
            case 2:
                nalogCoef = 1f;
                break;
            case 3:
                nalogCoef = 1.5f;
                break;
            case 4:
                nalogCoef = 2f;
                break;
            default:
                break;
        }
        InstantiateHouse(level, Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f));
        townOwner.playerMoney.AddPlayerMoney(-cost);
        CostOfTown = 0;
        for (int i = 0; i <= level; i++)
        {
            CostOfTown += _costsOfUpgrade[i];
        }
        costText.text = String.Format("{0:n0}", _cost * nalogCoef);

    }

    private int countNalog()
    {
        int nalog = (int)Mathf.Round(_cost * nalogCoef);
        print(nalog);
        return nalog;
    }

    private void InstantiateHouse(int level, Color color)
    {
        if (_housePrefabInUse != null)
        {
            Destroy(_housePrefabInUse);
        }
        Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
        _housePrefabInUse = Instantiate(housePrefabs[level - 1], transform.position + transform.forward, rotation);
        _housePrefabInUse.transform.SetParent(transform, true);
        _housePrefabInUse.GetComponent<HouseScript>().ChangeColor(color);
    }
}
