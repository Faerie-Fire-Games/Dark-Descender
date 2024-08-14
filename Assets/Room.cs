using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Room : MonoBehaviour
{
    public string roomType;

    public bool occupied = false;
    public bool occupiedByPlayer = false;
    public bool occupiedByEnemy = false;

    public GameObject player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    public virtual bool CheckPlayerInDoorway()
    {
        if (player.transform.position.x >= gameObject.transform.position.x - 1f && player.transform.position.x <= gameObject.transform.position.x + 1f)
        {
            if (player.transform.position.y >= gameObject.transform.position.y - 0.5f && player.transform.position.y <= gameObject.transform.position.y + 0.5f)
            {
                //print("Player In Doorway");
                //print(occupied.ToString() + Bow.bowPosition.ToString());
                return true;
            }
            else { return false; }
        }
        else { return false; }
    }

    public virtual void EnterRoom(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!occupied && CheckPlayerInDoorway())
            {
                print("Working");
                PlayerMovement.playerInRoom = true;
                player.GetComponent<SpriteRenderer>().sortingOrder = -2; //Place player back in front of door.
                player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z); // Centre Player
                occupied = true;
                occupiedByPlayer = true;
            }
            else if(occupied)
            {
                if(occupiedByEnemy)
                {
                    // Do smth
                }

                if(occupiedByPlayer)
                {
                    PlayerMovement.playerInRoom = false;
                    player.GetComponent<SpriteRenderer>().sortingOrder = 0; //Place player back in front of door.
                    occupied = false;
                    occupiedByPlayer = false;
                }
            }
        }
    }
}
