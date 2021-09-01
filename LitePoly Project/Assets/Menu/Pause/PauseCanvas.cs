using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _optionsMenu;

    private void OnEnable()
    {
        _pauseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }
}
