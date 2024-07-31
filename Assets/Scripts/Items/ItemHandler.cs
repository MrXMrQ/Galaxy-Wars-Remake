using UnityEngine;
using System;
using TMPro;

public class ItemHandler : MonoBehaviour
{
    public static ItemHandler Instance { get; private set; }

    [Header("ARRAYS")]
    [SerializeField] GameObject[] prefabs;
    [SerializeField] Slot[] slots;
    [SerializeField] ItemMovement[] items;
    [SerializeField] public bool[] collectedItem = { false, false, false, false, false };


    [Header("Item settings")]
    [SerializeField] TextMeshProUGUI cooldownText;
    [SerializeField] float itemCooldown = 0;
    [SerializeField] PlayerMovement playerMovement;

    [Header("STATS")]
    float _dashCooldown;
    int _healing;
    public static bool isImmortal = false;
    float _shootingCooldown;
    int _maxHealthpoints;

    int _index;
    float _lastItemUse;
    bool _isActive;
    System.Random rnd = new System.Random();

    private void Start()
    {
        Load();
        Instance = this;
        _lastItemUse = Time.time;
        Cooldown();
    }

    void Update()
    {
        if (isImmortal)
        {
            playerMovement.health.healthbar.fill.color = Color.yellow;
        }

        // Check if the current active item needs to be reset
        if (Time.time - _lastItemUse >= items[_index].itemDuration && _isActive)
        {
            _isActive = false;
            isImmortal = false;
            PlayerMovement.Instance.dashCooldown = PlayerMovement.Instance.dashCooldownDefaultValue;
            PlayerMovement.Instance.shotCooldown = PlayerMovement.Instance.shotCooldownDefaultValue;
        }

        // Slot cooldown rendering
        if (_isActive)
        {
            slots[_index].SetSliderValue(items[_index].itemDuration - (Time.time - _lastItemUse));
        }
        else
        {
            slots[_index].SetSliderValue(0);
        }

        // Check item cooldown
        if (Time.time - _lastItemUse <= itemCooldown)
        {
            cooldownText.text = (itemCooldown - (Time.time - _lastItemUse)).ToString("0");
            return;
        }

        // Handle item activation based on input keys
        HandleItemActivation(KeyCode.Alpha1, 0, () =>
        {
            PlayerMovement.Instance.dashCooldown = _dashCooldown;
            //playerController.SetDashingCooldown(_dashCooldown);
        });

        HandleItemActivation(KeyCode.Alpha2, 1, () =>
        {
            PlayerMovement.Instance.health.currentHealthpoints += _healing;
            //playerController.SetHealth(_healing);
        });

        HandleItemActivation(KeyCode.Alpha3, 2, () =>
        {
            isImmortal = true;
            PlayerMovement.Instance.health.healthbar.fill.color = Color.yellow;
            Debug.Log(_maxHealthpoints);
            PlayerMovement.Instance.health.currentHealthpoints = _maxHealthpoints;
        });

        HandleItemActivation(KeyCode.Alpha4, 3, () =>
        {
            PlayerMovement.Instance.shotCooldown = _shootingCooldown;
            //playerController.SetShootingCooldown(_shootingCooldown);
        });
    }

    private void Load()
    {
        GameData gameData = SaveSystem.Load();

        _maxHealthpoints = gameData.maxHealthpoints;
        _dashCooldown = gameData.dashCooldown;
        _healing = gameData.healing;
        _shootingCooldown = gameData.shootingCooldown;
    }

    private void HandleItemActivation(KeyCode keyCode, int imageIndex, Action onActivation)
    {
        //Activate item function (only one item can active)
        if (Input.GetKeyDown(keyCode) && collectedItem[imageIndex] && !_isActive)
        {
            slots[imageIndex].SetSliderMax(items[imageIndex].itemDuration);
            collectedItem[imageIndex] = false;
            _index = imageIndex;
            _isActive = true;
            slots[imageIndex].ResetSprite();
            _lastItemUse = Time.time;
            onActivation?.Invoke();
        }
    }

    public void SpawnItem(Vector2 position)
    {
        Instantiate(prefabs[rnd.Next(0, prefabs.Length)], position, Quaternion.identity);
    }

    public void UpdateSprite(Sprite sprite, int index)
    {
        slots[index - 1].SetSprite(sprite);
    }

    public void Cooldown()
    {
        if (Time.time - _lastItemUse <= itemCooldown)
        {
            cooldownText.text = (itemCooldown - (Time.time - _lastItemUse)).ToString("0");
            return;
        }
    }
}