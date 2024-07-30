using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image border;
    public GameObject healthbar;

    void Start()
    {
        healthbar.SetActive(false);
    }
    public void SetMaxHealth(int health, string bossName)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health, string bossName)
    {
        if (health <= 0)
        {
            healthbar.SetActive(false);
            return;
        }
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}