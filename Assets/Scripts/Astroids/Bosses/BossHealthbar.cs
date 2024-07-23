using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI healthpointsText;
    public GameObject healthbar;

    void Start()
    {
        healthbar.SetActive(false);
    }
    public void SetMaxHealth(int health, string bossName)
    {
        slider.maxValue = health;
        slider.value = health;
        healthpointsText.text = bossName + " " + health.ToString() + " / " + health.ToString();
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
        healthpointsText.text = bossName + " " + health.ToString() + " / " + slider.maxValue.ToString();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    /*void Update()
    {
        SetHealth(BossMovementTrippleShot.currentHealthpoints);

        if (BossMovementTrippleShot.currentHealthpoints <= 0)
        {
            healthbar.SetActive(false);
        }
    }

    public void SetValues(int index)
    {
        boss = bosses[index].GetComponentInChildren<BossMovementTrippleShot>();
        SetMaxHealth(boss.maxHealthpoints);
        healthbar.SetActive(true);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthPoints.text = boss.bossName + " " + health.ToString() + " / " + health.ToString();
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        healthPoints.text = boss.bossName + " " + health.ToString() + " / " + slider.maxValue.ToString();
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }*/
}
