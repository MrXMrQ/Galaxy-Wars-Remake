using TMPro;
using UnityEngine;

public class CardsController : MonoBehaviour
{
    [Header("SWIPE MENU")]
    [SerializeField] RectTransform cards_container;
    [SerializeField] public RectTransform[] cards;
    [SerializeField] int current_card;
    [SerializeField] Vector3 card_step;
    [HideInInspector] public bool is_focused;

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
        GetComponentInChildren<CardDisplay>().UpdateOutline(game_data);
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

    public void ButtonPressUnlock()
    {
        CardDisplay cardDisplay = cards[current_card - 1].GetComponentInChildren<CardDisplay>();
        cardDisplay.Load(game_data);

        if (cardDisplay.card is UpgradeCard)
        {
            cardDisplay.Upgrade(game_data);
        }
        else if (cardDisplay.card is WeaponCard)
        {
            cardDisplay.Unlock(game_data, cardDisplay.card);
        }
        else if (cardDisplay.card is AbilityCard)
        {
            cardDisplay.Unlock(game_data, cardDisplay.card);
        }

        UpdateText();
    }

    public void ButtonPressEquip()
    {
        CardDisplay cardDisplay = cards[current_card - 1].GetComponentInChildren<CardDisplay>();
        cardDisplay.Load(game_data);

        if (cardDisplay.card is WeaponCard)
        {
            cardDisplay.Equip(game_data, cardDisplay.card);
        }
        else if (cardDisplay.card is AbilityCard)
        {
            cardDisplay.Equip(game_data, cardDisplay.card);
        }
    }

    private void UpdateText()
    {
        total_scroe_text.text = "Score Points: " + game_data.total_score.ToString();
        level_text.text = "Level: " + game_data.level.ToString();
    }
}