using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    public Text text;

    public static void Reset()
    {
        score = 0;
    }

    public void Score(int points)
    {
        score += points;
        UpdateScore();
    }

    void UpdateScore()
    {
        text.text = "Score: " + score;
    }
}
