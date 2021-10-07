using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField]
    private MapControllerConnector mapControllerConnector;
    private MapController mapController;
    [SerializeField]
    private Camera camera;

    private void Start()
    {
        mapControllerConnector.ControllerAssigned += OnMapControllerAssigned;
    }
    void OnMapControllerAssigned(MapController mapController)
    {
        this.mapController = mapController;
        mapController.MapLoaded += OnMapLoaded;
        
    }
    void OnMapLoaded()
    {
        Vector3 gridSize = new Vector3(mapController.GridMap.Cols,
           mapController.GridMap.Rows) * mapController.GridMap.CellSize;
        Vector3 gridCenter = gridSize*.5f + mapController.GridMap.Origin;
        gridCenter.z = -10;

        camera.orthographicSize = mapController.GridMap.Rows * mapController.GridMap.CellSize * .5f + 1;
        camera.transform.position = gridCenter;
    }
}
