using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    string searchTag = "House";
    List<GameObject> actors = new List<GameObject>();

    void FindObjectwithTag(string _tag)
    {
        actors.Clear();
        Transform parent = transform;
        GetChildObject(parent, _tag);
    }

    void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                actors.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }

    public void ChangeColor(Color color)
    {
        if (searchTag != null)
        {
            FindObjectwithTag(searchTag);
        }

        foreach (var item in actors)
        {
            item.GetComponent<Renderer>().material.color = color;
        }
    }
}
