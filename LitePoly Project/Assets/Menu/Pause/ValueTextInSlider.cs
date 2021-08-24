using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ValueTextInSlider : MonoBehaviour
{
    void Awake()
    {
        GetComponentInParent<SensetivytySlider>().myValueText = this.GetComponent<TMP_Text>();
    }
}
