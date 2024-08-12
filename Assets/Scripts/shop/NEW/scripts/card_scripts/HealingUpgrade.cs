using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Upgrade/Healing")]
public class HealingUpgrade : Upgrades, IUpgradeable
{
    private int healing;
    private int healing_cost;
    private int total_score;

    public void Load(GameData gameData)
    {
        healing = gameData.healing;
        healing_cost = gameData.healing_cost;
        total_score = gameData.total_score;
        SetValuesForVisuals(healing, healing_cost);
    }

    public void Upgrade(GameData gameData)
    {
        if (total_score >= healing_cost)
        {
            if (healing + upgrade_value <= max_upgrade_stat)
            {
                total_score -= healing_cost;
                healing_cost = (int)(healing_cost * cost_multiplier);
                healing += (int)upgrade_value;
            }
            SetValuesForVisuals(healing, healing_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Save(GameData gameData)
    {
        gameData.healing = healing;
        gameData.healing_cost = healing_cost;
        gameData.total_score = total_score;
        SaveSystem.Save(gameData);
    }
}