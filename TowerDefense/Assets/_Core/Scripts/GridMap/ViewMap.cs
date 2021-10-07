using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMap : MonoBehaviour
{
    [SerializeField]
    private MapController mapController;
    [SerializeField]
    private Transform mapParent;

    private Dictionary<GameObject, Stack<GameObject>> activeCells = new Dictionary<GameObject, Stack<GameObject>>();
    private Dictionary<GameObject, Stack<GameObject>> recycledCells = new Dictionary<GameObject, Stack<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        mapController.MapLoaded += OnMapLoaded;
    }

    void OnMapLoaded()
    {
        Recycle();
        GridMap gridMap = mapController.GridMap;
        foreach(var cell in gridMap.Grid)
        {
            GameObject cellInstance;
            if (cell.IsWalkable)
                cellInstance = GetCellInstance(mapController.LevelData.WalkableCell);
            else
                cellInstance = GetCellInstance(mapController.LevelData.UnwalkableCell);

            cellInstance.transform.position = cell.WorldCoordinates;

            
        }
    }
    private void Recycle()
    {
        foreach (var cellGroup in activeCells)
        {
            Stack<GameObject> recycled;
            if (recycledCells.TryGetValue(cellGroup.Key, out recycled))
            {
                foreach (var cell in cellGroup.Value)
                {
                    recycled.Push(cell);
                    cell.SetActive(false);
                }
            }
            else
            {
                recycled = new Stack<GameObject>();
                foreach (var cell in cellGroup.Value)
                {
                    recycled.Push(cell);
                    cell.SetActive(false);
                }
                recycledCells.Add(cellGroup.Key, recycled);
            }
        }

    }
    GameObject GetCellInstance(GameObject prefab)
    {
        GameObject newCell = null;
        Stack <GameObject> cells;
        if(recycledCells.TryGetValue(prefab, out cells)){
            if(cells.Count>0)
            {
                newCell = cells.Pop();
                newCell.SetActive(true);
            }
        }
        if (newCell == null)
        {
            newCell = Instantiate(prefab, mapParent, false);
            Stack<GameObject> active;
            if(activeCells.TryGetValue(prefab,out active))
            {
                active.Push(newCell);
            }else
            {
                active = new Stack<GameObject>();
                active.Push(newCell);
                activeCells.Add(prefab, active);
            }
        }
        return newCell;
    }
}
