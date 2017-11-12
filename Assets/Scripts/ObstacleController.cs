using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    private GameObject _gameObject;
    private GameManager _gm;
    int damage;


    private void Awake()
    {

        damage = 1;
        
        _gameObject = GameObject.FindGameObjectWithTag("GameManager");
        _gm = _gameObject.GetComponent<GameManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "Player")
        {
            _gm.PlayerTakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
