using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownStep : Step
{
    [SerializeField] GameObject[] housePrefabs;
    [SerializeField] public Player _tempPlayer { get; private set; }

    int[] _costsOfUpgrade = new int[4];

    public int[] CostsOfUpgrade { get { return (int[])_costsOfUpgrade.Clone(); } }

    GameObject _housePrefabInUse;
    Player townOwner;
    Color _defaultColor;

    float nalogCoef;

    public int currentLevel;

    [SerializeField] private int _cost;
    public int Cost { get { return _cost; } }
    public int CostOfTown { get; private set; }


    public override void OnStart()
    {
        base.OnStart();
        _defaultColor = GetComponent<Renderer>().material.color;

        _costsOfUpgrade[0] = (int)Mathf.Round(_cost * 0.25f);
        _costsOfUpgrade[1] = (int)Mathf.Round(_cost * 0.5f);
        _costsOfUpgrade[2] = (int)Mathf.Round(_cost * 0.75f);
        _costsOfUpgrade[3] = _cost;
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
                OnAllScriptsDone.Invoke();

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

            townOwner.playerMoney.AddPlayerMoney(-_cost);
            CostOfTown = _cost;
            GetComponent<Renderer>().material.color = Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f);
        }
        _tempPlayer = null;
    }

    public void UpgradeTown(int level, Color color)
    {
        switch (level)
        {
            case 1:
                currentLevel = 1;
                nalogCoef = 0.5f;
                break;
            case 2:
                currentLevel = 2;
                nalogCoef = 1f;
                break;
            case 3:
                currentLevel = 3;
                nalogCoef = 1.5f;
                break;
            case 4:
                currentLevel = 4;
                nalogCoef = 2f;
                break;
            default:
                break;
        }
        InstantiateHouse(level, Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f));
        townOwner.playerMoney.AddPlayerMoney(-_costsOfUpgrade[level - 1]);
        CostOfTown = _cost + _costsOfUpgrade[level - 1];
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
