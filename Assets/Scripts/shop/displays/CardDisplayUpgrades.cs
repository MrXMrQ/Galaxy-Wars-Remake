using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayUpgrades : MonoBehaviour
{
    [SerializeField] Upgrades item;
    [SerializeField] TextMeshProUGUI card_name;
    [SerializeField] TextMeshProUGUI cost_text;
    [SerializeField] Slider slider_current_upgrade;
    [SerializeField] Slider slider_next_upgrade;

    void Start()
    {
        ////card.LoadDataOnCard(SwipeControllerCards.game_data);
        UpdateVisuals();
    }

    public void Upgrade(GameData game_data)
    {
        switch (item.upgrade_type)
        {
            case Upgrades.UPGRADES_TYPES.Healthpoints:
                (item as HealthpointsUpgrade)?.Upgrade(game_data);
                break;
            case Upgrades.UPGRADES_TYPES.Healing:
                (item as HealingUpgrade)?.Upgrade(game_data);
                break;
            case Upgrades.UPGRADES_TYPES.Dash:
                (item as DashUpgrade)?.Upgrade(game_data);
                break;
            case Upgrades.UPGRADES_TYPES.Shot:
                (item as ShotUpgrade)?.Upgrade(game_data);
                break;
            case Upgrades.UPGRADES_TYPES.Multiplier:
                (item as MultiplierUpgrade)?.Upgrade(game_data);
                break;
            case Upgrades.UPGRADES_TYPES.Damage:
                (item as DamageUpgrade)?.Upgrade(game_data);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type or unsupported item.");
                break;
        }

        ////dashItem.Upgrade(game_data);
        ////item.DashItem.Upgrade(game_data);
        ////(DashItem)item.Upgrade(game_data);
        UpdateVisuals();
    }

    //* update card and sliders from one card
    private void UpdateVisuals()
    {
        card_name.text = item.card_name;
        cost_text.text = item.current_stat == item.max_upgrade_stat ? "MAX" : "Cost: " + item.cost.ToString();

        //* next slider values
        slider_next_upgrade.maxValue = item.max_upgrade_stat - item.upgrade_value;
        slider_next_upgrade.value = item.current_stat;

        if (item.card_name.Equals("SHOT"))
        {
            slider_next_upgrade.maxValue = SaveSystem.SHOT_COOLDOWN;
            slider_next_upgrade.value = SaveSystem.SHOT_COOLDOWN - item.current_stat + item.upgrade_value;
        }
        else if (item.card_name.Equals("DASH"))
        {
            slider_next_upgrade.maxValue = SaveSystem.DASH_COOLDOWN;
            slider_next_upgrade.value = SaveSystem.DASH_COOLDOWN - item.current_stat + item.upgrade_value;
        }
        else if (item.card_name.Equals("MULTIPLIER"))
        {
            slider_next_upgrade.minValue = 1;
            slider_next_upgrade.maxValue = item.max_upgrade_stat;
            slider_next_upgrade.value = item.current_stat * item.upgrade_value;
        }

        //* current slider values
        slider_current_upgrade.maxValue = item.max_upgrade_stat - item.upgrade_value;
        slider_current_upgrade.value = item.current_stat - item.upgrade_value;

        if (item.card_name.Equals("SHOT"))
        {
            slider_current_upgrade.maxValue = SaveSystem.SHOT_COOLDOWN;
            slider_current_upgrade.value = SaveSystem.SHOT_COOLDOWN - item.current_stat;
        }
        else if (item.card_name.Equals("DASH"))
        {
            slider_current_upgrade.maxValue = SaveSystem.DASH_COOLDOWN;
            slider_current_upgrade.value = SaveSystem.DASH_COOLDOWN - item.current_stat;
        }
        else if (item.card_name.Equals("MULTIPLIER"))
        {
            slider_current_upgrade.minValue = 1;
            slider_current_upgrade.maxValue = item.max_upgrade_stat;
            slider_current_upgrade.value = item.current_stat;
        }
    }
}