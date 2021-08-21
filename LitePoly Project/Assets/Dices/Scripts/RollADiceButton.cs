using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollADiceButton : MonoBehaviour
{
    // SINGLETON
    private static RollADiceButton _instance;

    public static RollADiceButton Instance { get { return _instance; } }

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

    void Start()
    {
        myButton = gameObject.GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }


    public void TaskOnClick()
    {
        myButton.interactable = false;
        PlayerHandler.Instance.NewPLayerTurn();
    }

    public void SetColor(Color color)
    {
        GetComponent<Image>().color = color;
    }
}
