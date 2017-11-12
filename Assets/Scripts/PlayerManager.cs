using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    int playerHealth;

    public void setHealth(int health)
    {

        playerHealth = health;
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
