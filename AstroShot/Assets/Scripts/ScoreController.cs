using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private const int maxHighScores = 8;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        DisplayHighScores();
    }

    // Affiche les scores élevés dans le TextMeshProUGUI
    void DisplayHighScores()
    {
        string highScoreString = "High Scores:\n";

        for (int i = 0; i < maxHighScores; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);
            highScoreString += (i + 1) + ". " + score + "\n";
        }

        highScoreText.text = highScoreString;
    }
}