using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score_text;
    [HideInInspector] public int score;
    int _total_score;
    int _multiplier;

    void Start()
    {
        Load();
        UpdateScoreText();
    }

    public void UpdateScorePoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        score_text.text = score + " x" + _multiplier;
    }

    public void OnDeath()
    {
        _total_score += score * _multiplier;

        GameData gameData = SaveSystem.Load();
        gameData.total_score = _total_score;
        SaveSystem.Save(gameData);
    }

    public void Load()
    {
        GameData gameData = SaveSystem.Load();
        _multiplier = gameData.multiplier;
        _total_score = gameData.total_score;
    }
}