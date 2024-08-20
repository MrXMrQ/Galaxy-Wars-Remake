using UnityEngine;
using UnityEngine.UI;

public class UpgradeManagerNew : MonoBehaviour
{
    [SerializeField] UpgradeCard[] upgrades;
    [SerializeField] Slider[] sliders_current;
    [SerializeField] Slider[] sliders_next;

    void Awake()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            SetUpSlider(upgrades[i], sliders_current[i], sliders_next[i]);
        }
    }

    private void SetUpSlider(UpgradeCard upgrade, Slider currentSlider, Slider nextSlider)
    {
        float baseMaxValue = upgrade.MAX_UPGRADE_STAT - upgrade.UPGRADE_VALUE;

        upgrade.slider_current = currentSlider;
        upgrade.slider_next = nextSlider;

        SetSliderMaxValues(upgrade, baseMaxValue);
    }

    private void SetSliderMaxValues(UpgradeCard upgrade, float baseMaxValue)
    {
        float specificMaxValue = baseMaxValue;

        switch (upgrade.keys)
        {
            case UpgradeCard.KEYS.dash_cooldown:
                specificMaxValue = SaveSystem.DASH_COOLDOWN;
                break;
            case UpgradeCard.KEYS.shot_cooldown:
                specificMaxValue = SaveSystem.SHOT_COOLDOWN;
                break;
        }

        upgrade.slider_current.maxValue = specificMaxValue;
        upgrade.slider_next.maxValue = specificMaxValue;
    }
}
