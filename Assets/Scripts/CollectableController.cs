using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    private void Awake(){

        int score = 10;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            Debug.Log("Score added");
            //PlayerScore playerScore = collision.gameObject.GetComponent<PlayerScore>();
            //playerScore.AddScore(score);
        }
    }
}
