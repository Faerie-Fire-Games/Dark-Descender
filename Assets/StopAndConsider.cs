using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndConsider : StandardMovement
{
    [SerializeField] private float considerTime, considerVarience, moveTime, moveVarience;
    private bool considering;
    private bool inElevatorShaft;
    private bool inElevatorLastFrame = false;

    private float masterSpeed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        masterSpeed = speed;

        StartCoroutine(MoveTimer());
    }

    public override void Update()
    {
        if(!considering)
        {
            base.Update();
        }

        if(inElevatorLastFrame && !gameObject.GetComponent<Enemy>().inElevator)
        {
            StartCoroutine(MoveTimer());
        }

        if(gameObject.GetComponent<Enemy>().inElevator)
        {
            inElevatorLastFrame = true;
        }
        else { inElevatorLastFrame = false; }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ElevatorShaft"))
        {
            inElevatorShaft = true;
            if(col.transform.GetChild(0).gameObject.GetComponent<ElevatorMovement>().isMoving && col.transform.GetChild(0).transform.position.y > transform.position.y)
            {
                speed += 3;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ElevatorShaft"))
        {
            inElevatorShaft = false;
            speed = masterSpeed;
        }
    }

    public IEnumerator MoveTimer()
    {
        considering = false;
        yield return new WaitForSeconds(moveTime + Random.Range(moveVarience, -moveVarience));
        if(!inElevatorShaft)
        {
            StartCoroutine(ConsiderTimer());
        }
        else
        {
            yield return new WaitForSeconds(1.2f);
            StartCoroutine(ConsiderTimer());
        }
    }

    public IEnumerator ConsiderTimer()
    {
        considering = true;
        yield return new WaitForSeconds(considerTime + Random.Range(considerVarience, -considerVarience));
        if (!inElevatorShaft)
        {
            StartCoroutine(MoveTimer());
        }
        else
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(MoveTimer());
        }
    }
}
