using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    int playerHealth;

    // Use this for initialization
    void Start () {

        playerHealth = 3;
		
	}
	

    public void playerTakenDMG(int damage){

        playerHealth -= damage;

    }

    public void playerGainHealth(int heal)
    {

        playerHealth += heal;

    }

    public int returnPlayerHealth()
    {

        return playerHealth;
    }
}
