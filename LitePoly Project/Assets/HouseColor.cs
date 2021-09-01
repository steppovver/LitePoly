using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseColor : MonoBehaviour
{
    public Material Material;

    private void Start()
    {
        Material.color = Color.green;
    }
}
