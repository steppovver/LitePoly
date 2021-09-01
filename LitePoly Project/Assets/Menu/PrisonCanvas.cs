using UnityEngine;
using TMPro;


public class PrisonCanvas : MonoBehaviour
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
