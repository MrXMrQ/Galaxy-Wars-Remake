using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // player data default values
    private static int maxHealthpoints = 10;
    private static int totalScore = 999999;
    private static int level = 0;

    // item data default values
    private static float dashCooldown = 0.8f;
    private static int healing = 1;
    private static float shootingCooldown = 0.25f;
    private static int multiplier = 1;

    // item costs default values 
    private static int healthpointsCost = 3000;
    private static int dashCost = 500;
    private static int healingCost = 750;
    private static int shootingCost = 2000;
    private static int multiplierCost = 10000;

    // path
    private static string path = Application.persistentDataPath + "/saveFile.sv";

    public static void Save(GameData gameData)
    {
        Debug.Log("File load at: " + path);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameData.maxHealthpoints, gameData.totalScore, gameData.level, gameData.dashCooldown, gameData.healing, gameData.shootingCooldown, gameData.multiplier, gameData.maxHealthpointsCost, gameData.dashCooldownCost, gameData.healingCost, gameData.shootingCooldownCost, gameData.multiplierCost);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = (GameData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            GameData data = new GameData(maxHealthpoints, totalScore, level, dashCooldown, healing, shootingCooldown, multiplier, healthpointsCost, dashCost, healingCost, shootingCost, multiplierCost);
            Save(data);
            return Load();
        }
    }
}