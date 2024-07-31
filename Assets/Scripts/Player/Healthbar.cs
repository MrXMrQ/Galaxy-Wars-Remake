using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] public Image fill;
    [SerializeField] public Image border;
    [SerializeField] bool state;
    public Sprite sprite;

    void Start()
    {
        SetActive(state);
        border.sprite = sprite;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        if (health <= 0)
        {
            SetActive(false);
        }
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
}