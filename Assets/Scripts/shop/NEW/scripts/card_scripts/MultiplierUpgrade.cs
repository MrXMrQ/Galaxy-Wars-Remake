using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Upgrade/Multiplier")]
public class MultiplierUpgrade : Upgrades, IUpgradeable
{
    private int multiplier;
    private int multiplier_cost;
    private int total_score;

    public void Load(GameData gameData)
    {
        multiplier = gameData.multiplier;
        multiplier_cost = gameData.multiplier_cost;
        total_score = gameData.total_score;
        SetValuesForVisuals(multiplier, multiplier_cost);
    }

    public void Upgrade(GameData gameData)
    {
        Load(gameData);

        if (total_score >= multiplier_cost)
        {
            if (multiplier * upgrade_value <= max_upgrade_stat)
            {
                total_score -= multiplier_cost;
                multiplier_cost = (int)(multiplier_cost * cost_multiplier);
                multiplier *= (int)upgrade_value;
            }
            SetValuesForVisuals(multiplier, multiplier_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Save(GameData gameData)
    {
        gameData.multiplier = multiplier;
        gameData.multiplier_cost = multiplier_cost;
        gameData.total_score = total_score;
        SaveSystem.Save(gameData);
    }
}