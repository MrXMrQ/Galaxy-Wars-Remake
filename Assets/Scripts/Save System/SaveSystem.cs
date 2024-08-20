using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    // PLAYER DEFAULTS
    static int TOTAL_SCORE = 1000000;
    static int LEVEL = 0;

    // ITEM DEFAULTS
    static Dictionary<string, float> item_stats = new Dictionary<string, float>();
    static Dictionary<string, int> item_cost = new Dictionary<string, int>();
    public static float DASH_COOLDOWN = 0.8f;
    public static float SHOT_COOLDOWN = 0.25f;

    // WEAPON DEFAULTS
    static string WEAPON_PREFAB_PATH = "Player/Projectiles/default";
    static Dictionary<string, bool> unlocked_weapons = new Dictionary<string, bool>();

    // ABILITY DEFAULTS
    static string ABILITY_PREFAB_PATH = "Player/Abilities/default";
    static Dictionary<string, bool> unlocked_abilities = new Dictionary<string, bool>();

    // PATH
    static string PATH = Application.persistentDataPath + "/saveFile.sv"; //ALERT: C:/Users/username/AppData/LocalLow/Galaxy-Wars-Remake/Galaxy-Wars-Remake/saveFile.sv on Windows

    public static void Save(GameData game_data)
    {
        Debug.Log("File save at: " + PATH);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(PATH, FileMode.Create);

        GameData data = new GameData(game_data);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData Load()
    {
        if (File.Exists(PATH))
        {
            Debug.Log("File load at: " + PATH);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(PATH, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("File created at: " + PATH);

            Add(); // Add items, weapons and abilities to the dictionarys

            GameData data = new GameData
            (
            TOTAL_SCORE,
            LEVEL,
            item_stats,
            item_cost,
            WEAPON_PREFAB_PATH,
            unlocked_weapons,
            ABILITY_PREFAB_PATH,
            unlocked_abilities
            );

            Save(data);
            return Load();
        }
    }

    public static void Add()
    {
        item_stats.Add("healthpoints", 10);
        item_stats.Add("healing", 1);
        item_stats.Add("dash_cooldown", 0.8f);
        item_stats.Add("shot_cooldown", 0.25f);
        item_stats.Add("damage", 1);
        item_stats.Add("multiplier", 1);

        item_cost.Add("healthpoints", 1000);
        item_cost.Add("healing", 2000);
        item_cost.Add("dash_cooldown", 3000);
        item_cost.Add("shot_cooldown", 4000);
        item_cost.Add("damage", 5000);
        item_cost.Add("multiplier", 6000);

        unlocked_weapons.Add("Default", true);
        unlocked_weapons.Add("bomb", false);
        unlocked_weapons.Add("sniper", false);
        unlocked_weapons.Add("round_shot", false);
        unlocked_weapons.Add("dual_shot", false);
        unlocked_weapons.Add("tripple_shot", false);

        unlocked_abilities.Add("Default", true);
        unlocked_abilities.Add("boss_dash", false);
        unlocked_abilities.Add("turret", false);
        unlocked_abilities.Add("clone", false);
    }
}