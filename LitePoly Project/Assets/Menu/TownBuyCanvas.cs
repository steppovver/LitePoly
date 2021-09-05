using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TownBuyCanvas : MonoBehaviour
{
    [SerializeField] private Image _townBuyCanvas;
    [SerializeField] private TextMeshProUGUI textOfCost;

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
        textOfCost.text = townStep.Cost.ToString();
    }

    public void BuyATown()
    {
        _townBuyCanvas.gameObject.SetActive(false);
        _tempTownStep.BuyStep();

        GetComponent<TownUpgradeCanvas>().ShowUpgradeTownCanvas(_tempPlayer, _tempTownStep);

    }

    public void CancePurchase()
    {
        _townBuyCanvas.gameObject.SetActive(false);
        _tempTownStep.OnAllScriptsDone.Invoke();
    }
}
