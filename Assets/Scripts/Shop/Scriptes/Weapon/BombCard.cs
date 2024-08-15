using UnityEngine;

[CreateAssetMenu(fileName = "_weapon", menuName = "Card/Weapon/Bomb")]
public class BombCard : Card, IWeapon
{
    int total_score;
    string current_path;

    public void Load(GameData game_data)
    {
        is_unlocked_weapon = game_data.bomb_unlocked;
        total_score = game_data.total_score;
        current_path = game_data.weapon_prefab_path;
    }

    public void Unlock(GameData game_data)
    {
        if (total_score >= weapon_cost && !is_unlocked_weapon)
        {
            total_score -= weapon_cost;
            current_path = weapon_prefab_path;
            is_unlocked_weapon = true;

            Save(game_data);
        }
        else
        {
            Debug.LogWarning("not enought scroe points or is unlocked");
        }
    }

    public void Equip(GameData gameData)
    {
        current_path = weapon_prefab_path;
        Save(gameData);
    }

    private void Save(GameData game_data)
    {
        game_data.bomb_unlocked = is_unlocked_weapon;
        game_data.total_score = total_score;
        game_data.weapon_prefab_path = current_path;
        SaveSystem.Save(game_data);
    }
}
