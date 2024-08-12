using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Upgrade/Damage")]
public class DamageUpgrade : Upgrades
{
    private int damage;
    private int damage_cost;
    private int total_score;

    public void Upgrade(GameData gameData)
    {
        Load(gameData);

        if (total_score >= damage_cost)
        {
            if (damage + upgrade_value <= max_upgrade_stat)
            {
                total_score -= damage_cost;
                damage_cost = (int)(damage_cost * cost_multiplier);
                damage += (int)upgrade_value;
            }

            SetValuesForVisuals(damage, damage_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Load(GameData gameData)
    {
        damage = gameData.damge;
        damage_cost = gameData.damge_cost;
        total_score = gameData.total_score;
    }

    private void Save(GameData gameData)
    {
        gameData.damge = damage;
        gameData.damge_cost = damage_cost;
        gameData.total_score = total_score;

        SaveSystem.Save(gameData);
    }
}
