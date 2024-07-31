using UnityEngine;
using UnityEngine.UI;

public class CooldownSlotLogic : MonoBehaviour
{
    [SerializeField] Slider slider;

    void Start()
    {
        slider.value = 0;
    }

    public void SetSliderMaxValue(float itemDuration)
    {
        slider.maxValue = itemDuration;
        slider.value = slider.maxValue;
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }
}
