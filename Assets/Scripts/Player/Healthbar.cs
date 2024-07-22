using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI healthPoints;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthPoints.text = health.ToString() + " / " + health.ToString();
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        healthPoints.text = health.ToString() + " / " + slider.maxValue.ToString();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
