using System;

[Serializable]
public class GameData
{
    //Player data
    public int maxHealthpoints;
    public int totalScore;

    //Item data
    public float dashCooldown;
    public int healing;
    public float shootingCooldown;

    //Item costs
    public int healthpointsCost;
    public int dashCost;
    public int healingCost;
    public int shootingCost;

    public GameData(int maxHealthpoints, int totalScore, float dashCooldown, int healing, float shootingCooldown, int healthpointsCost, int dashCost, int healingCost, int shootingCost)
    {
        this.maxHealthpoints = maxHealthpoints;
        this.totalScore = totalScore;
        this.dashCooldown = dashCooldown;
        this.healing = healing;
        this.shootingCooldown = shootingCooldown;
        this.healthpointsCost = healthpointsCost;
        this.dashCost = dashCost;
        this.healingCost = healingCost;
        this.shootingCost = shootingCost;
    }
}