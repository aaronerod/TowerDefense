using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarStrategy : MonoBehaviour, IPathfinder
{
    [SerializeField]
    GridMap gridMap=>mapControllerConnector.Controller.GridMap;
    [SerializeField]
    MapControllerConnector mapControllerConnector;


    public List<GridCell> FindPath(Vector2 start, Vector2 end)
    {
        List<GridCell> finalPath = new List<GridCell>();
        GridCell startCell = gridMap.GetNearestCell(start);
        GridCell endCell = gridMap.GetNearestCell(end);
        if (startCell != null && endCell!=null)
        {
            List<GridCell> openCells = new List<GridCell>();
            HashSet<GridCell> closedCells = new HashSet<GridCell>();
            Dictionary<GridCell, float> gCost = new Dictionary<GridCell, float>();
            Dictionary<GridCell, float> hCost = new Dictionary<GridCell, float>();
            Dictionary<GridCell, GridCell> parents = new Dictionary<GridCell, GridCell>();

            openCells.Add(startCell);
            SetCost(gCost, startCell, 0);
            SetCost(hCost, startCell, 0);
            parents.Add(startCell, startCell);

            while (openCells.Count > 0)
            {
                GridCell currentCell = openCells[0];
                for(int i = 1; i < openCells.Count; i++)
                {
                    float fCostCurrent = GetCost(gCost, currentCell) + GetCost(hCost, currentCell);
                    float fCost = gCost[openCells[i]] + hCost[openCells[i]];
                    if (fCost < fCostCurrent)
                    {
                        currentCell = openCells[i];
                    }
                }

                openCells.Remove(currentCell);

                if (currentCell == endCell )
                    break;
                else
                {
                    closedCells.Add(currentCell);
                    List<GridCell> neighbours = GetNeighbours(gridMap, currentCell);
                    foreach(var neighbour in neighbours)
                    {
                        if(neighbour.IsWalkable && !closedCells.Contains(neighbour))
                        {
                            float movementCost = GetCost(gCost,currentCell) + GetDistance(currentCell, neighbour);
                            if(movementCost<GetCost(gCost,neighbour) || !openCells.Contains(neighbour))
                            {
                                SetCost(gCost, neighbour, movementCost);
                                SetCost(hCost, neighbour, GetDistance(neighbour, endCell));
                                SetParent(parents, neighbour, currentCell);

                                if (!openCells.Contains(neighbour))
                                    openCells.Add(neighbour);
                            }
                        }
                    }

                }
            }

            GridCell currentNode = endCell;
            if (currentNode != null)
            {
                while (currentNode != startCell)
                {
                    finalPath.Add(currentNode);
                    currentNode = parents[currentNode];
                }
                finalPath.Reverse();
            }
            finalPath.Insert(0, startCell);
        }
        return finalPath;
    }
    private void SetParent(Dictionary<GridCell,GridCell> parents, GridCell cell, GridCell parent)
    {
        if (parents.ContainsKey(cell))
            parents[cell] = parent;
        else
            parents.Add(cell, parent);
    }
    private void SetCost(Dictionary<GridCell,float> costs, GridCell cell, float cost)
    {
        if (costs.ContainsKey(cell))
            costs[cell] = cost;
        else
            costs.Add(cell, cost);
    }
    private float GetCost(Dictionary<GridCell,float> costs, GridCell cell)
    {
        float cost;
        if (costs.TryGetValue(cell, out cost))
            return cost;
        else
            return 0;
    }

    private float GetDistance(GridCell a, GridCell b)
    {
        float xDistance = Mathf.Abs(a.Coordinates.x - b.Coordinates.x);
        float yDistance = Mathf.Abs(a.Coordinates.y - b.Coordinates.y);
        if (xDistance > yDistance)
            return 1.4f * yDistance + (xDistance - yDistance);
        else
            return 1.4f * xDistance + (yDistance - xDistance);
    }

    private List<GridCell> GetNeighbours(GridMap grid, GridCell cell)
    {
        List<GridCell> neighbours = new List<GridCell>();
        for(int x=-1;x<=1;x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                GridCell neighbour= grid.GetCell(new Vector3(cell.Coordinates.x + x, cell.Coordinates.y + y));
                if (neighbour != null)
                    neighbours.Add(neighbour);
            }
        }
        return neighbours;

    }
}
