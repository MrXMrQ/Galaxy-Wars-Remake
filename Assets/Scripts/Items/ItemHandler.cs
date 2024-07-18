using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ItemHandler : MonoBehaviour
{
    public static ItemHandler Instance { get; private set; }

    [Header("Arrays")]
    public GameObject[] prefabs;
    public Image[] images;
    public ItemMovement[] items;
    public TextMeshProUGUI[] cooldownTextItem;
    public bool[] collectedItem = { false, false, false, false, false };

    [Header("Item settings")]
    public Color slotColor;
    public TextMeshProUGUI cooldownText;
    public float itemCooldown;
    public PlayerController playerController;

    [Header("Stats")]
    public float dashingCooldown;
    public float explosionRadius;
    public int healing;
    public bool immortality = false;
    public float shootingCooldown;

    private int index;
    private float lastItemUse = 0f;
    private bool isActive;
    private System.Random rnd = new System.Random();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (immortality)
        {
            playerController.immortality();
        }

        // Check if any active item needs to be reset
        if (Time.time - lastItemUse >= items[index].itemDuration && isActive)
        {
            isActive = false;
            immortality = false;
            playerController.ResetDashingCooldown();
            playerController.ResetShootingCooldown();
        }

        if (isActive)
        {
            cooldownTextItem[index].text = (items[index].itemDuration - (Time.time - lastItemUse)).ToString("0");
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
            playerController.SetDashingCooldown(dashingCooldown);
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

    private void HandleItemActivation(KeyCode keyCode, int imageIndex, Action onActivation)
    {
        if (Input.GetKeyDown(keyCode) && collectedItem[imageIndex])
        {
            collectedItem[imageIndex] = false;
            index = imageIndex;
            isActive = true;
            images[imageIndex].sprite = null;
            images[imageIndex].color = slotColor;
            lastItemUse = Time.time;
            onActivation?.Invoke();
        }
    }

    public void SpawnItem(Vector2 position)
    {
        Instantiate(prefabs[rnd.Next(0, prefabs.Length)], position, Quaternion.identity);
    }

    public void updateColor(Sprite sprite, int index)
    {
        images[index - 1].color = Color.white;
        images[index - 1].sprite = sprite;
    }
}