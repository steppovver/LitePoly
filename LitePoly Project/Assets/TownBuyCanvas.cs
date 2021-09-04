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

    public void ShowTownCanvas(Player player, int cost, TownStep townStep)
    {
        _tempTownStep = townStep;

        // canvas options
        Color temp = _townBuyCanvas.color = player.GetComponent<Renderer>().material.color;
        temp.a = 0.5f;
        _townBuyCanvas.color = temp;
        _townBuyCanvas.gameObject.SetActive(true);
        textOfCost.text = cost.ToString();
    }

    public void BuyATown()
    {
        _townBuyCanvas.gameObject.SetActive(false);
        _tempTownStep.SetNewOwner();
        _tempTownStep.OnAllScriptsDone.Invoke();

    }

    public void CancePurchase()
    {
        _townBuyCanvas.gameObject.SetActive(false);
        _tempTownStep.OnAllScriptsDone.Invoke();
    }
}
