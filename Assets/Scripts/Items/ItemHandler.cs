using UnityEngine;
using System;
using TMPro;

public class ItemHandler : MonoBehaviour
{
    public static ItemHandler Instance { get; private set; }

    [Header("Arrays")]
    public GameObject[] prefabs;
    public Slot[] slots;
    public ItemMovement[] items;
    public bool[] collectedItem = { false, false, false, false, false };


    [Header("Item settings")]
    public TextMeshProUGUI cooldownText;
    public float itemCooldown = 0;
    public PlayerController playerController;

    [Header("Stats")]
    public float dashCooldown;
    public int healing;
    public bool immortality = false;
    public float shootingCooldown;

    private int index;
    private float lastItemUse;
    private bool isActive;
    private System.Random rnd = new System.Random();

    /*private void Awake()
    {
        /*if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    */

    private void Start()
    {
        Load();
        Instance = this;
        lastItemUse = Time.time;
        Cooldown();
    }

    void Update()
    {
        if (immortality)
        {
            playerController.immortality();
        }

        // Check if the current active item needs to be reset
        if (Time.time - lastItemUse >= items[index].itemDuration && isActive)
        {
            isActive = false;
            immortality = false;
            playerController.ResetDashingCooldown();
            playerController.ResetShootingCooldown();
        }

        // Slot cooldown rendering
        if (isActive)
        {
            slots[index].SetSliderValue(items[index].itemDuration - (Time.time - lastItemUse));
        }

        // Check item cooldown
        if (Time.time - lastItemUse <= itemCooldown)
        {
            cooldownText.text = (itemCooldown - (Time.time - lastItemUse)).ToString("0");
            return;
        }

        // Handle item activation based on input keys
        HandleItemActivation(KeyCode.Alpha1, 0, () =>
        {
            playerController.SetDashingCooldown(dashCooldown);
        });

        HandleItemActivation(KeyCode.Alpha2, 1, () =>
        {
            playerController.SetHealth(healing);
        });

        HandleItemActivation(KeyCode.Alpha3, 2, () =>
        {
            immortality = true;
        });

        HandleItemActivation(KeyCode.Alpha4, 3, () =>
        {
            playerController.SetShootingCooldown(shootingCooldown);
        });
    }

    private void Load()
    {
        GameData gameData = SaveSystem.Load();

        dashCooldown = gameData.dashCooldown;
        healing = gameData.healing;
        shootingCooldown = gameData.shootingCooldown;

    }

    private void HandleItemActivation(KeyCode keyCode, int imageIndex, Action onActivation)
    {
        //Activate item function (only one item can active)
        if (Input.GetKeyDown(keyCode) && collectedItem[imageIndex] && !isActive)
        {
            slots[imageIndex].SetSliderMax(items[imageIndex].itemDuration);
            collectedItem[imageIndex] = false;
            index = imageIndex;
            isActive = true;
            slots[imageIndex].ResetSprite();
            lastItemUse = Time.time;
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
        // Check item cooldown
        if (Time.time - lastItemUse <= itemCooldown)
        {
            cooldownText.text = (itemCooldown - (Time.time - lastItemUse)).ToString("0");
            return;
        }
    }
}