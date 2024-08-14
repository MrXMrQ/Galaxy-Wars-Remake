using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("SWIPE MENU")]
    [SerializeField] Vector3 card_step;
    [SerializeField] RectTransform cards_container;
    [SerializeField] RectTransform[] cards;

    [Header("LEAN TWEEN")]
    [SerializeField] float tween_time;
    [SerializeField] LeanTweenType tween_type;
    [SerializeField] int current_card;

    Vector3 _target_position;
    CardsController swipe_controller_cards;

    void Start()
    {
        _target_position = cards_container.localPosition;
        SetFocus();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            PreviousCard();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            NextCard();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            PreviousCard();
        }
        else if (scroll < 0f)
        {
            NextCard();
        }
    }

    public void NextCard()
    {
        if (current_card < cards.Length - 1)
        {
            current_card++;
            _target_position -= card_step;
            MoveCards();
        }
    }

    public void PreviousCard()
    {
        if (current_card > 0)
        {
            current_card--;
            _target_position += card_step;  // Adjust this based on how your cards are arranged
            MoveCards();
        }
    }

    private void MoveCards()
    {
        cards_container.LeanMoveLocal(_target_position, tween_time).setEase(tween_type);
        SetFocus();
    }

    private void SetFocus()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            swipe_controller_cards = cards[i].GetComponentInChildren<CardsController>();

            if (i == current_card && swipe_controller_cards != null)
            {
                swipe_controller_cards.is_focused = true;
            }
            else
            {
                swipe_controller_cards.is_focused = false;
            }
        }
    }
}