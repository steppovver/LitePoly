using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject _gamePlayCanvas;
    [SerializeField] Image _gameOverCanvas;

    public void SetGameOverScreen(Color winColor)
    {
        _gamePlayCanvas.SetActive(false);

        winColor.a = 0.3f;
        _gameOverCanvas.color = winColor;
        _gameOverCanvas.gameObject.SetActive(true);
    }

}
