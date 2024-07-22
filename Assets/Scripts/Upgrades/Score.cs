using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int multiplier;

    void Start()
    {
        GameData gameData = SaveSystem.Load();

        multiplier = gameData.multiplier;
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString() + " x " + multiplier;
    }
}
