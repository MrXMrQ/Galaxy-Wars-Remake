using UnityEngine;
using UnityEngine.UI;

public class CooldownSlots : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
    }

    public void SetSliderMax(float itemDuration)
    {
        slider.maxValue = itemDuration;
        slider.value = slider.maxValue;
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }
}
