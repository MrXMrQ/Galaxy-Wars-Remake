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
    [SerializeField] public bool is_unlocked;


    public void LoadDataOnCard(GameData game_data)
    {
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

    //TODO: diffrent weapons to unlock

    public void Unlock(int total_score)
    {
        GameData game_data = SaveSystem.Load();
        if (total_score >= cost)
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
