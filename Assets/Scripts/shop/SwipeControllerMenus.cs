using UnityEngine;

public class SwipeControllerMenus : MonoBehaviour
{
    [SerializeField] Vector3 cardStep;
    [SerializeField] RectTransform cardsContainerRect;
    [SerializeField] RectTransform[] cards;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] int currentCard;
    Vector3 _targetPosition;

    void Start()
    {
        _targetPosition = cardsContainerRect.localPosition;
    }

    void Update()
    {
        // Navigate to the next card when W or Up Arrow is pressed
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            PreviousCard();
        }

        // Navigate to the previous card when S or Down Arrow is pressed
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            NextCard();
        }

        // Navigate using the mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // Scroll up
        {
            PreviousCard();
        }
        else if (scroll < 0f) // Scroll down
        {
            NextCard();
        }
    }

    public void NextCard()
    {
        if (currentCard < cards.Length - 1)
        {
            currentCard++;
            _targetPosition -= cardStep;  // Adjust this based on how your cards are arranged
            MoveCards();
        }
    }

    public void PreviousCard()
    {
        if (currentCard > 0)
        {
            currentCard--;
            _targetPosition += cardStep;  // Adjust this based on how your cards are arranged
            MoveCards();
        }
    }

    private void MoveCards()
    {
        cardsContainerRect.LeanMoveLocal(_targetPosition, tweenTime).setEase(tweenType);
    }
}