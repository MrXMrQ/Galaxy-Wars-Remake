using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CardDisplayWeapons : MonoBehaviour
{
    [SerializeField] public WeaponCard weapon_card;
    [SerializeField] TextMeshProUGUI weapon_name_text;
    [SerializeField] TextMeshProUGUI weapon_cost_text;
    [SerializeField] public Outline outline;
    public bool is_eqiped;

    void Start()
    {
        outline.enabled = is_eqiped;
        weapon_card.LoadDataOnCard(SwipeController.game_data);
        UpdateText();
    }


    public void Unlock(GameData game_data)
    {
        weapon_card.Unlock(game_data);
        UpdateText();
    }

    public void Equip()
    {
        if (weapon_card.is_unlocked)
        {
            weapon_card.Equip();
        }
        else
        {
            Debug.LogWarning("not unlocked");
        }
    }

    private void UpdateText()
    {
        weapon_name_text.text = weapon_card.card_name;
        weapon_cost_text.text = weapon_card.is_unlocked ? "UNLOCKED" : weapon_card.cost.ToString();
    }

    public void SetEquip(bool equip)
    {
        is_eqiped = equip;
    }
}