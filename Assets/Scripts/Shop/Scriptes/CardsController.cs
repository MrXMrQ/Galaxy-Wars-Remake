using System.Collections;
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
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().LoadUpgradeData();
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().UpgradeItem(game_data);
        UpdateText();
    }

    public void GetWeaponCard()
    {
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().LoadWeaponData();
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().UnlockWeapon(game_data);
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
        string current_path = game_data.weapon_prefab_path;
        string equipped_path = cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.weapon_prefab_path;

        if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.is_unlocked_weapon && !current_path.Equals(equipped_path))
        {
            Debug.Log("Equip");
            cards[current_card - 1].GetComponentInChildren<CardDisplay>().EquipWeapon(game_data);
            UpdateOutline();
        }
        else
        {
            if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.is_unlocked_weapon && current_path.Equals(equipped_path))
            {
                StartCoroutine(EquippedBorder(0.25f, 0.05f, 0.025f, new Color(255, 255, 255), true));
            }
            else
            {
                StartCoroutine(EquippedBorder(0.25f, 0.05f, 0.025f, new Color(255, 255, 255), false));
                Debug.Log("Not unlocked");
            }
        }
    }

    public void GetAbilityCard()
    {
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().LoadAbilityData();
        cards[current_card - 1].GetComponentInChildren<CardDisplay>().UnlockAbility(game_data);
        UpdateText();
        UpdateOutline();
    }

    public void EquipAbility()
    {
        string current_path = game_data.ability_scriptableobject_path;
        string equipped_path = cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.ability_scriptableobject_path;

        if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.is_unlocked_ability && !current_path.Equals(equipped_path))
        {
            Debug.Log("rein");
            cards[current_card - 1].GetComponentInChildren<CardDisplay>().EquipAbility(game_data);
            UpdateOutline();
        }
        else
        {
            if (cards[current_card - 1].GetComponentInChildren<CardDisplay>().card.is_unlocked_ability && current_path.Equals(equipped_path))
            {
                StartCoroutine(EquippedBorder(0.25f, 0.05f, 0.025f, new Color(255, 255, 255), true));
            }
            else
            {
                StartCoroutine(EquippedBorder(0.25f, 0.05f, 0.025f, new Color(255, 255, 255), false));
                Debug.Log("Not unlocked");
            }
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
            else if (cards[i].GetComponent<CardDisplay>().card is IAbility)
            {
                if (cards[i].GetComponent<CardDisplay>().card.ability_scriptableobject_path.Equals(game_data.ability_scriptableobject_path))
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

    private IEnumerator EquippedBorder(float totalDuration, float duration_red_border, float duration_white_border, Color default_color, bool is_unlocked)
    {
        cards[current_card - 1].GetComponent<CardDisplay>().outline.enabled = true;
        float elapsedTime = 0f;

        while (elapsedTime < totalDuration)
        {
            cards[current_card - 1].GetComponent<CardDisplay>().outline.effectColor = new Color(255, 0, 0);
            yield return new WaitForSeconds(duration_red_border);
            elapsedTime += duration_red_border;

            if (elapsedTime >= totalDuration) break;

            cards[current_card - 1].GetComponent<CardDisplay>().outline.effectColor = default_color;
            yield return new WaitForSeconds(duration_white_border);
            elapsedTime += duration_white_border;
        }

        // Optional: Reset to the original color after the loop
        cards[current_card - 1].GetComponent<CardDisplay>().outline.effectColor = default_color;

        if (!is_unlocked)
        {
            cards[current_card - 1].GetComponent<CardDisplay>().outline.enabled = false;
        }
    }
}