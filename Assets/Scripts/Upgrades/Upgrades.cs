using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    [Header("GUI components")]
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI maxHealthpointsText, maxHealthpointsCostText;
    public TextMeshProUGUI dashCooldownText, dashCooldownCostText;
    public TextMeshProUGUI healingText, healingCostText;
    public TextMeshProUGUI shootingCooldownText, shootingCooldownCostText;
    public TextMeshProUGUI multiplierText, multiplierCostText;

    [Header("Current stats")]
    private int totalScore;
    private int level;
    private int maxHealthpoints;
    private float dashCooldown;
    private int healing;
    private float shootingCooldown;
    private int multiplier;

    [Header("Upgrade values")]
    private int increaseMaxHealthpoints = 10;
    private float reduceDashCooldown = 0.1f;
    private int increaseHealingAmount = 1;
    private float reduceShootingCooldown = 0.1f;
    private int increaseMultiplier = 2;
    private int maxHealthpointsValue = 100;
    private float minDashCooldownValue = 0;
    private int maxHealingValue = 50;
    private float minShootingCooldownValue = 0;
    private int maxMultiplierValue = 8;

    [Header("Upgrade costs")]
    private float increaseCostForUpgrades = 1.5f;
    private int maxHealthpointsCost;
    private int dashCooldownCost;
    private int healingCost;
    private int shootingCooldownCost;
    private int multiplierCost;

    void Start()
    {
        Load();
        UpdateText();
    }

    public void Load()
    {
        GameData gameData = SaveSystem.Load();

        maxHealthpoints = gameData.maxHealthpoints;
        totalScore = gameData.totalScore;
        level = gameData.level;

        dashCooldown = gameData.dashCooldown;
        healing = gameData.healing;
        shootingCooldown = gameData.shootingCooldown;
        multiplier = gameData.multiplier;

        maxHealthpointsCost = gameData.maxHealthpointsCost;
        dashCooldownCost = gameData.dashCooldownCost;
        healingCost = gameData.healingCost;
        shootingCooldownCost = gameData.shootingCooldownCost;
        multiplierCost = gameData.multiplierCost;
    }

    public void Save()
    {
        SaveSystem.Save(new GameData(maxHealthpoints, totalScore, level, dashCooldown, healing, shootingCooldown, multiplier, maxHealthpointsCost, dashCooldownCost, healingCost, shootingCooldownCost, multiplierCost));
    }

    public int increaseCost(float cost)
    {
        return (int)(cost * increaseCostForUpgrades);
    }

    public void LevelUpHealthpoints()
    {
        if (totalScore >= maxHealthpointsCost && maxHealthpoints + increaseMaxHealthpoints <= maxHealthpointsValue)
        {
            maxHealthpoints += increaseMaxHealthpoints;
            LevelUp(maxHealthpointsCost);
            maxHealthpointsCost = increaseCost(maxHealthpointsCost);
            UpdateText();
            Save();
        }
    }

    public void LevelUpDashCooldown()
    {
        if (totalScore >= dashCooldownCost && dashCooldown - reduceDashCooldown >= minDashCooldownValue)
        {
            dashCooldown -= reduceDashCooldown;
            LevelUp(dashCooldownCost);
            dashCooldownCost = increaseCost(dashCooldownCost);
            UpdateText();
            Save();
        }
        else if (totalScore >= dashCooldownCost && dashCooldown - reduceDashCooldown != minDashCooldownValue - reduceDashCooldown)
        {
            dashCooldown = 0;
            LevelUp(dashCooldownCost);
        }
    }

    public void LevelUpHealing()
    {
        if (totalScore >= healingCost && healing + increaseHealingAmount <= maxHealingValue)
        {
            healing += increaseHealingAmount;
            LevelUp(healingCost);
            healingCost = increaseCost(healingCost);
            UpdateText();
            Save();
        }
    }

    public void LevelUpShootingCooldown()
    {
        if (totalScore >= shootingCooldownCost && shootingCooldown - reduceShootingCooldown >= minShootingCooldownValue)
        {
            shootingCooldown -= reduceShootingCooldown;
            LevelUp(shootingCooldownCost);
            shootingCooldownCost = increaseCost(shootingCooldownCost);
            UpdateText();
            Save();
        }
        else if (totalScore >= shootingCooldownCost && shootingCooldown - reduceShootingCooldown != minShootingCooldownValue - reduceShootingCooldown)
        {
            shootingCooldown = 0;
            LevelUp(shootingCooldownCost);
        }
    }

    public void LevelUpMultiplier()
    {
        if (totalScore >= multiplierCost && multiplier + increaseMultiplier <= maxMultiplierValue)
        {
            multiplier *= increaseMultiplier;
            LevelUp(multiplierCost);
            multiplierCost = increaseCost(multiplierCost);
            UpdateText();
            Save();
        }
    }

    private void LevelUp(int cost)
    {
        totalScore -= cost;
        level++;
        UpdateText();
        Save();
    }

    public void UpdateText()
    {
        totalScoreText.text = "Score points: " + totalScore.ToString();
        levelText.text = "Level: " + level.ToString();

        maxHealthpointsText.text = "Healthpoints: " + maxHealthpoints.ToString();
        maxHealthpointsCostText.text = maxHealthpoints >= maxHealthpointsValue ? "MAX" : "Cost: " + maxHealthpointsCost.ToString();

        dashCooldownText.text = "Dash cooldown: " + dashCooldown.ToString("F2");
        dashCooldownCostText.text = dashCooldown <= minDashCooldownValue ? "MAX" : "Cost: " + dashCooldownCost.ToString();

        healingText.text = "Healing: " + healing.ToString();
        healingCostText.text = healing >= maxHealingValue ? "MAX" : "Cost: " + healingCost.ToString();

        shootingCooldownText.text = "Shooting cooldown: " + shootingCooldown.ToString("F2");
        shootingCooldownCostText.text = shootingCooldown <= minShootingCooldownValue ? "MAX" : "Cost: " + shootingCooldownCost.ToString();

        multiplierText.text = "Multiplier: " + multiplier.ToString();
        multiplierCostText.text = multiplier >= maxMultiplierValue ? "MAX" : "Cost: " + multiplierCost.ToString();
    }

    public void LoadGameModes()
    {
        SceneManager.LoadScene(1);
    }
}
