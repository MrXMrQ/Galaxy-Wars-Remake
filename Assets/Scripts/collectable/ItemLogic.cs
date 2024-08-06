using UnityEngine;
using TMPro;

public class ItemLogic : MonoBehaviour
{
    public static ItemLogic Instance { get; private set; }

    [Header("ARRAYS")]
    [SerializeField] GameObject[] item_prefabs;
    [SerializeField] GameObject[] coin_prefabs;
    [SerializeField] ItemSlotLogic[] slots;
    [SerializeField] public bool[] collected_items = { false, false, false, false };

    [Header("OBJECTS")]
    [SerializeField] TextMeshProUGUI cooldown_text;

    [Header("VALUES")]
    float _last_item_time;
    bool _is_active;
    int _INDEX;
    int _MAX_HEALTHPOINTS;
    [HideInInspector] public Color healthbar_background;

    [Header("COOLDOWNS")]
    [SerializeField] float item_cooldown;
    float _dash_cooldown;
    int _healing;
    public static bool is_immortal = false;
    float _shooting_cooldown;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        Load();
        _last_item_time = Time.time;
        UpdateCooldownText();
    }

    void Update()
    {
        ResetItems();
        SlotRendering();

        if (Time.time - _last_item_time <= item_cooldown)
        {
            cooldown_text.text = (item_cooldown - (Time.time - _last_item_time)).ToString("0");
            return;
        }

        HandleItemActivation(KeyCode.Alpha1, 0, () =>
        {
            PlayerMovement.Instance.dash_cooldown = _dash_cooldown;
        });

        HandleItemActivation(KeyCode.Alpha2, 1, () =>
        {
            if (_healing + PlayerMovement.Instance.health.current_healthpoints > _MAX_HEALTHPOINTS)
            {
                PlayerMovement.Instance.health.current_healthpoints = _MAX_HEALTHPOINTS;
            }
            else
            {
                PlayerMovement.Instance.health.current_healthpoints += _healing;
            }
        });

        HandleItemActivation(KeyCode.Alpha3, 2, () =>
        {
            PlayerMovement.Instance.health.current_healthpoints = _MAX_HEALTHPOINTS;
            healthbar_background = PlayerMovement.Instance.health.healthbar.fill.color;
            is_immortal = true;
            PlayerMovement.Instance.health.healthbar.fill.color = Color.yellow;
        });

        HandleItemActivation(KeyCode.Alpha4, 3, () =>
        {
            PlayerMovement.Instance.shot_cooldown = _shooting_cooldown;
        });
    }

    private void HandleItemActivation(KeyCode keyCode, int imageIndex, System.Action onActivation)
    {
        //* Activate item function (only one item can be active)
        if (Input.GetKeyDown(keyCode) && collected_items[imageIndex] && !_is_active)
        {
            slots[imageIndex].SetSliderMaxValue(item_prefabs[imageIndex].GetComponent<SpawnItem>().ITEM_DURATION);
            collected_items[imageIndex] = false;
            _INDEX = imageIndex;
            _is_active = true;
            slots[imageIndex].item.sprite = slots[imageIndex].old_sprite;
            _last_item_time = Time.time;
            onActivation?.Invoke();
        }
    }

    private void ResetItems()
    {
        if (Time.time - _last_item_time >= item_prefabs[_INDEX].GetComponent<SpawnItem>().ITEM_DURATION && _is_active)
        {
            if (is_immortal)
            {
                PlayerMovement.Instance.health.healthbar.fill.color = healthbar_background;
            }

            _is_active = false;
            is_immortal = false;
            PlayerMovement.Instance.dash_cooldown = PlayerMovement.Instance.DASH_COOLDOWN_DEFAULT_VALUE;
            PlayerMovement.Instance.shot_cooldown = PlayerMovement.Instance.SHOT_COOLDOWN_DEFAULT_VALUE;
        }
    }

    private void SlotRendering()
    {
        if (_is_active)
        {
            slots[_INDEX].SetSliderValue(item_prefabs[_INDEX].GetComponent<SpawnItem>().ITEM_DURATION - (Time.time - _last_item_time));
        }
        else
        {
            slots[_INDEX].SetSliderValue(0);
        }
    }

    public void UpdateCooldownText()
    {
        if (Time.time - _last_item_time <= item_cooldown)
        {
            cooldown_text.text = (item_cooldown - (Time.time - _last_item_time)).ToString("0");
            return;
        }
    }

    private void Load()
    {
        GameData gameData = SaveSystem.Load();

        _MAX_HEALTHPOINTS = gameData.max_healthpoints;
        _dash_cooldown = gameData.dash_cooldown;
        _healing = gameData.healing;
        _shooting_cooldown = gameData.shot_cooldown;
    }

    public void SpawnItem(Vector2 position)
    {
        int index = Random.Range(0, item_prefabs.Length);
        Instantiate(item_prefabs[index], position, Quaternion.identity);
    }

    public void SpawnCoin(Vector2 position)
    {
        int index = Random.Range(0, coin_prefabs.Length);
        Instantiate(coin_prefabs[index], position, Quaternion.identity);
    }

    public void UpdateSprite(Sprite sprite, int index)
    {
        slots[index].item.sprite = sprite;
    }
}