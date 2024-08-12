using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayUpgrades : MonoBehaviour
{
    [SerializeField] private Upgrades item;
    [SerializeField] private TextMeshProUGUI card_name;
    [SerializeField] private TextMeshProUGUI cost_text;
    [SerializeField] private Slider slider_current_upgrade;
    [SerializeField] private Slider slider_next_upgrade;

    private void Start()
    {
        LoadUpgradeData();
        UpdateVisuals();
    }

    public void Upgrade(GameData game_data)
    {
        if (item is IUpgradeable upgradeableItem)
        {
            upgradeableItem.Upgrade(game_data);
            UpdateVisuals();
        }
        else
        {
            Debug.LogWarning("This item cannot be upgraded.");
        }
    }

    private void LoadUpgradeData()
    {
        if (item is IUpgradeable upgradeableItem)
        {
            upgradeableItem.Load(SwipeControllerCards.game_data);
        }
    }

    private void UpdateVisuals()
    {
        card_name.text = item.card_name;
        cost_text.text = item.current_stat == item.max_upgrade_stat ? "MAX" : $"Cost: {item.cost}";

        UpdateSliderValues(slider_next_upgrade, isCurrent: false);
        UpdateSliderValues(slider_current_upgrade, isCurrent: true);
    }

    private void UpdateSliderValues(Slider slider, bool isCurrent)
    {
        slider.maxValue = item.max_upgrade_stat - item.upgrade_value;
        slider.value = isCurrent ? item.current_stat - item.upgrade_value : item.current_stat;

        switch (item.upgrade_type)
        {
            case Upgrades.UPGRADES_TYPES.Shot:
                slider.maxValue = SaveSystem.SHOT_COOLDOWN;
                slider.value = SaveSystem.SHOT_COOLDOWN - item.current_stat + (isCurrent ? 0 : item.upgrade_value);
                break;

            case Upgrades.UPGRADES_TYPES.Dash:
                slider.maxValue = SaveSystem.DASH_COOLDOWN;
                slider.value = SaveSystem.DASH_COOLDOWN - item.current_stat + (isCurrent ? 0 : item.upgrade_value);
                break;

            case Upgrades.UPGRADES_TYPES.Multiplier:
                slider.minValue = 1;
                slider.maxValue = item.max_upgrade_stat;
                slider.value = isCurrent ? item.current_stat : item.current_stat * item.upgrade_value;
                break;
        }
    }
}