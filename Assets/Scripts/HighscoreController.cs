using UnityEngine;

public class HighscoreController : MonoBehaviour
{
   public void NewScore(int score, string date)
    {
        int highscore1Score = PlayerPrefs.GetInt("Highscore1Score", 0);
        string highscore1Date = PlayerPrefs.GetString("Highscore1Date", "NaN");
        if(score > highscore1Score)
        {
            PlayerPrefs.SetInt("Highscore1Score", score);
            PlayerPrefs.SetString("Highscore1Date", date);
            score = highscore1Score;
            date = highscore1Date;
        }

        int highscore2Score = PlayerPrefs.GetInt("Highscore2Score", 0);
        string highscore2Date = PlayerPrefs.GetString("Highscore2Date", "NaN");
        if (score > highscore2Score)
        {
            PlayerPrefs.SetInt("Highscore2Score", score);
            PlayerPrefs.SetString("Highscore2Date", date);
            score = highscore2Score;
            date = highscore2Date;
        }

        int highscore3Score = PlayerPrefs.GetInt("Highscore3Score", 0);
        if (score > highscore3Score)
        {
            PlayerPrefs.SetInt("Highscore1Score", score);
            PlayerPrefs.SetString("Highscore1Date", date);
        }
        PlayerPrefs.Save();
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("Highscore1Score");
        PlayerPrefs.DeleteKey("Highscore1Date");
        PlayerPrefs.DeleteKey("Highscore2Score");
        PlayerPrefs.DeleteKey("Highscore2Date");
        PlayerPrefs.DeleteKey("Highscore3Score");
        PlayerPrefs.DeleteKey("Highscore3Date");
        PlayerPrefs.Save();
    }
}
