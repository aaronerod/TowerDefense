using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridMap
{
    private GridCell[,] grid;
    [SerializeField]
    private int rows;
    [SerializeField]
    private int cols;
    [SerializeField]
    private float cellSize;
    [SerializeField]
    private Vector3 origin;

    [NaughtyAttributes.Button("Create Grid")]
    public void CreateGrid()
    {
        grid = new GridCell[cols, rows];
        for(int x = 0; x < cols; x++)
        {
            for(int y = 0; y < rows; y++)
            {
                grid[x, y] = new GridCell(new Vector2Int(x, y),origin+ new Vector3(x*cellSize,y*cellSize)+Vector3.one*cellSize*.5f, true);
            }
        }
    }

    public GridCell GetNearestCell(int x, int y)
    {
        if (grid != null)
        {
            x = Mathf.Clamp(x, 0, cols-1);
            y = Mathf.Clamp(y, 0, rows-1);

            Debug.LogError(x + " " + y);
            return grid[x, y];
        }
        else
            return null;
    }

    public GridCell GetNearestCell(Vector3 worldPosition)
    {
        int x;
        int y;
        WorldToGrid(worldPosition, out x, out y);
        return GetNearestCell(x, y);
    }

    public GridCell GetCell(Vector3 worldPosition)
    {
        int x;
        int y;
        WorldToGrid(worldPosition, out x, out y);
        if (x >= 0 && x < cols && y >= 0 && y < rows)
            return grid[x, y];
        return null;
    }

    /// <summary>
    /// Converts world coordinates to grid coordinates
    /// </summary>
    /// <param name="worldPosition">Position in world space</param>
    /// <param name="x">X position in grid</param>
    /// <param name="y">Y position in grid</param>
    public void WorldToGrid(Vector3 worldPosition, out int x, out int y)
    {
        Vector3 localPosition = worldPosition - origin;
        x =  Mathf.FloorToInt(localPosition.x / cellSize);
        y = Mathf.FloorToInt(localPosition.y / cellSize );
        
    }

    /// <summary>
    /// Converts grid coordinates to world coordinates
    /// </summary>
    /// <param name="x">X position in grid</param>
    /// <param name="y">Y position in grid</param>
    /// <returns>Vector3 world position</returns>
    public Vector3 GridToWorld(int x, int y)
    {
        Vector3 worldPosition = new Vector3(-1,-1);

        if(x>=0&& x<cols && y >= 0 && y < rows)
        {
            worldPosition.x = x * cellSize + origin.x+cellSize*.5f;
            worldPosition.y = y * cellSize + origin.y+cellSize*.5f;
        }
        return worldPosition;
    }

    public void OnDrawGizmosSelected()
    {
        if (grid != null)
        {
            for(int x=0;x<cols;x++)
            {
                for(int y = 0; y < rows; y++)
                {
                    Gizmos.DrawWireCube(origin + new Vector3(x, y) * cellSize+Vector3.one*cellSize*.5f, Vector3.one*cellSize);
                }
            }
        }
    }
}
