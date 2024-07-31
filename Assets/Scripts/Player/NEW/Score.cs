using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [HideInInspector] public int _score;
    int _totalScore;
    int _multiplier;

    void Start()
    {
        Load();
        UpdateScoreText();
    }

    public void UpdateScorePoints(int points)
    {
        _score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = _score + " x" + _multiplier;
    }

    public void OnDeath()
    {
        _totalScore += _score * _multiplier;

        GameData gameData = SaveSystem.Load();
        gameData.totalScore = _totalScore;
        SaveSystem.Save(gameData);
    }

    public void Load()
    {
        GameData gameData = SaveSystem.Load();
        _multiplier = gameData.multiplier;
        _totalScore = gameData.totalScore;
    }
}