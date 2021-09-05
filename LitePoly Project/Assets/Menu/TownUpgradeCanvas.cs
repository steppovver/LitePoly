using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TownUpgradeCanvas : MonoBehaviour
{
    [SerializeField] private Image _townUpgradeCanvas;
    [SerializeField] private Image _townUpgrateHotelCanvas;
    [SerializeField] private TextMeshProUGUI[] textsOfCosts;

    [SerializeField] private Button[] buttons;

    int[] _costsOfUpgrade = new int[4];

    Color color;

    TownStep _tempTownStep;
    Player _tempPlayer;

    public void ShowUpgradeTownCanvas(Player player, TownStep townStep)
    {
        _tempPlayer = player;
        _tempTownStep = townStep;
        color = player.GetComponent<Renderer>().material.color;
        color.a = 0.5f;

        for (int i = 0; i < 4; i++)
        {
            _costsOfUpgrade[i] = _tempTownStep.CostsOfUpgrade[i];
        }

        if (player.playerMoney.Money < Mathf.Clamp(_costsOfUpgrade[townStep.currentLevel], 0, 3)) // if not enough money to buy smth then skip upgrade
        {
            _tempTownStep.OnAllScriptsDone.Invoke();
            return;
        }

        // canvas options
        if (townStep.currentLevel < 3)
        {
            for (int i = 0; i < 3; i++)
            {
                textsOfCosts[i].text = String.Format("{0:n0}", _costsOfUpgrade[i]);
                if (player.playerMoney.Money >= _costsOfUpgrade[i] && townStep.currentLevel < i + 1)
                {
                    buttons[i].interactable = true;
                }
                else
                {
                    buttons[i].interactable = false;
                }

                if (i == 2 && player.numberOfLap < 1)
                {
                    buttons[i].interactable = false;
                }
            }

            _townUpgradeCanvas.color = color;
            _townUpgradeCanvas.gameObject.SetActive(true);
        }
        else
        {
            if (townStep.currentLevel == 4)
            {
                _tempTownStep.OnAllScriptsDone.Invoke();
            }
            else
            {
                textsOfCosts[3].text = String.Format("{0:n0}", _costsOfUpgrade[3]);
                if (player.playerMoney.Money >= _costsOfUpgrade[3])
                {
                    _townUpgrateHotelCanvas.color = color;
                    _townUpgrateHotelCanvas.gameObject.SetActive(true);
                }
                else
                {
                    _tempTownStep.OnAllScriptsDone.Invoke();
                }
            }
        }
        
    }

    public void CanceUpgrade()
    {
        _townUpgradeCanvas.gameObject.SetActive(false);
        _townUpgrateHotelCanvas.gameObject.SetActive(false);
        _tempTownStep.OnAllScriptsDone.Invoke();
    }

    public void UpgradeATown(int level)
    {
        _townUpgradeCanvas.gameObject.SetActive(false);
        _townUpgrateHotelCanvas.gameObject.SetActive(false);

        _tempTownStep.UpgradeTown(level, color);

        _tempTownStep.OnAllScriptsDone.Invoke();
    }
}
