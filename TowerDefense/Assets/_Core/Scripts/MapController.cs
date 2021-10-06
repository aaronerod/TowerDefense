using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private GridMap gridMap;
    [SerializeField]
    HordeController hordeController;
    private List<Turret> turrets = new List<Turret>();
    [SerializeField]
    private GameObject turretSpawnerReference;
    private ITurretSpawner turretSpawner;
    [SerializeField]
    private TurretData turretData;
    [SerializeField]
    private InputTap inputTap;

    public GridMap GridMap { get => gridMap; set => gridMap = value; }

    public void Start()
    {
        inputTap.PointerClick.AddListener(OnClickReceived);
        turretSpawner = turretSpawnerReference.GetComponent<ITurretSpawner>();
        GridMap.CreateGrid();
        
    }

    private void Update()
    {
        foreach (var turret in turrets)
            turret.UpdateBehaviors(hordeController.activeEnemies);
    }

    void OnClickReceived(Vector3 position)
    {
        int x;
        int y;
        GridCell gridCell = GridMap.GetCell(position);
        if (gridCell != null) {
            if (gridCell.IsEmpty) { 
                Vector2 finalPosition = GridMap.GridToWorld(gridCell.Coordinates.x, gridCell.Coordinates.y);
                Turret turret = turretSpawner.SpawnTurret(turretData, finalPosition);
                turrets.Add(turret);
                gridCell.AssignBuildable(turret);
            }
            else
            {
                Debug.LogError("Position in use");
            }
        }
        else
        {
            Debug.LogError("Wrong position");
        }
        
    }

    private string GetCoordinateKey(int x, int y)
    {
        string key = x + "," + y;
        return key;
    }

    private void OnDrawGizmos()
    {
        GridMap.OnDrawGizmosSelected();
    }
}
