using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    [SerializeField]
    private AStarStrategy aStarStrategy;

    private Transform target;
    private float movementSpeed;
    private float range;
    List<GridCell> path;

    GridCell targetPoint;
    public void Initialize(Transform targetPosition, float movementSpeed,float range)
    {
        target = targetPosition;
        this.movementSpeed = movementSpeed;
        this.range = range;
        targetPoint = null;
        path = aStarStrategy.FindPath(transform.position, targetPosition.position);
    }

    public void UpdateBehavior()
    {
        UpdateTargetPoint();
        if (Vector2.Distance(transform.position, target.position) > range)
        {
            Vector3 desiredVelocity = targetPoint.WorldCoordinates - transform.position;
            desiredVelocity.Normalize();
            desiredVelocity *= movementSpeed;
            Vector3 steeringVelocity = desiredVelocity - transform.up;
            transform.up = steeringVelocity;
            transform.position += steeringVelocity * Time.deltaTime;
        }

    }

    private void UpdateTargetPoint()
    {
        if(targetPoint==null && path.Count > 0)
        {
            targetPoint = path[0];
            path.RemoveAt(0);

        }
        else
        {
            if(Vector2.Distance(transform.position,targetPoint.WorldCoordinates)<.2f)
            {
                Debug.LogError("Move next point");
                targetPoint = path[0];
                path.RemoveAt(0);
            }
        }

//        Debug.LogError(Vector2.Distance(transform.position, targetPoint.WorldCoordinates));
    }
}
