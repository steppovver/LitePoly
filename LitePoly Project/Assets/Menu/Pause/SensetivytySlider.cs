using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SensetivytySlider : MonoBehaviour
{
    Slider slider;
    string sliderName;
    CameraOrbit cameraOrbit;
    [SerializeField] private Sensetivity sensAxis;

    [HideInInspector] public TMP_Text myValueText;

    void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneQuit;

    }

    void Start()
    {
        slider = GetComponent<Slider>();
        cameraOrbit = CameraOrbit.Instance;
        sliderName = slider.gameObject.name;
        if (PlayerPrefs.HasKey(sliderName))
        {
            slider.value = PlayerPrefs.GetFloat(sliderName);
        }
        ValueChangeCheck();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }


    void ValueChangeCheck()
    {
        cameraOrbit.ChangeSensetivity(sensAxis, slider.value);
        myValueText.text = slider.value.ToString("0.00");
    }

    void OnSceneQuit<Scene>(Scene scene)
    {
        PlayerPrefs.SetFloat(sliderName, slider.value);
        PlayerPrefs.Save();


        print("The scene was unloaded!");
    }
}
