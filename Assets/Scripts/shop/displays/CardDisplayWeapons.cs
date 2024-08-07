using TMPro;
using UnityEngine;

public class CardDisplayWeapons : MonoBehaviour
{
    [SerializeField] WeaponCard weapon_card;
    [SerializeField] TextMeshProUGUI weapon_name_text;
    [SerializeField] TextMeshProUGUI weapon_cost_text;
    GameData _game_data;

    //TODO: logic here
    void Start()
    {
        _game_data = SaveSystem.Load();
        weapon_card.LoadDataOnCard(_game_data);

        UpdateText();
    }


    public void Unlock()
    {
        weapon_card.Unlock(_game_data.total_score);
        UpdateText();
    }

    public void Equip()
    {
        weapon_card.Equip();
    }

    private void UpdateText()
    {
        weapon_name_text.text = weapon_card.card_name;
        weapon_cost_text.text = weapon_card.is_unlocked ? "UNLOCKED" : weapon_card.cost.ToString();
    }
}