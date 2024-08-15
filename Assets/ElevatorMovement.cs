using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ElevatorMovement : MonoBehaviour
{
    PlayerInput playerInput;

    [SerializeField] private List<Transform> stops;

    private Transform currentStop;
    public int currentStopIndex = -1;
    public Transform movingTo;
    private int sign;

    [SerializeField]private float disembarkTime = 1.7f;
    [SerializeField] private float elevatorSpeed;

    public bool isMoving;

    public bool alighting = true;
    public bool disembarking = false;

    GameObject player;

    void Start()
    {
        currentStopIndex = 0;
        currentStop = stops[0];
        sign = 1;
        movingTo = stops[currentStopIndex + sign];

        playerInput = GetComponent<PlayerInput>();


        player = GameObject.FindGameObjectWithTag("Player");
        NextStop();
        isMoving = true;
    }

    public bool checkPlayerInElevator()
    {
        if (player.transform.position.x >= gameObject.transform.position.x - 1.3f && player.transform.position.x <= gameObject.transform.position.x + 1.3f)
        {
            if (player.transform.position.y >= gameObject.transform.position.y + 1.4f && player.transform.position.y <= gameObject.transform.position.y + 2.8f)
            {
                return true;
            }
            else { return false; }
        }
        else { return false; }
    }

    void FixedUpdate()
    {
        if(checkPlayerInElevator())
        {
            if(isMoving)
            {
                PlayerMovement.playerInElevator = true;
                // StopCoroutine(AlightingTime());
                GameObject.FindGameObjectWithTag("Player").gameObject.transform.SetParent(transform);
                //player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
                player.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, player.transform.position.z);

            }
        }


        if(isMoving)
        {
            //print("isMoving");
            transform.position = Vector2.MoveTowards(transform.position, movingTo.position, elevatorSpeed);
            
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
        player.transform.parent = null;
        PlayerMovement.playerInElevator = false;
        if(checkPlayerInElevator())
        {
            player.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, player.transform.position.z);
        }
        disembarking = true;
        alighting = false;
        yield return new WaitForSeconds(disembarkTime);
        StartCoroutine(AlightingTime());
    }


    private IEnumerator AlightingTime()
    {
        NextStop();
        isMoving = true;
        //print("Passengers may now alight");
        alighting = true;
        disembarking = false;
        //yield return new WaitForSeconds(0.5f);
        yield return null;
    }
}
