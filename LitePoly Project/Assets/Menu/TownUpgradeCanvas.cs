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

        // canvas options
        if (townStep.currentLevel < 3)
        {
            for (int i = 0; i < 3; i++)
            {
                textsOfCosts[i].text = _costsOfUpgrade[i].ToString();
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
            textsOfCosts[3].text = _costsOfUpgrade[3].ToString();
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

    public void CanceUpgrade()
    {
        _townUpgradeCanvas.gameObject.SetActive(false);
        _tempTownStep.OnAllScriptsDone.Invoke();
    }

    public void UpgradeATown(int level)
    {
        _townUpgradeCanvas.gameObject.SetActive(false);
        _townUpgrateHotelCanvas.gameObject.SetActive(false);

        _tempTownStep.UpgradeTown(level, color);
        _tempPlayer.playerMoney.AddPlayerMoney(-_costsOfUpgrade[level-1]);

        _tempTownStep.OnAllScriptsDone.Invoke();
    }
}
