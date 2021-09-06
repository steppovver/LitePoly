using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrisonDiceButton : MonoBehaviour
{
    [HideInInspector] public Button myButton;

    void Start()
    {
        myButton = gameObject.GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }


    public void TaskOnClick()
    {
        transform.parent.gameObject.SetActive(false);
        PlayerHandler.Instance._activePlayer.playerMovement.numberOfTryToEscape--;
        PlayerHandler.Instance.NewPLayerTurn();
    }
}
