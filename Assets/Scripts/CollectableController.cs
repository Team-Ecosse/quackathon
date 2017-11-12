using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    private GameObject _gameObject;
    private GameManager _gm;

    int score;

    private void Awake(){

        score = 10;
        
        _gameObject = GameObject.FindGameObjectWithTag("GameManager");
        _gm = _gameObject.GetComponent<GameManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            Debug.Log("Score added");
            _gm.PlayerAddSco(10);
        }
    }
}
