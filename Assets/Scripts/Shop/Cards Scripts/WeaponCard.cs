using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "_weapon", menuName = "Card/Weapon")]
public class WeaponCard : Card
{
    public enum KEYS
    {
        Default,
        bomb,
        sniper,
        round_shot,
        dual_shot,
        tripple_shot,
    }

    [Header("WEAPON")]
    [SerializeField] KEYS keys;
    [SerializeField] public string WEAPON_PATH;
    [SerializeField] public int COST;
    [HideInInspector] public Outline outline;
    [HideInInspector] public bool is_unlocked;
    [HideInInspector] string key;
    int total_score;
    string current_weapon_path;

    public void Load(GameData game_data)
    {
        key = keys.ToString();

        current_weapon_path = game_data.weapon_path;
        is_unlocked = game_data.weapons[key];
        total_score = game_data.total_score;
    }

    public void Unlock(GameData game_data)
    {
        if (total_score >= COST && !game_data.weapons[key])
        {
            current_weapon_path = WEAPON_PATH;
            total_score -= COST;
            is_unlocked = true;

            Save(game_data);
        }
    }

    public void Equip(GameData game_data)
    {
        if (game_data.weapons[key] && !game_data.weapon_path.Equals(WEAPON_PATH))
        {
            current_weapon_path = WEAPON_PATH;
            Save(game_data);
        }
    }

    public void Save(GameData game_data)
    {
        game_data.weapon_path = current_weapon_path;
        game_data.weapons[key] = is_unlocked;
        game_data.total_score = total_score;

        SaveSystem.Save(game_data);
    }
}