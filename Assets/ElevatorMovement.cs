using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{

    [SerializeField] private List<Transform> stops;

    private Transform currentStop;
    public int currentStopIndex = -1;
    public Transform movingTo;
    private int sign;

    [SerializeField]private float disembarkTime = 1f;
    [SerializeField] private float alightingTime = 1.2f;
    [SerializeField] private float elevatorSpeed;

    public bool isMoving;

    public bool alighting = true;
    public bool disembarking = false;

    public GameObject passenger;

    void Start()
    {
        currentStopIndex = 0;
        currentStop = stops[0];
        sign = 1;
        movingTo = stops[currentStopIndex + sign];



        NextStop();
        isMoving = true;
    }



    public bool CheckPassangerInElevator()
    {
        if (passenger != null)
        {
            if (passenger.transform.position.x >= gameObject.transform.position.x - 1.3f && passenger.transform.position.x <= gameObject.transform.position.x + 1.3f)
            {
                if (passenger.transform.position.y >= gameObject.transform.position.y + 0.7f && passenger.transform.position.y <= gameObject.transform.position.y + 2.8f)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        else return false;
    }

    void FixedUpdate()
    {
        if(CheckPassangerInElevator())
        {
            if(isMoving)
            {
                print(passenger.name + " is in the elevator");
                if (passenger.CompareTag("Player"))
                {
                    PlayerMovement.playerInElevator = true;

                }
                if (passenger.CompareTag("Enemy"))
                {
                    passenger.GetComponent<Enemy>().inElevator = true;
                }
                // StopCoroutine(AlightingTime());
                passenger.transform.SetParent(transform);
                //player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
                passenger.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, passenger.transform.position.z);

            }

        }


        if(isMoving)
        {
            //print("isMoving");
            transform.position = Vector2.MoveTowards(transform.position, movingTo.position, elevatorSpeed);
            gameObject.transform.parent.gameObject.layer = 7; // Passable Wall Layer
        }
        else
        {
            gameObject.transform.parent.gameObject.layer = 0; // Default Layer
        }

        if(isMoving && transform.position.y >= movingTo.position.y - 0.1f && transform.position.y <= movingTo.position.y + 0.1f)
        {
            transform.position = movingTo.position;
            isMoving = false;
            if (!disembarking)
            {
                StartCoroutine(DisembarkTimer());
            }
        }
    }

    private void NextStop()
    {

        if(currentStopIndex == stops.Count - 1)
        {
            sign = -1;
        }
        else if(currentStopIndex == 0)
        {
            sign = 1;
        }
        //print("Next Stop Requested.\nSign = " + sign + "\nMoving To: " + stops[currentStopIndex + sign].name);

        movingTo = stops[currentStopIndex + sign];
        currentStopIndex += sign;
    }

    private IEnumerator DisembarkTimer()
    {
        currentStop = movingTo;
        if(passenger != null)
        {
            passenger.transform.parent = null;
            if(passenger.CompareTag("Player"))
            {
                PlayerMovement.playerInElevator = false;

            }
            if (passenger.CompareTag("Enemy"))
            {
                passenger.GetComponent<Enemy>().inElevator = false;
            }
            if(CheckPassangerInElevator())
            {
                passenger.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, passenger.transform.position.z);
            }
        }
        disembarking = true;
        alighting = false;
        yield return new WaitForSeconds(disembarkTime);
        StartCoroutine(AlightingTime());
    }


    private IEnumerator AlightingTime()
    {
        disembarking = false;
        alighting = true;
        yield return new WaitForSeconds(alightingTime);
        NextStop();
        isMoving = true;
        //print("Passengers may now alight");
        //yield return new WaitForSeconds(0.5f);
        yield return null;
    }
}
