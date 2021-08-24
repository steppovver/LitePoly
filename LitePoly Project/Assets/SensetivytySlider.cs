using UnityEngine;
using UnityEngine.UI;

public class SensetivytySlider : MonoBehaviour
{
    Slider slider;
    CameraOrbit cameraOrbit;
    [SerializeField] private Sensetivity sensAxis;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        cameraOrbit = CameraOrbit.Instance;
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }


    void ValueChangeCheck()
    {
        cameraOrbit.ChangeSensetivity(sensAxis, slider.value);
    }
}
