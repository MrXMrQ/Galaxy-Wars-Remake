using UnityEngine;

[CreateAssetMenu(fileName = "_ability", menuName = "Card/Ability/Dash")]
public class DashAbility : Card, IAbility
{
    int total_score;
    string current_path;

    public void Load(GameData game_data)
    {
        is_unlocked_ability = true;
        current_path = game_data.ability_scriptableobject_path;
        total_score = game_data.total_score;
    }

    public void Unlock(GameData game_data)
    {
        if (total_score >= ability_cost)
        {
            total_score -= ability_cost;
            current_path = ability_scriptableobject_path;
            is_unlocked_ability = true;

            Save(game_data);
        }
    }

    public void Equip(GameData game_data)
    {
        current_path = weapon_prefab_path;
        Save(game_data);
    }

    private void Save(GameData game_data)
    {
        game_data.total_score = total_score;
        game_data.ability_scriptableobject_path = current_path;
        SaveSystem.Save(game_data);
    }
}