using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject highscore1Score;
    public GameObject highscore1Date;
    public GameObject highscore2Score;
    public GameObject highscore2Date;
    public GameObject highscore3Score;
    public GameObject highscore3Date;

    void Start()
    {
        AudioListener.volume = 1;
        UpdateHighscoreText();
    }

    public void UpdateHighscoreText()
    {
        highscore1Score.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("Highscore1Score", 0).ToString();
        highscore1Date.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetString("Highscore1Date", "NaN");
        highscore2Score.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("Highscore2Score", 0).ToString();
        highscore2Date.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetString("Highscore2Date", "NaN");
        highscore3Score.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("Highscore3Score", 0).ToString();
        highscore3Date.GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetString("Highscore3Date", "NaN");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
