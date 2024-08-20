using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    // PLAYER STATS
    public int total_score;
    public int level;

    // ITEM STATS
    public Dictionary<string, float> item_stats = new Dictionary<string, float>();
    public Dictionary<string, int> item_cost = new Dictionary<string, int>();

    // WEAPON STATS
    public string weapon_path;
    public Dictionary<string, bool> weapons = new Dictionary<string, bool>();

    // ABILITY STATS
    public string ability_path;
    public Dictionary<string, bool> abilities = new Dictionary<string, bool>();

    public GameData(
        int total_score,
        int level,
        Dictionary<string, float> item_stats,
        Dictionary<string, int> item_cost,
        string weapon_path,
        Dictionary<string, bool> weapons,
        string ability_path,
        Dictionary<string, bool> abilities
        )
    {
        this.total_score = total_score;
        this.level = level;
        this.item_stats = item_stats;
        this.item_cost = item_cost;
        this.weapon_path = weapon_path;
        this.weapons = weapons;
        this.ability_path = ability_path;
        this.abilities = abilities;
    }

    public GameData(GameData game_data)
    {
        total_score = game_data.total_score;
        level = game_data.level;
        item_stats = game_data.item_stats;
        item_cost = game_data.item_cost;
        weapon_path = game_data.weapon_path;
        weapons = game_data.weapons;
        ability_path = game_data.ability_path;
        abilities = game_data.abilities;
    }
}