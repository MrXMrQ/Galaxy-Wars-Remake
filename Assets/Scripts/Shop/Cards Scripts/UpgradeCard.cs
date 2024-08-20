using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Card/Upgrade")]
public class UpgradeCard : Card
{
    public enum KEYS
    {
        healthpoints,
        healing,
        dash_cooldown,
        shot_cooldown,
        damage,
        multiplier,
    }

    [Header("UPGRADE")]
    [SerializeField] public KEYS keys;
    [SerializeField] public float MAX_UPGRADE_STAT;
    [SerializeField] public float UPGRADE_VALUE;
    [SerializeField] float MULTIPLIER;
    [HideInInspector] public string key;
    [HideInInspector] public float current_stat;
    [HideInInspector] public Slider slider_current;
    [HideInInspector] public Slider slider_next;
    [HideInInspector] public int cost;
    float stat;
    int total_score;

    public void Load(GameData game_data)
    {
        key = keys.ToString();

        stat = game_data.item_stats[key];
        cost = game_data.item_cost[key];

        UpdateSlider();
        current_stat = stat;
        total_score = game_data.total_score;
    }

    public void ApplyUpgrade(GameData game_data)
    {
        if (total_score >= cost && ((keys == KEYS.dash_cooldown || keys == KEYS.shot_cooldown) ? stat > MAX_UPGRADE_STAT : stat < MAX_UPGRADE_STAT))
        {
            total_score -= cost;
            stat = (keys == KEYS.dash_cooldown || keys == KEYS.shot_cooldown) ? stat - UPGRADE_VALUE : stat + UPGRADE_VALUE;
            if (stat < 0)
            {
                stat = 0;
            }

            current_stat = stat;
            cost = Mathf.RoundToInt(cost * MULTIPLIER);

            UpdateSlider();

            Save(game_data);
        }
    }

    public void Save(GameData game_data)
    {
        game_data.item_stats[key] = stat;
        game_data.item_cost[key] = cost;
        game_data.total_score = total_score;

        SaveSystem.Save(game_data);
    }

    private void UpdateSlider()
    {
        slider_current.value = stat - UPGRADE_VALUE;
        slider_next.value = slider_current.value + UPGRADE_VALUE;

        switch (keys)
        {
            case KEYS.dash_cooldown:
                slider_current.value = SaveSystem.DASH_COOLDOWN - stat;
                slider_next.value = SaveSystem.DASH_COOLDOWN - stat + UPGRADE_VALUE;
                break;
            case KEYS.shot_cooldown:
                slider_current.value = SaveSystem.SHOT_COOLDOWN - stat;
                slider_next.value = SaveSystem.SHOT_COOLDOWN - stat + UPGRADE_VALUE;
                break;
        }
    }
}