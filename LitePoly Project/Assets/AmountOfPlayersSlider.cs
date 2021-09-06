using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmountOfPlayersSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] TextMeshProUGUI myValueText;

    // Start is called before the first frame update
    void Start()
    {
        BetweenSceneScript betweenSceneScript = new BetweenSceneScript();

        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        BetweenSceneScript.Instance.amountOfPlayers = (int)slider.value;
    }

    void ValueChangeCheck()
    {
        BetweenSceneScript.Instance.amountOfPlayers = (int)slider.value;
        myValueText.text = slider.value.ToString("0");
    }
}
