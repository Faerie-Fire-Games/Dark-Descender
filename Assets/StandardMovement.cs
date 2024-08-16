using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardMovement : MonoBehaviour
{
    public float speed = 5f;

    public Vector2 moveTo;

    [SerializeField] private LayerMask layerMask = 3;
    [SerializeField] private LayerMask shaftLayerMask = 7;
    private RaycastHit2D pathRay;
    private RaycastHit2D wallRay;

    public bool facingRight = true;
    public float bodyWidth;



    public virtual void Start()
    {
        bodyWidth /= 2;

        moveTo = FindFurthestPoint();
    }

    public virtual Vector2 FindFurthestPoint()
    {
        pathRay = Physics2D.Raycast(gameObject.transform.position, Vector2.right, Mathf.Infinity, layerMask);
        var pointA = pathRay.point;
        var distanceA = Vector2.Distance(pathRay.point, transform.position);

        pathRay = Physics2D.Raycast(gameObject.transform.position, Vector2.left, Mathf.Infinity, layerMask);
        var pointB = pathRay.point;
        var distanceB = Vector2.Distance(pathRay.point, transform.position);

        if (distanceA >= distanceB)
        {
            return pointA;
        }
        else
        {
            return pointB;
        }
    }

    public virtual bool InFrontOfWall(LayerMask checkLayer)
    {
        if(facingRight)
        {
            wallRay = Physics2D.Raycast(gameObject.transform.position, Vector2.right, bodyWidth + 0.1f, checkLayer);
            if (wallRay.collider != null && (wallRay.collider.gameObject.CompareTag("Solid") || wallRay.collider.gameObject.CompareTag("ElevatorShaft")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            wallRay = Physics2D.Raycast(gameObject.transform.position, Vector2.left, bodyWidth + 0.1f, layerMask);
            if (wallRay.collider != null && (wallRay.collider.gameObject.CompareTag("Solid") || wallRay.collider.gameObject.CompareTag("ElevatorShaft")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public virtual void Update()
    {

        if (InFrontOfWall(layerMask) || transform.position.y < moveTo.y - 1 || transform.position.y > moveTo.y + 1)
        {
            moveTo = FindFurthestPoint();
        }

        if(moveTo.x > transform.position.x) { facingRight = true; }
        else { facingRight = false; }


        
        if(!gameObject.GetComponent<Enemy>().inElevator/* && !InFrontOfWall(shaftLayerMask)*/)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(moveTo, Vector3.one / 2);
    }
}
