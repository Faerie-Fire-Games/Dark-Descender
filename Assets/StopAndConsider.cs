using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndConsider : StandardMovement
{
    [SerializeField] private float considerTime, considerVarience, moveTime, moveVarience;
    private bool considering;
    private bool inElevatorShaft;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        StartCoroutine(MoveTimer());
    }

    public override void Update()
    {
        if(!considering)
        {
            base.Update();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ElevatorShaft"))
        {
            inElevatorShaft = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ElevatorShaft"))
        {
            inElevatorShaft = false;
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
            yield return new WaitForSeconds(1f);
            StartCoroutine(ConsiderTimer());
        }
    }

    public IEnumerator ConsiderTimer()
    {
        considering = true;
        yield return new WaitForSeconds(considerTime + Random.Range(considerVarience, -considerVarience));
        StartCoroutine(MoveTimer());
    }
}
