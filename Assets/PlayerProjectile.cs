using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int baseDamage;

    public virtual void Start()
    {
        Destroy(gameObject, 10f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            var enemy = col.gameObject;
            enemy.GetComponent<Enemy>().TakeDamage(baseDamage);
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Solid"))
        {
            Destroy(gameObject);
        }
    }
}
