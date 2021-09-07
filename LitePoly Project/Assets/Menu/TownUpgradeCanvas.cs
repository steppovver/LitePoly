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

    int[] _costsOfUpgrade;

    Color color;

    TownStep _tempTownStep;
    Player _tempPlayer;

    public void ShowUpgradeTownCanvas(Player player, TownStep townStep)
    {
        _costsOfUpgrade = new int[5];
        _tempPlayer = player;
        _tempTownStep = townStep;
        color = player.GetComponent<Renderer>().material.color;
        color.a = 0.5f;

        for (int i = townStep.currentLevel + 1; i < _costsOfUpgrade.Length; i++)
        {
            _costsOfUpgrade[i] = _tempTownStep.CostsOfUpgrade[i] + _costsOfUpgrade[i - 1];
        }
        if (player.playerMoney.Money < _costsOfUpgrade[Mathf.Clamp(townStep.currentLevel + 1, 0, 4)]) // if not enough money to buy smth then skip upgrade
        {
            _tempTownStep.OnAllScriptsDone.Invoke();
            return;
        }

        // canvas options
        if (townStep.currentLevel < 3)
        {
            for (int i = 1; i < 4; i++)
            {
                textsOfCosts[i-1].text = String.Format("{0:n0}", _costsOfUpgrade[i]);
                if (player.playerMoney.Money >= _costsOfUpgrade[i] && townStep.currentLevel < i)
                {
                    buttons[i-1].interactable = true;
                }
                else
                {
                    buttons[i-1].interactable = false;
                }

                if (i == 3 && player.numberOfLap < 1)
                {
                    buttons[i-1].interactable = false;
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
                textsOfCosts[3].text = String.Format("{0:n0}", _costsOfUpgrade[4]);
                if (player.playerMoney.Money >= _costsOfUpgrade[4])
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

        _tempTownStep.UpgradeTown(level, color, _costsOfUpgrade[level]);

        _tempTownStep.OnAllScriptsDone.Invoke();
    }
}
