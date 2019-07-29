using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text biscuitsNum;
    public Text score;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

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

    public void ShowScore(int biscuits)
    {
        score.text = "Score: " + biscuits;
    }
}

