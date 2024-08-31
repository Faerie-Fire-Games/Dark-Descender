using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    GameObject player;

    public int baseDamage = 2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == player)
        {
            player.GetComponent<PlayerVariables>().TakeDamage(baseDamage);
        }
    }
}
