using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    [Header("GUI components")]
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI maxHealthpointsText, maxHealtpointsCostText;
    //public TextMeshProUGUI dashCooldownText, dashCooldownCostText;
    //public TextMeshProUGUI healingText, healingCostText;
    //public TextMeshProUGUI shootingCooldownText, shootingCooldownCostText;


    [Header("cost")]
    public static int maxHealthpointsCost;
    public static int dashCost;
    public static int healingCost;
    public static int shootingCost;

    private float score = PlayerController.totalScore, healthpoints = PlayerController.maxHealthpoints;

    // Start is called before the first frame update
    void Start()
    {
        Load();

        UpdateText();
    }

    public void Load()
    {
        GameData gameData = SaveSystem.Load();

        PlayerController.maxHealthpoints = gameData.maxHealthpoints;
        PlayerController.totalScore = gameData.totalScore;

        PlayerController.dashCooldown = gameData.dashCooldown;
        PlayerController.healing = gameData.healing;
        PlayerController.shootingCooldown = gameData.shootingCooldown;

        maxHealthpointsCost = gameData.healthpointsCost;
        dashCost = gameData.dashCost;
        healingCost = gameData.healingCost;
        shootingCost = gameData.shootingCost;
    }

    public void Save()
    {
        SaveSystem.Save(new GameData(PlayerController.maxHealthpoints, PlayerController.totalScore, PlayerController.dashCooldown, PlayerController.healing, PlayerController.shootingCooldown, 0, 0, 0, 0));
    }

    public void LevelUpHealthpoints()
    {
        if (PlayerController.totalScore >= maxHealthpointsCost)
        {
            PlayerController.maxHealthpoints += 10;
            PlayerController.totalScore -= maxHealthpointsCost;
            maxHealthpointsCost += 20;
        }

        UpdateText();
        Save();
    }

    public void LevelUpDashCooldown()
    {
        Save();
    }

    public void LevelUpHealing()
    {
        Save();
    }

    public void LevelUpShootingCooldown()
    {
        Save();
    }

    public void UpdateText()
    {
        maxHealthpointsText.text = "Healthpoints: " + PlayerController.maxHealthpoints.ToString();
        totalScoreText.text = "Score points " + PlayerController.totalScore.ToString();
        maxHealtpointsCostText.text = "Cost: " + maxHealthpointsCost.ToString();
    }

    public void LoadGameModes()
    {
        SceneManager.LoadScene(1);
    }
}