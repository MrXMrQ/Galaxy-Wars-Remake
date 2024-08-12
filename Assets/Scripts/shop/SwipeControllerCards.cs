using TMPro;
using UnityEngine;

public class SwipeControllerCards : MonoBehaviour
{
    [SerializeField] Vector3 card_step;
    [SerializeField] RectTransform cards_container_rect;
    [SerializeField] RectTransform[] cards;
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
        UpdateOutline();
    }

    public void NextCard()
    {
        if (current_card < cards.Length)
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
        for (int i = 0; i < cards.Length; i++)
        {
            float target_scale = (i + 1 == current_card) ? SCALE_UP : SCALE_DOWN;
            cards[i].LeanScale(Vector3.one * target_scale, TWEEN_TIME).setEase(tween_type);
        }
    }

    public void GetUpgradeCard()
    {
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().Upgrade(game_data);
        UpdateText();
    }

    public void GetWeaponCard()
    {
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().Unlock(game_data);
        UpdateText();
        UpdateOutline();
    }

    public void EquipWeapon()
    {
        if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().item.is_unlocked)
        {
            if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().item.weapon_prefab_path.Equals(game_data.weapon_prefab))
            {
                game_data.weapon_prefab = "prefabs/player_projectiles/player_projectile_default";
                SaveSystem.Save(game_data);
                game_data = SaveSystem.Load();
            }
            else
            {
                cards[current_card - 1].GetComponentInChildren<CardDisplay>().Equip(game_data);
            }
            UpdateOutline();
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

    private void UpdateOutline()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<CardDisplay>().item is IWeapon)
            {
                Debug.Log("jap");

                if (cards[i].GetComponent<CardDisplay>().item.weapon_prefab_path.Equals(game_data.weapon_prefab))
                {
                    cards[i].GetComponent<CardDisplay>().outline.enabled = true;
                }
                else
                {
                    cards[i].GetComponent<CardDisplay>().outline.enabled = false;
                }
            }

        }
    }
}