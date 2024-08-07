using UnityEngine;
[CreateAssetMenu(fileName = "_card", menuName = "Weapon Card")]
public class WeaponCard : ScriptableObject
{
    public enum WEAPON_TYPES
    {
        Bomb,
        Dual_Shot,
        Tripple_Shot,
        Sniper,
    }

    [SerializeField] WEAPON_TYPES _weapon_type;
    [SerializeField] string weapon_prefab_path;
    [SerializeField] public string card_name;
    [SerializeField] public int cost;
    [HideInInspector] public bool is_unlocked;

    public void LoadDataOnCard(GameData game_data)
    {
        if (game_data == null)
        {
            Debug.LogWarning("null");
        }
        switch (_weapon_type)
        {
            case WEAPON_TYPES.Bomb:
                is_unlocked = game_data.bomb_unlocked;
                break;
            case WEAPON_TYPES.Dual_Shot:
                is_unlocked = game_data.dual_shot_unlocked;
                break;
            case WEAPON_TYPES.Tripple_Shot:
                is_unlocked = game_data.trippl_shot_unlocked;
                break;
            case WEAPON_TYPES.Sniper:
                is_unlocked = game_data.sniper_unlocked;
                break;
            default:
                Debug.LogWarning("Upgrade name not recognized.");
                break;
        }
    }

    public void Unlock(GameData game_data)
    {

        if (game_data.total_score >= cost)
        {
            bool upgrade_applied = false;

            switch (_weapon_type)
            {
                case WEAPON_TYPES.Bomb:
                    ApplyUnlock(game_data);
                    game_data.bomb_unlocked = is_unlocked;
                    upgrade_applied = true;
                    break;
                case WEAPON_TYPES.Dual_Shot:
                    ApplyUnlock(game_data);
                    game_data.dual_shot_unlocked = is_unlocked;
                    upgrade_applied = true;
                    break;
                case WEAPON_TYPES.Tripple_Shot:
                    ApplyUnlock(game_data);
                    game_data.trippl_shot_unlocked = is_unlocked;
                    upgrade_applied = true;
                    break;
                case WEAPON_TYPES.Sniper:
                    ApplyUnlock(game_data);
                    game_data.sniper_unlocked = is_unlocked;
                    upgrade_applied = true;
                    break;
                default:
                    Debug.LogWarning("Upgrade name not recognized.");
                    break;
            }

            if (upgrade_applied)
            {
                Save(game_data);
            }
        }
    }

    public void Equip()
    {
        GameData game_data = SaveSystem.Load();
        game_data.weapon_prefab = weapon_prefab_path;
        Save(game_data);
    }

    private void ApplyUnlock(GameData game_data)
    {
        if (!is_unlocked)
        {
            is_unlocked = true;
            game_data.weapon_prefab = weapon_prefab_path;
            game_data.total_score -= cost;
        }
    }

    private void Save(GameData game_data)
    {
        SaveSystem.Save(game_data);
    }
}