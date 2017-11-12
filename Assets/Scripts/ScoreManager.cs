using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{


    int playerScore;


    // Use this for initialization
    void Start()
    {
    }

    public void setScore(int sco)
    {
        playerScore = sco;

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
