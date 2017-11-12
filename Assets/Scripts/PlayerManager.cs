using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    int playerHealth;


    public void setHealth(int health)
    {

        playerHealth = health;
    }

    public void playerTakenDMG(int damage){
        
        playerHealth -= damage;

        if (playerHealth <= 0) GameManager.ResetScene();

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
