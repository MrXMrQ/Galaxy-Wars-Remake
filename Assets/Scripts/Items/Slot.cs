using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Slider slider;
    public Image fill, item;
    public Sprite sprite;

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

    public void SetSprite(Sprite sprite)
    {
        item.color = Color.white;
        item.sprite = sprite;
    }

    public void ResetSprite()
    {
        item.sprite = sprite;
    }
}