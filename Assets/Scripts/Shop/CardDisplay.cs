using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] public Card card;
    [SerializeField] TextMeshProUGUI text_name;
    [SerializeField] TextMeshProUGUI text_cost;

    void Start()
    {
        Load(CardsController.game_data);
        Visuals(card);
    }

    public void Upgrade(GameData game_data)
    {
        if (card is UpgradeCard upgradeableItem)
        {
            upgradeableItem.ApplyUpgrade(game_data);
            Visuals(upgradeableItem);
        }
        else
        {
            Debug.LogWarning("No upgrade availabel");
        }
    }

    public void Unlock(GameData game_data, Card card)
    {
        if (card is WeaponCard weapon_card)
        {
            weapon_card.Unlock(game_data);
        }
        else if (card is AbilityCard ability_card)
        {
            ability_card.Unlock(game_data);
        }

        UpdateOutline(game_data);
        Visuals(card);
    }

    public void Equip(GameData game_data, Card card)
    {
        if (card is WeaponCard weapon_card && weapon_card.is_unlocked)
        {
            weapon_card.Equip(game_data);
            UpdateOutline(game_data);
        }
        else if (card is AbilityCard ability_card && ability_card.is_unlocked)
        {
            ability_card.Equip(game_data);
            UpdateOutline(game_data);
        }
        else
        {
            StartCoroutine(EquippedBorder(0.2f, 0.025f, new Color(255, 0, 0), Color.white, game_data));
        }
    }

    private void Visuals(Card card)
    {
        text_name.text = this.card.NAME;
        string cost_text = string.Empty;

        if (card is UpgradeCard upgrade_card)
        {
            cost_text = upgrade_card.current_stat == upgrade_card.MAX_UPGRADE_STAT ? "MAX" : upgrade_card.cost.ToString();
        }

        if (card is WeaponCard weapon_card)
        {
            cost_text = weapon_card.is_unlocked ? "UNLOCKED" : weapon_card.COST.ToString();
        }

        if (card is AbilityCard ability_card)
        {
            cost_text = ability_card.is_unlocked ? "UNLOCKED" : ability_card.COST.ToString();
        }

        text_cost.text = cost_text;
    }

    public void Load(GameData game_data)
    {
        if (card is UpgradeCard upgrade_card)
        {
            upgrade_card.Load(game_data);
        }

        if (card is WeaponCard weapon_card)
        {
            weapon_card.Load(game_data);
        }

        if (card is AbilityCard ability_card)
        {
            ability_card.Load(game_data);
        }
    }

    public void UpdateOutline(GameData game_data)
    {
        CardsController cards_controller = GetComponentInParent<CardsController>();

        foreach (var cardDisplay in cards_controller.cards)
        {
            Card card = cardDisplay.GetComponent<CardDisplay>().card;

            bool change_outline;

            if (card is WeaponCard weapon_card)
            {
                change_outline = weapon_card.WEAPON_PATH.Equals(game_data.weapon_path);
                weapon_card.outline.enabled = change_outline;
            }
            else if (card is AbilityCard ability_card)
            {
                change_outline = ability_card.ABILITY_PATH.Equals(game_data.ability_path);
                ability_card.outline.enabled = change_outline;
            }
        }
    }

    public IEnumerator EquippedBorder(float total_duration, float duration_border, Color blink_color, Color default_color, GameData game_data)
    {
        Outline outline = null;
        bool is_unlocked = false;

        if (card is WeaponCard weapon_card)
        {
            outline = weapon_card.outline;
            is_unlocked = weapon_card.is_unlocked;
        }
        else if (card is AbilityCard ability_card)
        {
            outline = ability_card.outline;
            is_unlocked = ability_card.is_unlocked;
        }

        if (outline != null && !is_unlocked)
        {
            outline.enabled = true;

            float elapsed_time = 0;

            while (elapsed_time < total_duration)
            {
                outline.effectColor = blink_color;
                yield return new WaitForSeconds(duration_border);

                elapsed_time += duration_border;

                if (elapsed_time >= total_duration)
                {
                    break;
                }

                outline.effectColor = default_color;
                yield return new WaitForSeconds(duration_border);
                elapsed_time += duration_border;
            }

            /*outline.effectColor = default_color;
            outline.enabled = false;*/
        }

        UpdateOutline(game_data);
    }
}