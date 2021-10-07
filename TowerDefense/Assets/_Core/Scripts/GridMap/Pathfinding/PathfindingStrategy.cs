using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathfindingStrategy : MonoBehaviour, IPathfinder
{
    [SerializeField]
    protected GridMap gridMap => mapControllerConnector.Controller.GridMap;
    [SerializeField]
    protected MapControllerConnector mapControllerConnector;
    public abstract List<GridCell> FindPath(Vector2 start, Vector2 end);
}
