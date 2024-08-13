using TMPro;
using UnityEngine;

public class CardsController : MonoBehaviour
{
    [Header("SWIPE MENU")]
    [SerializeField] RectTransform cards_container;
    [SerializeField] public RectTransform[] cards;
    [SerializeField] int current_card;
    [SerializeField] Vector3 card_step;
    public bool is_focused;

    [Header("LEAN TWEEN")]
    [SerializeField] float TWEEN_TIME;
    [SerializeField] LeanTweenType TWEEN_TYPE;
    [SerializeField] float SCALE_UP;
    [SerializeField] float SCALE_DOWN;

    [Header("GUI COMPONENTS")]
    [SerializeField] TextMeshProUGUI total_scroe_text;
    [SerializeField] TextMeshProUGUI level_text;

    Vector3 _target_position;
    public static GameData game_data;

    void Awake()
    {
        game_data = SaveSystem.Load();
    }

    void Start()
    {
        _target_position = cards_container.localPosition;

        UpdateText();
        CardScaling();
        UpdateOutline();
    }

    void Update()
    {
        if (!is_focused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && is_focused)
        {
            PreviousCard();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && is_focused)
        {
            NextCard();
        }
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
        cards_container.LeanMoveLocal(_target_position, TWEEN_TIME).setEase(TWEEN_TYPE);
        CardScaling();
    }

    private void CardScaling()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            float target_scale = (i + 1 == current_card) ? SCALE_UP : SCALE_DOWN;
            cards[i].LeanScale(Vector3.one * target_scale, TWEEN_TIME).setEase(TWEEN_TYPE);
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

    private void UpdateText()
    {
        total_scroe_text.text = "Score Points: " + game_data.total_score.ToString();
        level_text.text = "Level: " + game_data.level.ToString();
    }

    public void EquipWeapon()
    {
        if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.is_unlocked)
        {
            if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.weapon_prefab_path.Equals(game_data.weapon_prefab_path))
            {
                game_data.weapon_prefab_path = "prefabs/player_projectiles/player_projectile_default";
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

    private void UpdateOutline()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<CardDisplay>().card is IWeapon)
            {
                if (cards[i].GetComponent<CardDisplay>().card.weapon_prefab_path.Equals(game_data.weapon_prefab_path))
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