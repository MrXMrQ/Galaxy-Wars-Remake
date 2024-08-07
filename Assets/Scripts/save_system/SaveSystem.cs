using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //* player default stats
    static int MAX_HEALTHPOINTS = 10;
    static int TOTAL_SCORE = 1000000;
    static int LEVEL = 0;
    static int DAMAGE = 1;

    //* weapon default stats
    static string WEAPON_PREFAB_PATH = "prefabs/player_projectiles/player_projectile_default";
    static bool BOMB_UNLOCKED;
    static bool DUAL_SHOT_UNLOCKED;
    static bool TRIPPLE_SHOT_UNLOCKED;
    static bool SNIPER_UNLOCKED;

    //* item default stats
    public static float DASH_COOLDOWN = 0.8f;
    static int HEALING = 1;
    public static float SHOT_COOLDOWN = 0.25f;
    static int MULTIPLIER = 1;

    //* item default costs 
    static int HEALTHPOINTS_COST = 3000;
    static int DAMAGE_COST = 1000;
    static int DASH_COOLDOWN_COST = 500;
    static int HEALING_COST = 750;
    static int SHOT_COOLDOWN_COST = 2000;
    static int MULTIPLIER_COST = 10000;

    //! C:/Users/username/AppData/LocalLow/Galaxy-Wars-Remake/Galaxy-Wars-Remake/saveFile.sv on Windows
    static string PATH = Application.persistentDataPath + "/saveFile.sv";

    public static void Save(GameData gameData)
    {
        Debug.Log("File save at: " + PATH);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(PATH, FileMode.Create);

        ////GameData data = new GameData(gameData.maxHealthpoints, gameData.totalScore, gameData.level, gameData.dashCooldown, gameData.healing, gameData.shootingCooldown, gameData.multiplier, gameData.maxHealthpointsCost, gameData.dashCooldownCost, gameData.healingCost, gameData.shootingCooldownCost, gameData.multiplierCost);

        GameData data = new GameData(gameData);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData Load()
    {
        if (File.Exists(PATH))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(PATH, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            GameData data = new GameData
            (MAX_HEALTHPOINTS,
            TOTAL_SCORE,
            LEVEL,
            DAMAGE,
            WEAPON_PREFAB_PATH,
            BOMB_UNLOCKED,
            DUAL_SHOT_UNLOCKED,
            TRIPPLE_SHOT_UNLOCKED,
            SNIPER_UNLOCKED,
            DASH_COOLDOWN,
            HEALING,
            SHOT_COOLDOWN,
            MULTIPLIER,
            HEALTHPOINTS_COST,
            DAMAGE_COST,
            DASH_COOLDOWN_COST,
            HEALING_COST,
            SHOT_COOLDOWN_COST,
            MULTIPLIER_COST);

            Save(data);
            return Load();
        }
    }
}