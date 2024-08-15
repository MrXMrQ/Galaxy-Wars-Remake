using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Card/Upgrade/Healthpoints")]
public class HealthpointsCard : Card, IUpgradeable
{
    int max_healthpoints;
    int max_healthpoints_cost;
    int total_score;

    public void Load(GameData gameData)
    {
        max_healthpoints = gameData.max_healthpoints;
        max_healthpoints_cost = gameData.max_healthpoints_cost;
        total_score = gameData.total_score;
        SetUpgradeValuesVisuals(max_healthpoints, max_healthpoints_cost);
    }

    public void Upgrade(GameData gameData)
    {
        if (total_score >= max_healthpoints_cost)
        {
            if (max_healthpoints + upgrade_value <= max_upgrade_stat)
            {
                total_score -= max_healthpoints_cost;
                max_healthpoints_cost = (int)(max_healthpoints_cost * cost_multiplier);
                max_healthpoints += (int)upgrade_value;
            }

            SetUpgradeValuesVisuals(max_healthpoints, max_healthpoints_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Save(GameData gameData)
    {
        gameData.max_healthpoints = max_healthpoints;
        gameData.max_healthpoints_cost = max_healthpoints_cost;
        gameData.total_score = total_score;
        SaveSystem.Save(gameData);
    }
}
