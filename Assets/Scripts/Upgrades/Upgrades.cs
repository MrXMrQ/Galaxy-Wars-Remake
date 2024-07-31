using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrades : MonoBehaviour
{
    [Header("GUI COMPONENTS")]
    [SerializeField] TextMeshProUGUI total_score_text;
    [SerializeField] TextMeshProUGUI level_text;
    [SerializeField] TextMeshProUGUI _max_healthpoints_text, _max_healthpoints_cost_text;
    [SerializeField] TextMeshProUGUI dash_cooldown_text, dash_cooldown_cost_text;
    [SerializeField] TextMeshProUGUI healing_text, healing_cost_text;
    [SerializeField] TextMeshProUGUI shot_cooldown_text, shot_cooldown_cost_text;
    [SerializeField] TextMeshProUGUI multiplier_text, multiplier_cost_text;

    [Header("OLD STATS")]
    int _total_score;
    int _level;
    int _max_healthpoints;
    float _dash_cooldown;
    int _healing;
    float _shot_cooldown;
    int _multiplier;

    [Header("UPGRADE VALUES")]
    int _increase_max_healthpoints = 10;
    float _reduce_dash_cooldown = 0.1f;
    int _increase_healing_amount = 1;
    float _reduce_shooting_cooldown = 0.1f;
    int increase_multiplier = 2;
    int _max_healthpoints_value = 100;
    float _min_dash_cooldown_value = 0;
    int _max_healing_value = 50;
    float _min_shooting_cooldown_value = 0;
    int _max_multiplier_value = 8;

    [Header("UPGRADE COST")]
    float increase_cost_for_upgrades = 1.5f;
    int _max_healthpoints_cost;
    int _dash_cooldown_cost;
    int healing_cost;
    int _shooting_cooldown_cost;
    int _multiplier_cost;

    void Start()
    {
        Load();
        UpdateText();
    }

    public void Load()
    {
        GameData gameData = SaveSystem.Load();

        _max_healthpoints = gameData.maxHealthpoints;
        _total_score = gameData.totalScore;
        _level = gameData.level;

        _dash_cooldown = gameData.dashCooldown;
        _healing = gameData.healing;
        _shot_cooldown = gameData.shootingCooldown;
        _multiplier = gameData.multiplier;

        _max_healthpoints_cost = gameData.maxHealthpointsCost;
        _dash_cooldown_cost = gameData.dashCooldownCost;
        healing_cost = gameData.healingCost;
        _shooting_cooldown_cost = gameData.shootingCooldownCost;
        _multiplier_cost = gameData.multiplierCost;
    }

    public void Save()
    {
        SaveSystem.Save(new GameData(_max_healthpoints, _total_score, _level, _dash_cooldown, _healing, _shot_cooldown, _multiplier, _max_healthpoints_cost, _dash_cooldown_cost, healing_cost, _shooting_cooldown_cost, _multiplier_cost));
    }

    public int increaseCost(float cost)
    {
        return (int)(cost * increase_cost_for_upgrades);
    }

    public void LevelUpHealthpoints()
    {
        if (_total_score >= _max_healthpoints_cost && _max_healthpoints + _increase_max_healthpoints <= _max_healthpoints_value)
        {
            _max_healthpoints += _increase_max_healthpoints;
            LevelUp(_max_healthpoints_cost);
            _max_healthpoints_cost = increaseCost(_max_healthpoints_cost);
            UpdateText();
            Save();
        }
    }

    public void LevelUpDashCooldown()
    {
        if (_total_score >= _dash_cooldown_cost && _dash_cooldown - _reduce_dash_cooldown >= _min_dash_cooldown_value)
        {
            _dash_cooldown -= _reduce_dash_cooldown;
            LevelUp(_dash_cooldown_cost);
            _dash_cooldown_cost = increaseCost(_dash_cooldown_cost);
            UpdateText();
            Save();
        }
        else if (_total_score >= _dash_cooldown_cost && _dash_cooldown - _reduce_dash_cooldown != _min_dash_cooldown_value - _reduce_dash_cooldown)
        {
            _dash_cooldown = 0;
            LevelUp(_dash_cooldown_cost);
        }
    }

    public void LevelUpHealing()
    {
        if (_total_score >= healing_cost && _healing + _increase_healing_amount <= _max_healing_value)
        {
            _healing += _increase_healing_amount;
            LevelUp(healing_cost);
            healing_cost = increaseCost(healing_cost);
            UpdateText();
            Save();
        }
    }

    public void LevelUpShootingCooldown()
    {
        if (_total_score >= _shooting_cooldown_cost && _shot_cooldown - _reduce_shooting_cooldown >= _min_shooting_cooldown_value)
        {
            _shot_cooldown -= _reduce_shooting_cooldown;
            LevelUp(_shooting_cooldown_cost);
            _shooting_cooldown_cost = increaseCost(_shooting_cooldown_cost);
            UpdateText();
            Save();
        }
        else if (_total_score >= _shooting_cooldown_cost && _shot_cooldown - _reduce_shooting_cooldown != _min_shooting_cooldown_value - _reduce_shooting_cooldown)
        {
            _shot_cooldown = 0;
            LevelUp(_shooting_cooldown_cost);
        }
    }

    public void LevelUpMultiplier()
    {
        if (_total_score >= _multiplier_cost && _multiplier + increase_multiplier <= _max_multiplier_value)
        {
            _multiplier *= increase_multiplier;
            LevelUp(_multiplier_cost);
            _multiplier_cost = increaseCost(_multiplier_cost);
            UpdateText();
            Save();
        }
    }

    private void LevelUp(int cost)
    {
        _total_score -= cost;
        _level++;
        UpdateText();
        Save();
    }

    public void UpdateText()
    {
        total_score_text.text = "Score points: " + _total_score.ToString();
        level_text.text = "Level: " + _level.ToString();

        _max_healthpoints_text.text = "Healthpoints: " + _max_healthpoints.ToString();
        _max_healthpoints_cost_text.text = _max_healthpoints >= _max_healthpoints_value ? "MAX" : "Cost: " + _max_healthpoints_cost.ToString();

        dash_cooldown_text.text = "Dash cooldown: " + _dash_cooldown.ToString("F2");
        dash_cooldown_cost_text.text = _dash_cooldown <= _min_dash_cooldown_value ? "MAX" : "Cost: " + _dash_cooldown_cost.ToString();

        healing_text.text = "Healing: " + _healing.ToString();
        healing_cost_text.text = _healing >= _max_healing_value ? "MAX" : "Cost: " + healing_cost.ToString();

        shot_cooldown_text.text = "Shooting cooldown: " + _shot_cooldown.ToString("F2");
        shot_cooldown_cost_text.text = _shot_cooldown <= _min_shooting_cooldown_value ? "MAX" : "Cost: " + _shooting_cooldown_cost.ToString();

        multiplier_text.text = "Multiplier: " + _multiplier.ToString();
        multiplier_cost_text.text = _multiplier >= _max_multiplier_value ? "MAX" : "Cost: " + _multiplier_cost.ToString();
    }

    public void LoadGameModes()
    {
        SceneManager.LoadScene(1);
    }
}
