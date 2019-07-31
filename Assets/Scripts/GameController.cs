using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text biscuitsNum;
    public Text scoreText;
    private int score;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private bool gameEnded;
    public GameObject endScreen;

    public GameObject spawnContainer;
    public GameObject germanSoldierA;
    public GameObject germanSoldierB;


    void Start()
    {
        gameEnded = false;
        endScreen.SetActive(false);
        SpawnEnemyGroups();
    }

    void Update()
    {
        if (gameEnded)
        {
            endScreen.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("GameScene");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioListener.volume = (AudioListener.volume + 5) % 10;
        }
    }

    public void EndGame()
    {
        gameEnded = true;
    }

    public void ShowHealth(int health)
    {
        if (health < 1)
        {
            heart1.SetActive(false);
        }
        else if(health < 2)
        {
            heart2.SetActive(false);
        }
        else if(health < 3)
        {
            heart3.SetActive(false);
        }
    }

    public void ResetHealth()
    {
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
    }

    public void ShowBiscuitsCarried(int biscuits)
    {
        biscuitsNum.text = biscuits.ToString();
    }

    public void SetScore(int biscuits)
    {
        score = biscuits;
        scoreText.text = "Score: " + score;
    }

    void SpawnEnemyGroups()
    {
        int spawnCounter = 0;
        List<float> spawnIndexes = new List<float>();
        while (spawnCounter < 5)
        {
            int randIndex = Random.Range(0, spawnContainer.transform.childCount - 1);
            if (!spawnIndexes.Contains(randIndex))
            {
                spawnIndexes.Add(randIndex);
                SpawnGroupAtPoint(randIndex);
                spawnCounter += 1;
            }
        }
        
    }

    void SpawnGroupAtPoint(int i)
    {
        Transform spawnGroup = spawnContainer.transform.GetChild(i);
        foreach(Transform spawnPoint in spawnGroup)
        {
            Instantiate(germanSoldierA, spawnPoint.position, spawnPoint.rotation);
        }
    }
}

