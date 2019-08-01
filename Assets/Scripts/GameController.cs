using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // UI
    public Text biscuitsNum;
    private int biscuitsCarried;
    public Text scoreText;
    private int score;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    // Game Over
    private bool gameEnded;
    public GameObject endScreen;

    // Spawning
    public int startingGroups;
    public int startingGroupSize;
    public int startingSoldierBs;
    public float spawnRate;
    private float nextSpawn;
    public GameObject spawnContainer;
    public GameObject germanSoldierA;
    public GameObject germanSoldierB;
    public GameObject enemyContainer;
    public GameObject spawnText;

    // Pickups
    public GameObject pickupContainer;
    public GameObject pickupSpawnsContainer;
    public GameObject healthPickup;
    public GameObject rapidFirePickup;

    void Start()
    {
        gameEnded = false;
        endScreen.SetActive(false);
        if(startingGroups < 1)
        {
            startingGroups = 1;
        }
        Reset();
    }

    void Update()
    {
        if (gameEnded)
        {
            Reset();
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

        if(Time.time > nextSpawn && !gameEnded)
        {
            int numOfGroups = startingGroups + biscuitsCarried / 6;
            int numInEachGroup = startingGroupSize + (biscuitsCarried / 2) % 3;
            int numOfSoldierB = startingSoldierBs + biscuitsCarried / 15;
            SpawnEnemyGroups(numOfGroups, numInEachGroup, numOfSoldierB);
            nextSpawn = Time.time + spawnRate;
            spawnText.SetActive(true);
            StartCoroutine(SpawnTextInactiveAfter(2));
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioListener.volume = (AudioListener.volume + 5) % 10;
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        System.DateTime localDate = System.DateTime.Now;
        gameObject.GetComponent<HighscoreController>().NewScore(score, localDate.Date.ToString("d"));
        Debug.Log("Score: " + score + " Date: " + localDate.Date.ToString("d"));
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
        biscuitsCarried = biscuits;
        biscuitsNum.text = biscuits.ToString();
    }

    public void SetScore(int biscuits)
    {
        score = biscuits;
        scoreText.text = "Score: " + score;
    }

    public void Reset()
    {
        foreach(Transform enemy in enemyContainer.transform)
        {
            Destroy(enemy.gameObject);
        }
        foreach (Transform pickup in pickupContainer.transform)
        {
            Destroy(pickup.gameObject);
        }
        ResetHealth();
        SpawnPickups();
    }

    IEnumerator SpawnTextInactiveAfter(int n)
    {
        yield return new WaitForSeconds(n);
        spawnText.SetActive(false);
    }

    void SpawnPickups()
    {
        int spawnCounter = 0;
        List<float> spawnIndexes = new List<float>();
        while (spawnCounter < 2)
        {
            int randIndex = Random.Range(0, pickupSpawnsContainer.transform.childCount);
            if (!spawnIndexes.Contains(randIndex))
            {
                Transform spawnPoint = pickupSpawnsContainer.transform.GetChild(randIndex);
                if (spawnCounter == 0)
                {
                    Instantiate(healthPickup, spawnPoint.position, spawnPoint.rotation);
                }
                else
                {
                    Instantiate(rapidFirePickup, spawnPoint.position, spawnPoint.rotation);
                }
                spawnIndexes.Add(randIndex);
                spawnCounter += 1;
            }
        }
    }
    void SpawnEnemyGroups(int numOfGroups, int numInEachGroup, int numOfSoldierB)
    {
        int spawnCounter = 0;
        int div = numOfSoldierB / numOfGroups;
        int rest = numOfSoldierB % numOfGroups;
        List<float> spawnIndexes = new List<float>();
        while (spawnCounter < numOfGroups && spawnCounter < spawnContainer.transform.childCount)
        {
            int randIndex = Random.Range(0, spawnContainer.transform.childCount);
            if (!spawnIndexes.Contains(randIndex))
            {
                spawnIndexes.Add(randIndex);
                if(rest != 0)
                {
                    SpawnGroupAtPoint(randIndex, numInEachGroup, div+1);
                    rest -= 1;
                }
                else
                {
                    SpawnGroupAtPoint(randIndex, numInEachGroup, div);
                }
                spawnCounter += 1;
            }
        }
        
    }

    void SpawnGroupAtPoint(int index, int numInEachGroup, int numOfSoldierB)
    {
        Transform spawnGroup = spawnContainer.transform.GetChild(index);
        foreach(Transform spawnPoint in spawnGroup)
        {
            if (numOfSoldierB > 0)
            {
                GameObject newSoldierB = Instantiate(germanSoldierB, spawnPoint.position, spawnPoint.rotation);
                newSoldierB.transform.SetParent(enemyContainer.transform);
                numOfSoldierB -= 1;
            }
            else
            {
                GameObject newSoldierA = Instantiate(germanSoldierA, spawnPoint.position, spawnPoint.rotation);
                newSoldierA.transform.SetParent(enemyContainer.transform);
            }
            numInEachGroup -= 1;
            if(numInEachGroup == 0)
            {
                return;
            }
        }
        SpawnGroupAtPoint(index, numInEachGroup, numOfSoldierB);
    }
}

