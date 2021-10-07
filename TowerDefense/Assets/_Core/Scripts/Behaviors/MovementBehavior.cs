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
    private List<GridCell> path;

    private GridCell targetPoint;
    private Vector3 targetPointModified;
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

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > range)
        {
            Vector3 desiredVelocity = targetPointModified - transform.position;
            Vector3 desiredVelocityNormalized = desiredVelocity.normalized;
            desiredVelocity.Normalize();
            desiredVelocityNormalized *= movementSpeed;
            Vector3 steeringVelocity = desiredVelocityNormalized - transform.up;
            steeringVelocity = Vector3.ClampMagnitude(steeringVelocity, movementSpeed);

            transform.up = steeringVelocity;
            transform.position += steeringVelocity * Time.deltaTime;
        }
        else
        {
            Vector3 desiredDirection = target.position - transform.position;
            desiredDirection.Normalize();
            Vector3 steeringDirection = desiredDirection - transform.up;
            transform.up += steeringDirection * Time.deltaTime * 5;
        }

    }

    private void UpdateTargetPoint()
    {
        if(targetPoint==null && path.Count > 0)
        {
            SelectNextCell();

        }
        else
        {
            if(Vector2.Distance(transform.position,targetPointModified)<.2f)
            {
                Debug.LogError("Move next point");
                SelectNextCell();
            }
        }

//        Debug.LogError(Vector2.Distance(transform.position, targetPoint.WorldCoordinates));
    }
    private void SelectNextCell()
    {
        if (path.Count > 0)
        {
            targetPoint = path[0];
            path.RemoveAt(0);
            if (path.Count > 0)
                targetPointModified = targetPoint.WorldCoordinates + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
            else
            {
                Vector3 direction = transform.position - targetPoint.WorldCoordinates;
                direction.Normalize();
                Vector3 rangePosition = targetPoint.WorldCoordinates + direction * range;
                targetPointModified = targetPoint.WorldCoordinates + (new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * range*.7f);
                
            }
        }
     }
}
