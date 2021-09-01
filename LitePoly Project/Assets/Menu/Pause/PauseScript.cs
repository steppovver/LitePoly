using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] private PauseCanvas _pauseCanvas;

    [SerializeField] private GameObject _inGamecanvas;

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
        _pauseCanvas.gameObject.SetActive(false);
        _inGamecanvas.gameObject.SetActive(true);
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
        _inGamecanvas.gameObject.SetActive(false);
        _pauseCanvas.gameObject.SetActive(true);
        GameIsPaused = true;
    }
}
