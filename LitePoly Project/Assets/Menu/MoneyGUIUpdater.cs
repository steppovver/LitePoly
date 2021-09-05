using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class ImageStorage
{
    public Image image;
    public TextMeshProUGUI textMesh;

    public ImageStorage(Image image, TextMeshProUGUI textMesh)
    {
        this.image = image;
        this.textMesh = textMesh;
    }
}

public class MoneyGUIUpdater : MonoBehaviour
{
    /// <summary>
    /// SINGLETON Start
    /// </summary>
    private static MoneyGUIUpdater _instance;

    public static MoneyGUIUpdater Instance { get { return _instance; } }


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

    [SerializeField] Image[] images;
    List<ImageStorage> imageStoragesInUse = new List<ImageStorage>();
    Dictionary<PlayerMoney, ImageStorage> dict = new Dictionary<PlayerMoney, ImageStorage>();

    int index = 0;

    public void InitMoneyOfPlayer(PlayerMoney playerMoney)
    {
        Image image = images[index];
        TextMeshProUGUI textMeshProUGUI = image.GetComponentInChildren<TextMeshProUGUI>();
        ImageStorage newimageStorage = new ImageStorage(image, textMeshProUGUI);
        dict.Add(playerMoney, newimageStorage);

        Color temp = playerMoney.gameObject.GetComponent<Renderer>().material.color;
        temp.a = 0.5f;
        image.color = temp;

        image.gameObject.SetActive(true);
        textMeshProUGUI.text = String.Format("{0:n0}", playerMoney.Money);

        imageStoragesInUse.Add(newimageStorage);
        index++;        
    }

    public void MoneyGUIUpdate(PlayerMoney playerMoney)
    {
        dict[playerMoney].textMesh.text = String.Format("{0:n0}", playerMoney.Money);
    }

    private void OnEnable()
    {
        foreach (var item in dict.Keys)
        {
            dict[item].textMesh.text = String.Format("{0:n0}", item.Money);
        }
    }
}
