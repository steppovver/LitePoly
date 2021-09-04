using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownStep : Step
{
    [SerializeField] GameObject[] housePrefabs;
    GameObject _housePrefabInUse;

    Player townOwner;
    float nalogCoef;
    Color _defaultColor;

    public int currentLevel;

    [SerializeField] private int _cost;
    public int Cost { get { return _cost; } }

    [SerializeField] public Player _tempPlayer { get; private set; }

    public override void OnStart()
    {
        base.OnStart();
        _defaultColor = GetComponent<Renderer>().material.color;
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
                playerMoney.AddPlayerMoney(-nalog);
                townOwner.playerMoney.AddPlayerMoney(nalog);
                OnAllScriptsDone.Invoke();

            }
            else
            {
                GamePlayCanvas.Instance.TownUpgradeCanvas.ShowUpgradeTownCanvas(player, this);


            }
        }
    }

    public void SetNewOwner()
    {
        if (_tempPlayer != null)
        {
            townOwner = _tempPlayer;
            townOwner.myOwnTownSteps.Add(this);
            currentLevel = 0;
            nalogCoef = 0.1f;

            townOwner.playerMoney.AddPlayerMoney(-_cost);
            GetComponent<Renderer>().material.color = Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f);
        }
        _tempPlayer = null;
    }

    public void UpgradeTown(int level)
    {
        switch (level)
        {
            case 1:
                currentLevel = 1;
                nalogCoef = 0.5f;
                if (_housePrefabInUse is null)
                {
                    Destroy(_housePrefabInUse);
                }
                _housePrefabInUse = Instantiate(housePrefabs[level-1], transform.position + transform.forward, Quaternion.identity);
                _housePrefabInUse.transform.SetParent(transform, true);
                break;
            case 2:
                currentLevel = 2;
                nalogCoef = 1f;
                if (_housePrefabInUse is null)
                {
                    Destroy(_housePrefabInUse);
                }
                _housePrefabInUse = Instantiate(housePrefabs[level - 1], transform.position + transform.forward, Quaternion.identity);
                _housePrefabInUse.transform.SetParent(transform, true);
                break;
            case 3:
                currentLevel = 3;
                nalogCoef = 1.5f;
                if (_housePrefabInUse is null)
                {
                    Destroy(_housePrefabInUse);
                }
                _housePrefabInUse = Instantiate(housePrefabs[level - 1], transform.position + transform.forward, Quaternion.identity);
                _housePrefabInUse.transform.SetParent(transform, true);
                break;
            case 4:
                currentLevel = 4;
                nalogCoef = 2f;
                if (_housePrefabInUse is null)
                {
                    Destroy(_housePrefabInUse);
                }
                _housePrefabInUse = Instantiate(housePrefabs[level - 1], transform.position + transform.forward, Quaternion.identity);
                _housePrefabInUse.transform.SetParent(transform, true);
                break;
            default:
                break;
        }
    }

    private int countNalog()
    {
        int nalog = (int)Mathf.Round(_cost * nalogCoef);
        print(nalog);
        return nalog;
    }
}
