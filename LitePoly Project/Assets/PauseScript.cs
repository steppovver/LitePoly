using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // SINGLETON
    private static PauseMenu _instance;

    public static PauseMenu Instance { get { return _instance; } }


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

    public static bool GameIsPaused = false;

    private PauseMenu _PauseMenu;

    private void Start()
    {
        _PauseMenu = GetComponentInChildren<PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("esc");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        _PauseMenu.gameObject.SetActive(false);
        GameIsPaused = false;
    }

    void Pause()
    {
        _PauseMenu.gameObject.SetActive(true);
        GameIsPaused = true;
    }
}
