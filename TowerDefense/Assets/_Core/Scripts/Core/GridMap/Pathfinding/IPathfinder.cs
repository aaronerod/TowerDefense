using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfinder
{
    public List<GridCell> FindPath(GridMap gridMap, Vector2 start, Vector2 end);
}
