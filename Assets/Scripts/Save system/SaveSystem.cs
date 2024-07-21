using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // player data default values
    private static int maxHealthpoints = 10;
    private static int totalScore = 0;

    // item data default values
    private static float dashCooldown = 1;
    private static int healing = 5;
    private static float shootingCooldown = 0.5f;

    // item costs default values 
    private static int healthpointsCost = 100;
    private static int dashCost = 100;
    private static int healingCost = 100;
    private static int shootingCost = 100;

    // path
    private static string path = Application.persistentDataPath + "/saveFile.sv";

    public static void Save(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(gameData.maxHealthpoints, gameData.totalScore, gameData.dashCooldown, gameData.healing, gameData.shootingCooldown, gameData.healthpointsCost, gameData.dashCost, gameData.healingCost, gameData.shootingCost);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Save file at" + path);
    }

    public static GameData Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            GameData data = (GameData)formatter.Deserialize(stream);
            stream.Close();

            Debug.Log("Load file at" + path);
            return data;
        }
        else
        {
            GameData data = new GameData(maxHealthpoints, totalScore, dashCooldown, healing, shootingCooldown, healthpointsCost, dashCost, healingCost, shootingCost);
            Save(data);
            return Load();
        }
    }
}