using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SwipeController : MonoBehaviour
{
    [SerializeField] Vector3 card_step;
    [SerializeField] RectTransform cards_container_rect;
    [SerializeField] RectTransform[] cardes;
    [SerializeField] float TWEEN_TIME;
    [SerializeField] LeanTweenType tween_type;
    [SerializeField] float SCALE_UP;
    [SerializeField] float SCALE_DOWN;
    [SerializeField] TextMeshProUGUI total_scroe_text;
    [SerializeField] TextMeshProUGUI level_text;
    [SerializeField] int current_card;
    Vector3 _target_position;
    public static GameData game_data;

    void Awake()
    {
        game_data = SaveSystem.Load();
    }
    void Start()
    {

        _target_position = cards_container_rect.localPosition;
        UpdateText();
        CardScaling();
    }

    public void NextCard()
    {
        if (current_card < cardes.Length)
        {
            current_card++;
            _target_position += card_step;
            MoveCards();
        }
    }

    public void PreviousCard()
    {
        if (current_card > 1)
        {
            current_card--;
            _target_position -= card_step;
            MoveCards();
        }
    }

    private void MoveCards()
    {
        cards_container_rect.LeanMoveLocal(_target_position, TWEEN_TIME).setEase(tween_type);
        CardScaling();
    }

    private void CardScaling()
    {
        for (int i = 0; i < cardes.Length; i++)
        {
            float target_scale = (i + 1 == current_card) ? SCALE_UP : SCALE_DOWN;
            cardes[i].LeanScale(Vector3.one * target_scale, TWEEN_TIME).setEase(tween_type);
        }
    }

    public void GetUpgradeCard()
    {
        cardes[current_card - 1].GetComponentInChildren<CardDisplayUpgrades>().Upgrade(game_data);
        UpdateText();
    }

    public void GetWeaponCard()
    {
        cardes[current_card - 1].GetComponentInChildren<CardDisplayWeapons>().Unlock(game_data);
        UpdateText();
    }

    public void EquipWeapon()
    {
        if (cardes[current_card - 1].GetComponentInChildren<CardDisplayWeapons>().weapon_card.is_unlocked)
        {
            cardes[current_card - 1].GetComponentInChildren<CardDisplayWeapons>().Equip();
        }
        else
        {
            Debug.LogWarning("not unlocked");
        }
    }

    private void UpdateText()
    {
        total_scroe_text.text = "Score Points: " + game_data.total_score.ToString();
        level_text.text = "Level: " + game_data.level.ToString();
    }
}