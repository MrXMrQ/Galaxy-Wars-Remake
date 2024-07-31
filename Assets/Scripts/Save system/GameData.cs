using System;

[Serializable]
public class GameData
{
    //Player data
    public int maxHealthpoints;
    public int totalScore;
    public int level;

    //Item data
    public float dashCooldown;
    public int healing;
    public float shootingCooldown;
    public int multiplier;

    //Item costs
    public int maxHealthpointsCost;
    public int dashCooldownCost;
    public int healingCost;
    public int shootingCooldownCost;
    public int multiplierCost;

    public GameData(int maxHealthpoints, int totalScore, int level, float dashCooldown, int healing, float shootingCooldown, int multiplier, int healthpointsCost, int dashCost, int healingCost, int shootingCost, int multiplierCost)
    {
        this.maxHealthpoints = maxHealthpoints;
        this.totalScore = totalScore;
        this.level = level;

        this.dashCooldown = dashCooldown;
        this.healing = healing;
        this.shootingCooldown = shootingCooldown;
        this.multiplier = multiplier;

        this.maxHealthpointsCost = healthpointsCost;
        this.dashCooldownCost = dashCost;
        this.healingCost = healingCost;
        this.shootingCooldownCost = shootingCost;
        this.multiplierCost = multiplierCost;
    }
}