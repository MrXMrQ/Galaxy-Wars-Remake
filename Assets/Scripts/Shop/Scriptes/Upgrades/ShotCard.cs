using UnityEngine;

[CreateAssetMenu(fileName = "_upgrade", menuName = "Upgrade/Shot")]
public class ShotCard : Card, IUpgradeable
{
    float shot_cooldown;
    int shot_cost;
    int total_score;

    public void Load(GameData gameData)
    {
        shot_cooldown = gameData.shot_cooldown;
        shot_cost = gameData.shot_cooldown_cost;
        total_score = gameData.total_score;
        SetUpgradeValuesVisuals(shot_cooldown, shot_cost);
    }

    public void Upgrade(GameData gameData)
    {
        Load(gameData);

        if (total_score >= shot_cost)
        {
            if (shot_cooldown - upgrade_value >= max_upgrade_stat)
            {
                total_score -= shot_cost;
                shot_cost = (int)(shot_cost * cost_multiplier);
                shot_cooldown -= upgrade_value;
            }
            else if (shot_cooldown - upgrade_value != max_upgrade_stat - upgrade_value)
            {
                total_score -= shot_cost;
                shot_cooldown = 0;
            }
            SetUpgradeValuesVisuals(shot_cooldown, shot_cost);
            Save(gameData);
        }
        else
        {
            Debug.LogWarning("not enought scroe points");
        }
    }

    private void Save(GameData gameData)
    {
        gameData.shot_cooldown = shot_cooldown;
        gameData.shot_cooldown_cost = shot_cost;
        gameData.total_score = total_score;

        SaveSystem.Save(gameData);
    }
}