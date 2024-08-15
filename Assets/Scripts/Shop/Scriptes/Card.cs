using UnityEngine;

public abstract class Card : ScriptableObject
{
    public enum UPGRADES_TYPES
    {
        Healthpoints,
        Damage,
        Dash,
        Healing,
        Shot,
        Multiplier,
        none
    }

    [Header("GENERAL")]
    [SerializeField] public string card_name;

    [Header("UPGRADE")]
    [SerializeField] public UPGRADES_TYPES upgrade_type;
    [SerializeField] protected float cost_multiplier;
    [SerializeField] public float upgrade_value;
    [SerializeField] public float max_upgrade_stat;
    [HideInInspector] public float current_stat;
    [HideInInspector] public int upgrade_cost;

    [Header("WEAPON")]
    [SerializeField] public string weapon_prefab_path;
    [SerializeField] public int weapon_cost;
    [SerializeField] public bool is_unlocked_weapon;

    [Header("ABILITIES")]
    [SerializeField] public string ability_scriptableobject_path;
    [SerializeField] public int ability_cost;
    [SerializeField] public bool is_unlocked_ability;


    protected void SetUpgradeValuesVisuals(float current_stat, int upgrade_cost)
    {
        this.current_stat = current_stat;
        this.upgrade_cost = upgrade_cost;
    }
}