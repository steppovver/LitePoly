using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    /// <summary>
    /// SINGLETON Start
    /// </summary>
    private static PauseScript _instance;

    public static PauseScript Instance { get { return _instance; } }


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
    /// <summary>
    /// SINGLETON END
    /// </summary>
    /// 

    public static bool GameIsPaused = false;

    private GameObject _PauseMenu;

    private PauseButton _pauseButton;
    
    private void Start()
    {
        _PauseMenu = transform.GetChild(0).gameObject;
        _pauseButton = GetComponentInChildren<PauseButton>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                SetPause();
            }
        }
    }

    public void Resume()
    {
        _PauseMenu.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        GameIsPaused = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPause()
    {
        _PauseMenu.gameObject.SetActive(true);
        GameIsPaused = true;
    }
}
