using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPath : MonoBehaviour
{
    // SINGLETON
    private static InitPath _instance;

    public static InitPath Instance { get { return _instance; } }


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

    public Step[] PathStep;

    // Start is called before the first frame update
    void Start()
    {
        PathStep = GetComponentsInChildren<Step>();
    }
}
