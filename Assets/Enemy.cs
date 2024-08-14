using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public virtual void Start()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        print("Taking damage through enemy.cs");
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int healAmount)
    {
        health += healAmount;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject, 0.1f);
    }
}
