using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPathStrategy : PathfindingStrategy
{
    public override List<GridCell> FindPath(Vector2 start, Vector2 end)
    {
        GridCell startCell = gridMap.GetNearestCell(start);
        GridCell endCell = gridMap.GetNearestCell(end);
        List<GridCell> airPath = new List<GridCell>();
        airPath.Add(startCell);
        airPath.Add(endCell);
        return airPath;
    }
}
