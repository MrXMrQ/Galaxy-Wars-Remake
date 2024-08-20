using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "_ability", menuName = "Card/Ability")]
public class AbilityCard : Card
{
    public enum KEYS
    {
        Default,
        boss_dash,
        turret,
        clone,
    }

    [Header("ABILITY")]
    [SerializeField] KEYS keys;
    [SerializeField] public string ABILITY_PATH;
    [SerializeField] public int COST;
    [HideInInspector] public Outline outline;
    [HideInInspector] public bool is_unlocked;
    [HideInInspector] string key;
    int total_score;
    string current_path;

    public void Load(GameData game_data)
    {
        key = keys.ToString();

        current_path = game_data.ability_path;
        is_unlocked = game_data.abilities[key];
        total_score = game_data.total_score;
    }

    public void Unlock(GameData game_data)
    {
        if (total_score >= COST && !game_data.abilities[key])
        {
            current_path = ABILITY_PATH;
            total_score -= COST;
            is_unlocked = true;

            Save(game_data);
        }
    }

    public void Equip(GameData game_data)
    {
        if (game_data.abilities[key] && !game_data.ability_path.Equals(ABILITY_PATH))
        {
            current_path = ABILITY_PATH;
            Save(game_data);
        }
    }

    public void Save(GameData game_data)
    {
        game_data.ability_path = current_path;
        game_data.abilities[key] = is_unlocked;
        game_data.total_score = total_score;

        SaveSystem.Save(game_data);
    }
}