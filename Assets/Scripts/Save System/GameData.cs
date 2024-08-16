using System;

[Serializable]
public class GameData
{
    //* player stats
    public int max_healthpoints;
    public int total_score;
    public int level;
    public int damge;

    //* weapon stats
    public string weapon_prefab_path;
    public bool bomb_unlocked;
    public bool dual_shot_unlocked;
    public bool trippl_shot_unlocked;
    public bool sniper_unlocked;
    public bool round_shot_unlocked;

    //* ability stats
    public string ability_scriptableobject_path;
    public bool boss_dash_unlocked;

    //* item stats
    public float dash_cooldown;
    public int healing;
    public float shot_cooldown;
    public int multiplier;

    //* item costs
    public int max_healthpoints_cost;
    public int damge_cost;
    public int dash_cooldown_cost;
    public int healing_cost;
    public int shot_cooldown_cost;
    public int multiplier_cost;

    public GameData(
        int max_healthpoints,
        int total_score,
        int level,
        int damge,

        string weapon_prefab_path,
        bool bomb_unlocked,
        bool dual_shot_unlocked,
        bool trippl_shot_unlocked,
        bool sniper_unlocked,
        bool round_shot_unlocked,

        string ability_scriptableobject_path,
        bool boss_dash_unlocked,

        float dash_cooldown,
        int healing,
        float shot_cooldown,
        int multiplier,

        int max_healthpoints_cost,
        int damge_cost,
        int dash_cooldown_cost,
        int healing_cost,
        int shot_cooldown_cost,
        int multiplier_cost)
    {
        this.max_healthpoints = max_healthpoints;
        this.total_score = total_score;
        this.level = level;
        this.damge = damge;

        this.weapon_prefab_path = weapon_prefab_path;
        this.bomb_unlocked = bomb_unlocked;
        this.dual_shot_unlocked = dual_shot_unlocked;
        this.trippl_shot_unlocked = trippl_shot_unlocked;
        this.sniper_unlocked = sniper_unlocked;
        this.round_shot_unlocked = round_shot_unlocked;

        this.ability_scriptableobject_path = ability_scriptableobject_path;
        this.boss_dash_unlocked = boss_dash_unlocked;

        this.dash_cooldown = dash_cooldown;
        this.healing = healing;
        this.shot_cooldown = shot_cooldown;
        this.multiplier = multiplier;

        this.max_healthpoints_cost = max_healthpoints_cost;
        this.damge_cost = damge_cost;
        this.dash_cooldown_cost = dash_cooldown_cost;
        this.healing_cost = healing_cost;
        this.shot_cooldown_cost = shot_cooldown_cost;
        this.multiplier_cost = multiplier_cost;
    }

    public GameData(GameData game_data)
    {
        max_healthpoints = game_data.max_healthpoints;
        total_score = game_data.total_score;
        level = game_data.level;
        damge = game_data.damge;

        weapon_prefab_path = game_data.weapon_prefab_path;
        bomb_unlocked = game_data.bomb_unlocked;
        dual_shot_unlocked = game_data.dual_shot_unlocked;
        trippl_shot_unlocked = game_data.trippl_shot_unlocked;
        sniper_unlocked = game_data.sniper_unlocked;
        round_shot_unlocked = game_data.round_shot_unlocked;

        ability_scriptableobject_path = game_data.ability_scriptableobject_path;
        boss_dash_unlocked = game_data.boss_dash_unlocked;

        dash_cooldown = game_data.dash_cooldown;
        healing = game_data.healing;
        shot_cooldown = game_data.shot_cooldown;
        multiplier = game_data.multiplier;

        max_healthpoints_cost = game_data.max_healthpoints_cost;
        damge_cost = game_data.damge_cost;
        dash_cooldown_cost = game_data.dash_cooldown_cost;
        healing_cost = game_data.healing_cost;
        shot_cooldown_cost = game_data.shot_cooldown_cost;
        multiplier_cost = game_data.multiplier_cost;
    }
}