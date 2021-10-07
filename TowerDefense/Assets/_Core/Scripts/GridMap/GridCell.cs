using UnityEngine;
using System.Collections.Generic;
public class GridCell
{
    private Vector2Int coordinates;
    private Vector2 worldCoordinates;
    private IBuildable currentBuilding;
    private List<Enemy> enemies = new List<Enemy>();
    private bool isWalkable;

    public bool IsEmpty => CurrentBuilding == null;
    public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }
    public IBuildable CurrentBuilding { get => currentBuilding; set => currentBuilding = value; }
    public List<Enemy> Enemies { get => enemies; set => enemies = value; }
    public bool IsWalkable { get => isWalkable&&IsEmpty; set => isWalkable = value; }
    public Vector3 WorldCoordinates { get => worldCoordinates; set => worldCoordinates = value; }
    
    public GridCell(Vector2Int coordinates, Vector3 worldCoordinates, bool isBuildable)
    {
        this.Coordinates = coordinates;
        this.WorldCoordinates = worldCoordinates;
        this.IsWalkable = isBuildable;
    }
    public void AssignBuildable(IBuildable buildable)
    {
        CurrentBuilding = buildable;
    }
    public void RemoveBuildable()
    {
        CurrentBuilding = null;
    }

}
