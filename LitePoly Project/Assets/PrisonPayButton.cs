using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrisonPayButton : MonoBehaviour
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

        PlayerHandler.Instance._activePlayer.playerMoney.AddPlayerMoney(-200000);
        PlayerHandler.Instance._activePlayer.isInPrison = false;
        PlayerHandler.Instance.NewPLayerTurn();
    }
}