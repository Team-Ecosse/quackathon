using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");

            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage();

        }
    }
}