using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPassengers : MonoBehaviour
{
    GameObject passenger;
    ElevatorMovement elevatorMovement;
    private void Start()
    {
        elevatorMovement = transform.GetChild(0).gameObject.GetComponent<ElevatorMovement>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Enemy"))
        {
            passenger = col.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Enemy"))
        {
            passenger = null;
        }
    }

    void Update()
    {
        elevatorMovement.passenger = passenger;
    }
}
