using UnityEngine;

[CreateAssetMenu(fileName = "_card", menuName = "Upgrade Card")]
public class CardUpgrades : ScriptableObject
{
    public enum UPGRADES_TYPES
    {
        Healthpoints,
        Damage,
        Dash,
        Healing,
        Shot,
        Multiplier
    }

    [SerializeField] UPGRADES_TYPES _upgrade_type;
    [SerializeField] public string card_name;
    [SerializeField] float upgrade_value;
    [SerializeField] public float max_upgrad_value;
    [SerializeField] float increase_cost_for_upgrade;
    [HideInInspector] public float current_stat;
    [HideInInspector] public int cost;

    public void LoadDataOnCard(GameData game_data)
    {
        switch (_upgrade_type)
        {
            case UPGRADES_TYPES.Healthpoints:
                current_stat = game_data.max_healthpoints;
                cost = game_data.max_healthpoints_cost;
                break;
            case UPGRADES_TYPES.Damage:
                current_stat = game_data.damge;
                cost = game_data.damge_cost;
                break;
            case UPGRADES_TYPES.Dash:
                current_stat = game_data.dash_cooldown;
                cost = game_data.dash_cooldown_cost;
                break;
            case UPGRADES_TYPES.Healing:
                current_stat = game_data.healing;
                cost = game_data.healing_cost;
                break;
            case UPGRADES_TYPES.Shot:
                current_stat = game_data.shot_cooldown;
                cost = game_data.shot_cooldown_cost;
                break;
            case UPGRADES_TYPES.Multiplier:
                current_stat = game_data.multiplier;
                cost = game_data.multiplier_cost;
                break;
            default:
                Debug.LogWarning("Upgrade name not recognized.");
                break;
        }
    }

    public void ApplyUpgrade(int total_score)
    {
        GameData game_data = SaveSystem.Load();
        if (total_score >= cost)
        {
            bool upgrade_applied = false;

            switch (_upgrade_type)
            {
                case UPGRADES_TYPES.Healthpoints:
                    upgrade_applied = HealthpointsUpgrade(game_data);
                    current_stat = game_data.max_healthpoints;
                    break;
                case UPGRADES_TYPES.Damage:
                    upgrade_applied = DamageUpgrade(game_data);
                    current_stat = game_data.damge;
                    break;
                case UPGRADES_TYPES.Dash:
                    upgrade_applied = DashUpgrade(game_data);
                    current_stat = game_data.dash_cooldown;
                    break;
                case UPGRADES_TYPES.Healing:
                    upgrade_applied = HealingUpgrade(game_data);
                    current_stat = game_data.healing;
                    break;
                case UPGRADES_TYPES.Shot:
                    upgrade_applied = ShotUpgrade(game_data);
                    current_stat = game_data.shot_cooldown;
                    break;
                case UPGRADES_TYPES.Multiplier:
                    upgrade_applied = MultiplierUpgrade(game_data);
                    current_stat = game_data.multiplier;
                    break;
                default:
                    Debug.LogWarning("Upgrade name not recognized.");
                    break;
            }

            if (upgrade_applied)
            {
                game_data.level++;
                SaveSystem.Save(game_data);
            }
        }
    }

    private bool HealthpointsUpgrade(GameData game_data)
    {
        if (game_data.max_healthpoints < max_upgrad_value)
        {
            game_data.max_healthpoints += (int)upgrade_value;

            game_data.total_score -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.max_healthpoints_cost = cost;

            return true;
        }

        return false;
    }

    private bool DamageUpgrade(GameData game_data)
    {
        if (game_data.damge < max_upgrad_value)
        {
            game_data.damge += (int)upgrade_value;

            game_data.total_score -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.damge_cost = cost;

            return true;
        }

        return false;
    }

    private bool DashUpgrade(GameData game_data)
    {
        if (game_data.dash_cooldown - upgrade_value >= max_upgrad_value)
        {
            game_data.dash_cooldown -= upgrade_value;

            game_data.total_score -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.dash_cooldown_cost = cost;

            return true;
        }
        else if (game_data.dash_cooldown - upgrade_value != max_upgrad_value - upgrade_value)
        {
            game_data.total_score -= cost;
            game_data.dash_cooldown = 0;
            return true;
        }
        return false;
    }

    private bool HealingUpgrade(GameData game_data)
    {
        if (game_data.healing < max_upgrad_value)
        {
            game_data.healing += (int)upgrade_value;

            game_data.total_score -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.healing_cost = cost;

            return true;
        }
        return false;
    }

    private bool ShotUpgrade(GameData game_data)
    {
        if (game_data.shot_cooldown - upgrade_value >= max_upgrad_value)
        {
            game_data.shot_cooldown -= upgrade_value;

            game_data.total_score -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.shot_cooldown_cost = cost;

            return true;
        }
        else if (game_data.shot_cooldown - upgrade_value != max_upgrad_value - upgrade_value)
        {
            game_data.total_score -= cost;
            game_data.shot_cooldown = 0;
            return true;
        }
        return false;
    }

    private bool MultiplierUpgrade(GameData game_data)
    {
        if (game_data.multiplier * upgrade_value <= max_upgrad_value)
        {
            game_data.multiplier *= (int)upgrade_value;

            game_data.total_score -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.multiplier_cost = cost;

            return true;
        }
        return false;
    }
}