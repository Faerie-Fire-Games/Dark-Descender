using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    CapsuleCollider2D capCollider;

    public Vector2 move;

    public static bool playerInElevator, playerInRoom;
    public bool facingRight = true;

    public float speed = 30f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }


    public void getMovement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();

        if(move.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
        if(move.x < 0)
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        if(!playerInElevator || !playerInRoom)
        {
            rb.velocity = move * speed;
            //capCollider.enabled = true;
            rb.isKinematic = false;
        }

        if(playerInElevator || playerInRoom)
        {
            rb.velocity = Vector2.zero;
            //capCollider.enabled = false;
            rb.isKinematic = true;
        }

        if(playerInRoom)
        {
            capCollider.enabled = false;
        }
        else
        {
            capCollider.enabled = true;
        }
    }

}