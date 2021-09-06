using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PrisonCanvas : MonoBehaviour
{
    [SerializeField] private GameObject prisonCanvas;
    [SerializeField] private TextMeshProUGUI textOfAttempts;
    [SerializeField] private Button payButton;
    public void ShowPrisonCanvas(PlayerMovement player)
    {
        prisonCanvas.gameObject.SetActive(true);
        if (player.player.playerMoney.Money < 200000)
        {
            payButton.gameObject.SetActive(true);
        }
        else
        {
            payButton.gameObject.SetActive(false);

        }

        print(player.numberOfTryToEscape);
        textOfAttempts.text = player.numberOfTryToEscape.ToString();
    }
}
