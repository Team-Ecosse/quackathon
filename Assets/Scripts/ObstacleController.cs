using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    public GameManager GameManager;
    int damage;


    private void Awake()
    {

        damage = 1;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player"){

            //Debug.Log("Player hit!");

            GameManager.PlayerTakeDamage(damage);

        }
    }
}
