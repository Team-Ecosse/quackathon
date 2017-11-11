using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collected Collectable");
            //PlayerScore playerScore = collision.gameObject.GetComponent<PlayerScore>();
            //playerScore.AddPoints();
        }

    }
}
