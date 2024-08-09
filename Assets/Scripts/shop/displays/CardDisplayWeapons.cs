using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CardDisplayWeapons : MonoBehaviour
{
    [SerializeField] public WeaponCard weapon_card;
    [SerializeField] TextMeshProUGUI weapon_name_text;
    [SerializeField] TextMeshProUGUI weapon_cost_text;
    [SerializeField] public Outline outline;

    void Start()
    {
        weapon_card.LoadDataOnCard(SwipeController.game_data);
        UpdateText();
    }


    public void Unlock(GameData game_data)
    {
        weapon_card.Unlock(game_data);
        UpdateText();
    }

    public void Equip(GameData game_data)
    {
        weapon_card.Equip(game_data);
    }

    private void UpdateText()
    {
        weapon_name_text.text = weapon_card.card_name;
        weapon_cost_text.text = weapon_card.is_unlocked ? "UNLOCKED" : weapon_card.cost.ToString();
    }
}