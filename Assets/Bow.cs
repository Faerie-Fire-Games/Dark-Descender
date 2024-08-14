using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    SpriteRenderer sr;
    PlayerMovement playerMovement;

    public bool isLoaded;

    public static int bowPosition = 2;

    [SerializeField] private Sprite bowLoaded, bowUnloaded;
    [SerializeField] private GameObject arrowMaster;
    [SerializeField] private float arrowSpeed;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        playerMovement = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>();

        bowPosition = 2;
    }


    public void Fire(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if (isLoaded && bowPosition == 2)
            {
                isLoaded = false;
                sr.sprite = bowUnloaded;

                
                GameObject playerArrow = Instantiate(arrowMaster, transform.position, transform.rotation);
                var arrowDirection = 1;
                if(!playerMovement.facingRight) { arrowDirection = -1; }
                playerArrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * arrowDirection, 0);
            }
        }
        
    }

    public void BowUp(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if (bowPosition < 3) { bowPosition++; }

            if (bowPosition == 3)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                sr.sprite = bowLoaded;
                isLoaded = true;
            }
            if (bowPosition == 2)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
    }

    public void BowDown(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if (bowPosition > 1) { bowPosition--; }

            if (bowPosition == 2)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (bowPosition == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
        }

        
    }
}
