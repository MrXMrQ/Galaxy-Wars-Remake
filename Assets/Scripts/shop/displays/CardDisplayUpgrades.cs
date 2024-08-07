using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplayUpgrades : MonoBehaviour
{
    [SerializeField] CardUpgrades card;
    [SerializeField] TextMeshProUGUI card_name;
    [SerializeField] TextMeshProUGUI cost_text;
    [SerializeField] Slider slider_current_upgrade;
    [SerializeField] Slider slider_next_upgrade;

    void Start()
    {
        card.LoadDataOnCard(SwipeController.game_data);
        UpdateVisuals();
    }

    public void Upgrade(GameData game_data)
    {
        card.ApplyUpgrade(game_data);
        UpdateVisuals();
    }

    //* update card and sliders from one card
    private void UpdateVisuals()
    {
        card_name.text = card.card_name;
        cost_text.text = card.current_stat == card.max_upgrad_value ? "MAX" : "Cost: " + card.cost.ToString();

        //* next slider values
        slider_next_upgrade.maxValue = card.max_upgrad_value - card.upgrade_value;
        slider_next_upgrade.value = card.current_stat;

        if (card.card_name.Equals("SHOT"))
        {
            slider_next_upgrade.maxValue = SaveSystem.SHOT_COOLDOWN;
            slider_next_upgrade.value = SaveSystem.SHOT_COOLDOWN - card.current_stat + card.upgrade_value;
        }
        else if (card.card_name.Equals("DASH"))
        {
            slider_next_upgrade.maxValue = SaveSystem.DASH_COOLDOWN;
            slider_next_upgrade.value = SaveSystem.DASH_COOLDOWN - card.current_stat + card.upgrade_value;
        }
        else if (card.card_name.Equals("MULTIPLIER"))
        {
            slider_next_upgrade.minValue = 1;
            slider_next_upgrade.maxValue = card.max_upgrad_value;
            slider_next_upgrade.value = card.current_stat * card.upgrade_value;
        }

        //* current slider values
        slider_current_upgrade.maxValue = card.max_upgrad_value - card.upgrade_value;
        slider_current_upgrade.value = card.current_stat - card.upgrade_value;

        if (card.card_name.Equals("SHOT"))
        {
            slider_current_upgrade.maxValue = SaveSystem.SHOT_COOLDOWN;
            slider_current_upgrade.value = SaveSystem.SHOT_COOLDOWN - card.current_stat;
        }
        else if (card.card_name.Equals("DASH"))
        {
            slider_current_upgrade.maxValue = SaveSystem.DASH_COOLDOWN;
            slider_current_upgrade.value = SaveSystem.DASH_COOLDOWN - card.current_stat;
        }
        else if (card.card_name.Equals("MULTIPLIER"))
        {
            slider_current_upgrade.minValue = 1;
            slider_current_upgrade.maxValue = card.max_upgrad_value;
            slider_current_upgrade.value = card.current_stat;
        }
    }
}