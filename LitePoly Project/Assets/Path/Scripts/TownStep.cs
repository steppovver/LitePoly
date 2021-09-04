using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownStep : Step
{
    Player townOwner;
    [SerializeField] int cost;
    [SerializeField] int nalog;

    Color _defaultColor;

    public Player _tempPlayer { get; private set; }

    public override void OnStart()
    {
        base.OnStart();
        _defaultColor = GetComponent<Renderer>().material.color;
    }

    public override void DoOnPlayerStop(Player player)
    {
        _tempPlayer = player;
        PlayerMoney playerMoney = _tempPlayer.GetComponent<PlayerMoney>();

        if (townOwner is null)
        {
            if (playerMoney.Money > cost)
            {
                GamePlayCanvas.Instance.TownBuyCanvas.ShowTownCanvas(player, cost, this);
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
                playerMoney.AddPlayerMoney(-nalog);
                townOwner.GetComponent<PlayerMoney>().AddPlayerMoney(nalog);
                OnAllScriptsDone.Invoke();

            }
            else
            {
                OnAllScriptsDone.Invoke();
            }
        }
    }

    public void SetNewOwner()
    {
        if (_tempPlayer != null)
        {
            townOwner = _tempPlayer;
            townOwner.GetComponent<PlayerMoney>().AddPlayerMoney(-cost);
            GetComponent<Renderer>().material.color = Color.Lerp(_defaultColor, townOwner.GetComponent<Renderer>().material.color, 0.5f);
        }
        _tempPlayer = null;
    }
}
