using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Upgrade/Damage")]
public class DamageCard : Card, IUpgradeable
{
    int damage;
    int damage_cost;
    int total_score;

    public void Load(GameData gameData)
    {
        damage = gameData.damge;
        damage_cost = gameData.damge_cost;
        total_score = gameData.total_score;
        SetUpgradeValuesVisuals(damage, damage_cost);
    }

    public void Upgrade(GameData gameData)
    {
        if (total_score >= damage_cost)
        {
            if (damage + upgrade_value <= max_upgrade_stat)
            {
                total_score -= damage_cost;
                damage_cost = (int)(damage_cost * cost_multiplier);
                damage += (int)upgrade_value;
            }

            SetUpgradeValuesVisuals(damage, damage_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Save(GameData gameData)
    {
        gameData.damge = damage;
        gameData.damge_cost = damage_cost;
        gameData.total_score = total_score;
        SaveSystem.Save(gameData);
    }
}