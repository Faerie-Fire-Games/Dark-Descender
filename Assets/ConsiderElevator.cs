using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsiderElevator : StopAndConsider
{

    [SerializeField] private float useElevatorChance = 7f;

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("ElevatorShaft"))
        {
            var elevator = col.gameObject.transform.GetChild(0).gameObject;
            if (elevator.transform.position.y < transform.position.y && elevator.GetComponent<ElevatorMovement>().alighting)
            {
                gameObject.GetComponent<Enemy>().inElevator = true;
                gameObject.transform.position = new Vector3(elevator.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else
            {
                gameObject.GetComponent<Enemy>().inElevator = false;
            }
        }
    }
}
