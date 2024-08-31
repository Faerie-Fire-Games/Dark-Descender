using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerVariables : MonoBehaviour
{
    int playerHealth = 2;

    void Start()
    {
        
    }

    public void TakeDamage(int recievedDamage, string damageType = "normal")
    {
        playerHealth -= recievedDamage;
        if(playerHealth <= 0)
        {
            Die();
        }
    }

    
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
