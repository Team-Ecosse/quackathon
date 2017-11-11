using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    public GameManager GameManager;

    int score;

    private void Awake(){

        score = 10;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            Debug.Log("Score added");
            GameManager.PlayerAddSco(score);
        }
    }
}
