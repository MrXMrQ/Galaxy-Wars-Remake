using TMPro;
using UnityEngine;

public abstract class Upgrades : ScriptableObject
{
    public enum UPGRADES_TYPES
    {
        Healthpoints,
        Damage,
        Dash,
        Healing,
        Shot,
        Multiplier
    }

    [SerializeField] public UPGRADES_TYPES upgrade_type;
    [SerializeField] public string card_name;
    [SerializeField] protected float cost_multiplier;
    [SerializeField] public float upgrade_value;
    [SerializeField] public float max_upgrade_stat;
    [HideInInspector] public float current_stat;
    [HideInInspector] public int cost;

    protected void SetValuesForVisuals(float current_stat, int cost)
    {
        this.current_stat = current_stat;
        this.cost = cost;
    }
}
