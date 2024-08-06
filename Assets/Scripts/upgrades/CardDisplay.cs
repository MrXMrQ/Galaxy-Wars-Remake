using TMPro;
using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] Card card;
    [SerializeField] TextMeshProUGUI card_name;
    [SerializeField] TextMeshProUGUI cost_text;
    GameData _game_data;

    void Start()
    {
        _game_data = SaveSystem.Load();
        card.LoadDataOnCard(_game_data);

        UpdateText();
    }

    public void Upgrade()
    {
        _game_data = SaveSystem.Load();
        card.ApplyUpgrade(_game_data.totalScore);
        UpdateText();
    }

    private void UpdateText()
    {
        card_name.text = card.card_name;
        cost_text.text = card.current_stat == card.max_upgrad_value ? "MAX" : "Cost: " + card.cost.ToString();
    }
}
