using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject prisonCanvas;
    [SerializeField] private TextMeshProUGUI textOfAttempts;
    public void ShowPrisonCanvas(PlayerMovement player)
    {
        prisonCanvas.gameObject.SetActive(true);
        print(player.numberOfTryToEscape);
        textOfAttempts.text = player.numberOfTryToEscape.ToString();

    }
}
