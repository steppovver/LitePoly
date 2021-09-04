using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrisonDiceButton : MonoBehaviour
{
    // SINGLETON
    private static PrisonDiceButton _instance;

    public static PrisonDiceButton Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    [HideInInspector] public Button myButton;
    [SerializeField] private GameObject _prisonCanvas;

    void Start()
    {
        myButton = gameObject.GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }


    public void TaskOnClick()
    {
        _prisonCanvas.gameObject.SetActive(false);
        PlayerHandler.Instance._activePlayer.playerMovement.numberOfTryToEscape--;
        PlayerHandler.Instance.NewPLayerTurn();
    }
}
