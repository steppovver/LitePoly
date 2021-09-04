using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class TownUpgradeCanvas : MonoBehaviour
{
    [SerializeField] private Image _townUpgradeCanvas;
    [SerializeField] private TextMeshProUGUI[] textsOfCosts;

    [SerializeField] private Button[] buttons;

    int[] costsOfUpgrade = new int[4];

    private TownStep _tempTownStep;
    private Player _tempPlayer;

    public void ShowUpgradeTownCanvas(Player player, TownStep townStep)
    {
        _tempPlayer = player;
        _tempTownStep = townStep;

        costsOfUpgrade[0] = (int)Mathf.Round(_tempTownStep.Cost * 0.25f);
        costsOfUpgrade[1] = (int)Mathf.Round(_tempTownStep.Cost * 0.5f);
        costsOfUpgrade[2] = (int)Mathf.Round(_tempTownStep.Cost * 0.75f);
        costsOfUpgrade[3] = _tempTownStep.Cost;

        // canvas options
        for (int i = 0; i < 4; i++)
        {
            textsOfCosts[i].text = costsOfUpgrade[i].ToString();
            if (player.playerMoney.Money >= costsOfUpgrade[i] && townStep.currentLevel < i + 1)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }

        Color temp = _townUpgradeCanvas.color = player.GetComponent<Renderer>().material.color;
        temp.a = 0.5f;
        _townUpgradeCanvas.color = temp;
        _townUpgradeCanvas.gameObject.SetActive(true);
    }

    public void CanceUpgrade()
    {
        _townUpgradeCanvas.gameObject.SetActive(false);
        _tempTownStep.OnAllScriptsDone.Invoke();
    }

    public void UpgradeATown(int level)
    {
        _townUpgradeCanvas.gameObject.SetActive(false);
        _tempTownStep.UpgradeTown(level);
        _tempPlayer.playerMoney.AddPlayerMoney(-costsOfUpgrade[level-1]);

        _tempTownStep.OnAllScriptsDone.Invoke();
    }
}
