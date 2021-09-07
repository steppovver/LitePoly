using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TownBuyCanvas : MonoBehaviour
{
    [SerializeField] private Image _townBuyCanvas;
    [SerializeField] private Image _townResellCanvas;
    [SerializeField] private TextMeshProUGUI textOfCost;
    [SerializeField] private TextMeshProUGUI textOfResellCost;

    private TownStep _tempTownStep;
    private Player _tempPlayer;

    public void ShowTownCanvas(Player player, TownStep townStep)
    {
        _tempTownStep = townStep;
        _tempPlayer = player;

        // canvas options
        Color temp = _townBuyCanvas.color = player.GetComponent<Renderer>().material.color;
        temp.a = 0.5f;
        _townBuyCanvas.color = temp;
        _townBuyCanvas.gameObject.SetActive(true);
        textOfCost.text = String.Format("{0:n0}", townStep.Cost);
    }

    public void ShowResellCanvas(Player player, TownStep townStep)
    {
        _tempTownStep = townStep;
        _tempPlayer = player;

        // canvas options
        Color temp = _townResellCanvas.color = player.GetComponent<Renderer>().material.color;
        temp.a = 0.5f;
        _townResellCanvas.color = temp;
        _townResellCanvas.gameObject.SetActive(true);
        textOfResellCost.text = String.Format("{0:n0}", townStep.CostOfTown);
    }

    public void BuyATown()
    {
        _townBuyCanvas.gameObject.SetActive(false);
        _tempTownStep.BuyStep();

        GetComponent<TownUpgradeCanvas>().ShowUpgradeTownCanvas(_tempPlayer, _tempTownStep);

    }

    public void ResellATown()
    {
        _townResellCanvas.gameObject.SetActive(false);
        _tempTownStep.ResellStep();
    }

    public void CancePurchase()
    {
        _townResellCanvas.gameObject.SetActive(false);
        _townBuyCanvas.gameObject.SetActive(false);
        _tempTownStep.OnAllScriptsDone.Invoke();
    }
}
