using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChanceCard : MonoBehaviour
{
    public UnityEvent OnAllScriptsDone;

    public void DoSmth()
    {
        print("done");
        OnAllScriptsDone.Invoke();
    }
}
