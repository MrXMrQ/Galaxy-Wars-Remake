using UnityEngine;

[CreateAssetMenu(fileName = "_card", menuName = "Card")]
public class Card : ScriptableObject
{
    public enum UPGRADES_TYPES
    {
        Healthpoints,
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
                current_stat = game_data.maxHealthpoints;
                cost = game_data.maxHealthpointsCost;
                break;
            case UPGRADES_TYPES.Dash:
                current_stat = game_data.dashCooldown;
                cost = game_data.dashCooldownCost;
                break;
            case UPGRADES_TYPES.Healing:
                current_stat = game_data.healing;
                cost = game_data.healingCost;
                break;
            case UPGRADES_TYPES.Shot:
                current_stat = game_data.shootingCooldown;
                cost = game_data.shootingCooldownCost;
                break;
            case UPGRADES_TYPES.Multiplier:
                current_stat = game_data.multiplier;
                cost = game_data.multiplierCost;
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
                    current_stat = game_data.maxHealthpoints;
                    break;
                case UPGRADES_TYPES.Dash:
                    upgrade_applied = DashUpgrade(game_data);
                    current_stat = game_data.dashCooldown;
                    break;
                case UPGRADES_TYPES.Healing:
                    upgrade_applied = HealingUpgrade(game_data);
                    current_stat = game_data.healing;
                    break;
                case UPGRADES_TYPES.Shot:
                    upgrade_applied = ShotUpgrade(game_data);
                    current_stat = game_data.shootingCooldown;
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
        if (game_data.maxHealthpoints < max_upgrad_value)
        {
            game_data.maxHealthpoints += (int)upgrade_value;

            game_data.totalScore -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.maxHealthpointsCost = cost;

            return true;
        }

        return false;
    }

    private bool DashUpgrade(GameData game_data)
    {
        if (game_data.dashCooldown - upgrade_value >= max_upgrad_value)
        {
            game_data.dashCooldown -= upgrade_value;

            game_data.totalScore -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.dashCooldownCost = cost;

            return true;
        }
        else if (game_data.dashCooldown - upgrade_value != max_upgrad_value - upgrade_value)
        {
            game_data.totalScore -= cost;
            game_data.dashCooldown = 0;
            return true;
        }
        return false;
    }

    private bool HealingUpgrade(GameData game_data)
    {
        if (game_data.healing < max_upgrad_value)
        {
            game_data.healing += (int)upgrade_value;

            game_data.totalScore -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.healingCost = cost;

            return true;
        }
        return false;
    }

    private bool ShotUpgrade(GameData game_data)
    {
        if (game_data.shootingCooldown - upgrade_value >= max_upgrad_value)
        {
            game_data.shootingCooldown -= upgrade_value;

            game_data.totalScore -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.shootingCooldownCost = cost;

            return true;
        }
        else if (game_data.shootingCooldown - upgrade_value != max_upgrad_value - upgrade_value)
        {
            game_data.totalScore -= cost;
            game_data.shootingCooldown = 0;
            return true;
        }
        return false;
    }

    private bool MultiplierUpgrade(GameData game_data)
    {
        if (game_data.multiplier * upgrade_value <= max_upgrad_value)
        {
            game_data.multiplier *= (int)upgrade_value;

            game_data.totalScore -= cost;
            cost = (int)(cost * increase_cost_for_upgrade);
            game_data.multiplierCost = cost;

            return true;
        }
        return false;
    }
}