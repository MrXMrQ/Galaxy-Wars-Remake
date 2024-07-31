using UnityEngine;
using UnityEngine.UI;

public class ItemSlotLogic : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] public Image item;
    Sprite _old_sprite;

    [SerializeField]
    public Sprite old_sprite
    {
        get
        {
            return _old_sprite;
        }
    }

    void Start()
    {
        _old_sprite = item.sprite;
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