using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
