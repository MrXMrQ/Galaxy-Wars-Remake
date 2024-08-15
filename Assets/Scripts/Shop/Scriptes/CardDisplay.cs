using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] public Card card;
    [SerializeField] private TextMeshProUGUI card_name;
    [SerializeField] private TextMeshProUGUI cost_text;
    [SerializeField] private Slider slider_current_upgrade;
    [SerializeField] private Slider slider_next_upgrade;
    [SerializeField] public Outline outline;

    private void Start()
    {
        LoadUpgradeData();
        LoadWeaponData();
        LoadAbilityData();

        if (card is IUpgradeable)
        {
            UpgradesVisuals();
        }
        else if (card is IWeapon)
        {
            WeaponVisuals();
        }
        else if (card is IAbility)
        {
            AbilityVisuals();
        }
    }

    public void UpgradeItem(GameData game_data)
    {
        if (card is IUpgradeable upgradeableItem)
        {
            upgradeableItem.Upgrade(game_data);
            UpgradesVisuals();
        }
        else
        {
            Debug.LogWarning("This item cannot be upgraded.");
        }
    }

    public void UnlockWeapon(GameData game_data)
    {
        if (card is IWeapon weaponItem)
        {
            weaponItem.Unlock(game_data);
            WeaponVisuals();
        }
        else
        {
            Debug.LogWarning("This item cannot be unlocked.");
        }
    }

    public void EquipWeapon(GameData game_data)
    {
        if (card is IWeapon weaponItem)
        {
            weaponItem.Equip(game_data);
            WeaponVisuals();
        }
        else
        {
            Debug.LogWarning("This item cannot be equipped.");
        }
    }

    public void UnlockAbility(GameData gameData)
    {
        if (card is IAbility abilityItem)
        {
            abilityItem.Unlock(gameData);
            AbilityVisuals();
        }
        else
        {
            Debug.LogWarning("This item cannot be unlocked.");
        }
    }

    public void EquipAbility(GameData gameData)
    {
        if (card is IAbility abilityItem)
        {
            abilityItem.Equip(gameData);
            AbilityVisuals();
        }
        else
        {
            Debug.LogWarning("This item cannot be equipped.");
        }
    }

    public void LoadUpgradeData()
    {
        if (card is IUpgradeable upgradeableItem)
        {
            upgradeableItem.Load(CardsController.game_data);
        }
    }

    public void LoadWeaponData()
    {
        if (card is IWeapon weaponItem)
        {
            weaponItem.Load(CardsController.game_data);
        }
    }

    public void LoadAbilityData()
    {
        if (card is IAbility abilityItem)
        {
            abilityItem.Load(CardsController.game_data);
        }
    }

    private void UpgradesVisuals()
    {
        card_name.text = card.card_name;
        cost_text.text = card.current_stat == card.max_upgrade_stat ? "MAX" : $"Cost: {card.upgrade_cost}";

        UpdateSliderValues(slider_next_upgrade, isCurrent: false);
        UpdateSliderValues(slider_current_upgrade, isCurrent: true);
    }

    private void UpdateSliderValues(Slider slider, bool isCurrent)
    {
        slider.maxValue = card.max_upgrade_stat - card.upgrade_value;
        slider.value = isCurrent ? card.current_stat - card.upgrade_value : card.current_stat;

        switch (card.upgrade_type)
        {
            case Card.UPGRADES_TYPES.Shot:
                slider.maxValue = SaveSystem.SHOT_COOLDOWN;
                slider.value = SaveSystem.SHOT_COOLDOWN - card.current_stat + (isCurrent ? 0 : card.upgrade_value);
                break;

            case Card.UPGRADES_TYPES.Dash:
                slider.maxValue = SaveSystem.DASH_COOLDOWN;
                slider.value = SaveSystem.DASH_COOLDOWN - card.current_stat + (isCurrent ? 0 : card.upgrade_value);
                break;

            case Card.UPGRADES_TYPES.Multiplier:
                slider.minValue = 1;
                slider.maxValue = card.max_upgrade_stat;
                slider.value = isCurrent ? card.current_stat : card.current_stat * card.upgrade_value;
                break;
        }
    }

    private void WeaponVisuals()
    {
        card_name.text = card.card_name;
        cost_text.text = card.is_unlocked_weapon ? "UNLOCKED" : $"Cost: {card.weapon_cost}";
    }

    private void AbilityVisuals()
    {
        card_name.text = card.card_name;
        cost_text.text = card.is_unlocked_ability ? "UNLOCKED" : $"Cost: {card.ability_cost}";
    }
}