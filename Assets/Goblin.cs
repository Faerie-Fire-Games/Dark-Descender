using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{

    private bool canSeePlayer = false;
    [SerializeField] private GameObject spear;
    private RaycastHit2D findPlayerRay;
    [SerializeField] private LayerMask checkLayer;
    private bool isFacingRight = true;
    private bool readyingSpear = false;
    [SerializeField] private int ammo = 1;
    [SerializeField] private float spearSpeed = 10f;

    [SerializeField] private GameObject spearMaster;

    void Start()
    {
        base.Start();
        spear.transform.rotation =  Quaternion.Euler(0, 0, 90);
    }

    // Update is called once per frame
    void Update()
    {
        isFacingRight = gameObject.GetComponent<StandardMovement>().facingRight;
        findPlayerRay = Physics2D.Raycast(gameObject.transform.position, Vector2.right * (isFacingRight ? 1 : -1), 5f, checkLayer);
        if(findPlayerRay.collider != null)
        {
            if (findPlayerRay.collider.gameObject.CompareTag("Player") && ammo > 0 && !readyingSpear)
            {
                readyingSpear = true;
                spear.transform.rotation = Quaternion.Euler(0, 0, (isFacingRight ? 0 : 180));
                StartCoroutine(ReadySpear());
            }
            else if(!findPlayerRay.collider.gameObject.CompareTag("Player") && readyingSpear)
            {
                readyingSpear = false;
                spear.transform.rotation = Quaternion.Euler(0, 0, 90);
                StopCoroutine(ReadySpear());
            }
        }

        if(ammo == 0)
        {
            spear.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public IEnumerator ReadySpear()
    {
        yield return new WaitForSeconds(0.8f);
        if(readyingSpear)
        {
            ThrowSpear();
        }
    }

    void ThrowSpear()
    {
        ammo--;
        GameObject thrownSpear = Instantiate(spearMaster, spear.transform.position, spear.transform.rotation);
        thrownSpear.GetComponent<Rigidbody2D>().velocity = (Vector2.right * (isFacingRight ? 1 : -1)) * spearSpeed;
    }

    /*public override void TakeDamage(int damage)
    {

    }*/
}
