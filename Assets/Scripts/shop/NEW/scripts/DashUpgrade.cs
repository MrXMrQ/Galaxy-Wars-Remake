using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Upgrade/Dash")]
public class DashUpgrade : Upgrades
{
    private float dash_cooldown;
    private int dash_cost;
    private int total_score;

    public void Upgrade(GameData gameData)
    {
        Load(gameData);

        if (total_score >= dash_cost)
        {
            if (dash_cooldown - upgrade_value >= max_upgrade_stat)
            {
                total_score -= dash_cost;
                dash_cost = (int)(dash_cost * cost_multiplier);
                dash_cooldown -= upgrade_value;
            }
            else if (dash_cooldown - upgrade_value != max_upgrade_stat - upgrade_value)
            {
                total_score -= dash_cost;
                dash_cooldown = 0;
            }
            SetValuesForVisuals(dash_cooldown, dash_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Load(GameData gameData)
    {
        dash_cooldown = gameData.dash_cooldown;
        dash_cost = gameData.dash_cooldown_cost;
        total_score = gameData.total_score;
    }

    private void Save(GameData gameData)
    {
        gameData.dash_cooldown = dash_cooldown;
        gameData.dash_cooldown_cost = dash_cost;
        gameData.total_score = total_score;

        SaveSystem.Save(gameData);
    }
}