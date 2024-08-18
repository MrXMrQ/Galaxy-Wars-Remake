using UnityEngine;

[CreateAssetMenu(fileName = "_ability", menuName = "Card/Ability/Clone")]
public class CloneAbility : Card, IAbility
{
    int total_score;
    string current_path;

    public void Load(GameData game_data)
    {
        is_unlocked_ability = game_data.clone_unlocked;
        current_path = game_data.ability_scriptableobject_path;
        total_score = game_data.total_score;
    }

    public void Unlock(GameData game_data)
    {
        if (total_score >= ability_cost && !is_unlocked_ability)
        {
            total_score -= ability_cost;
            current_path = ability_scriptableobject_path;
            is_unlocked_ability = true;

            Save(game_data);
        }
    }

    public void Equip(GameData game_data)
    {
        current_path = ability_scriptableobject_path;
        Save(game_data);
    }

    private void Save(GameData game_data)
    {
        game_data.clone_unlocked = is_unlocked_ability;
        game_data.total_score = total_score;
        game_data.ability_scriptableobject_path = current_path;
        SaveSystem.Save(game_data);
    }
}