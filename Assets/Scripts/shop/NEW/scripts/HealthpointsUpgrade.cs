using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Upgrade/Healthpoints")]
public class HealthpointsUpgrade : Upgrades
{
    private int max_healthpoints;
    private int max_healthpoints_cost;
    private int total_score;

    public void Upgrade(GameData gameData)
    {
        Load(gameData);

        if (total_score >= max_healthpoints_cost)
        {
            if (max_healthpoints + upgrade_value <= max_upgrade_stat)
            {
                total_score -= max_healthpoints_cost;
                max_healthpoints_cost = (int)(max_healthpoints_cost * cost_multiplier);
                max_healthpoints += (int)upgrade_value;
            }

            SetValuesForVisuals(max_healthpoints, max_healthpoints_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Load(GameData gameData)
    {
        max_healthpoints = gameData.max_healthpoints;
        max_healthpoints_cost = gameData.max_healthpoints_cost;
        total_score = gameData.total_score;
    }

    private void Save(GameData gameData)
    {
        gameData.max_healthpoints = max_healthpoints;
        gameData.max_healthpoints_cost = max_healthpoints_cost;
        gameData.total_score = total_score;

        SaveSystem.Save(gameData);
    }
}
