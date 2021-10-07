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

    public int Rows { get => rows; set => rows = value; }
    public int Cols { get => cols; set => cols = value; }
    public float CellSize { get => cellSize; set => cellSize = value; }
    public Vector3 Origin { get => origin; set => origin = value; }
    public GridCell[,] Grid { get => grid; set => grid = value; }

    public GridMap(Vector2Int gridSize, float cellSize, Vector3 origin)
    {
        this.cols = gridSize.x;
        this.rows = gridSize.y;
        this.origin = origin;
        this.cellSize = cellSize;
    }

    public void LoadBoardData(string boardData, out Vector3 towerPos, out Vector3 spawnPos)
    {
        towerPos = Vector2.zero; 
        spawnPos = Vector2.zero;
        string[] rows = boardData.Split('\n');
        for(int y=0;y<rows.Length;y++)
        {
            string row = rows[y];
            string[] cols = row.Split(',');
            for(int x =0;x< cols.Length;x++)
            {
                switch (cols[x])
                {
                    case "0":
                        //Unwalkable
                        Grid[x,rows.Length-1- y].IsWalkable = false;
                        break;
                    case "1":
                        //Walkable
                        Grid[x, rows.Length - 1 - y].IsWalkable = true;
                        break;
                    case "2":
                        //Tower
                        Grid[x, rows.Length - 1 - y].IsWalkable = true;
                        towerPos = Grid[x, rows.Length - 1 - y].WorldCoordinates;
                        break;
                    case "3":
                        //Spawn
                        Grid[x, rows.Length - 1 - y].IsWalkable = true;
                        spawnPos = Grid[x, rows.Length - 1 - y].WorldCoordinates;
                        break;
                }
            }
        }
    }

    [NaughtyAttributes.Button("Create Grid")]
    public void CreateGrid()
    {
        Grid = new GridCell[Cols, Rows];
        for(int x = 0; x < Cols; x++)
        {
            for(int y = 0; y < Rows; y++)
            {
                Grid[x, y] = new GridCell(new Vector2Int(x, y),Origin+ new Vector3(x*CellSize,y*CellSize)+Vector3.one*CellSize*.5f, true);
            }
        }
    }

    public GridCell GetNearestCell(int x, int y)
    {
        if (Grid != null)
        {
            x = Mathf.Clamp(x, 0, Cols-1);
            y = Mathf.Clamp(y, 0, Rows-1);

            return Grid[x, y];
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

    public GridCell GetCell(int x, int y)
    {
        if (x >= 0 && x < Cols && y >= 0 && y < Rows)
            return Grid[x, y];
        return null;
    }

    public GridCell GetCell(Vector3 worldPosition)
    {
        int x;
        int y;
        WorldToGrid(worldPosition, out x, out y);
        return GetCell(x, y);
    }

    public GridCell GetCenterCell()
    {
        return Grid[cols/2, rows/2];
    }

    /// <summary>
    /// Converts world coordinates to grid coordinates
    /// </summary>
    /// <param name="worldPosition">Position in world space</param>
    /// <param name="x">X position in grid</param>
    /// <param name="y">Y position in grid</param>
    public void WorldToGrid(Vector3 worldPosition, out int x, out int y)
    {
        Vector3 localPosition = worldPosition - Origin;
        x =  Mathf.FloorToInt(localPosition.x / CellSize);
        y = Mathf.FloorToInt(localPosition.y / CellSize );
        
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

        if(x>=0&& x<Cols && y >= 0 && y < Rows)
        {
            worldPosition.x = x * CellSize + Origin.x+CellSize*.5f;
            worldPosition.y = y * CellSize + Origin.y+CellSize*.5f;
        }
        return worldPosition;
    }

    public void OnDrawGizmosSelected()
    {
        if (Grid != null)
        {
            for(int x=0;x<Cols;x++)
            {
                for(int y = 0; y < Rows; y++)
                {
                    Gizmos.DrawWireCube(Origin + new Vector3(x, y) * CellSize+Vector3.one*CellSize*.5f, Vector3.one*CellSize);
                }
            }
        }
    }
}
