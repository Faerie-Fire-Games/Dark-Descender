using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoverRoom : Room
{
    public GameObject partnerDoor;

    private bool entering = false, inside = false, exiting = false;


    public override void EnterRoom(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if(CheckPlayerInDoorway())
            {

                if(occupiedByPlayer && entering)
                {
                    entering = false;
                    inside = true;
                    exiting = false;
                }
                if (!occupiedByPlayer && !entering && !exiting)
                {
                    entering = true;
                }

                if (entering)
                { 
                    base.EnterRoom(ctx);
                }
                if (exiting)
                {
                    base.EnterRoom(ctx);
                    exiting = false;
                }


                print(gameObject.name + "Entering = " + entering);
                print(gameObject.name + "Inside = " + inside);

                if(inside)
                {
                    player.transform.position = partnerDoor.transform.position;
                    inside = false;
                    occupied = false;
                    occupiedByPlayer = false;
                    partnerDoor.GetComponent<MoverRoom>().exiting = true;
                    partnerDoor.GetComponent<MoverRoom>().occupied = true;
                    partnerDoor.GetComponent<MoverRoom>().occupiedByPlayer = true;
                }
            }

        }
    }
}
