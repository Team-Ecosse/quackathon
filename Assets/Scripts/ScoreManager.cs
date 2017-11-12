using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{


    int playerScore;


    // Use this for initialization
    void Start()
    {

        playerScore = 0;

    }


    public void playerAddScore(int add)
    {

        playerScore += add;

    }

    public void playerLoseScore(int lose)
    {

        playerScore -= lose;

    }

    public int returnPlayerScore()
    {

        return playerScore;
    }
}
