using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private event System.Action mapLoaded;
    public event System.Action MapLoaded
    {
        add
        {
            mapLoaded += value;
            if (gridMap != null)
                value();

        }
        remove => mapLoaded -= value;
    }
    [SerializeField]
    private MapControllerConnector mapControllerConnector;
    [SerializeField]
    private PlayerData playerData;

    private GridMap gridMap;
    [SerializeField]
    private PreviewTurret previewTurret;
    [SerializeField]
    private ViewTurretInfo turretInfo;
    [SerializeField]
    HordeController hordeController;
    [SerializeField]
    private GameObject turretSpawnerReference;
    [SerializeField]
    private InputTap inputTap;
    [SerializeField]
    private bool allowBuildOverWalkable;

    private TurretData currentTurretData;
    private LevelData levelData;
    public GridMap GridMap { get => gridMap; set => gridMap = value; }

    private List<Turret> turrets = new List<Turret>();

    private ITurretSpawner turretSpawner;
    public bool AllowBuildOverWalkable { get => allowBuildOverWalkable; set => allowBuildOverWalkable = value; }
    public LevelData LevelData { get => levelData; set => levelData = value; }

    public void Initialize(LevelData levelData, out Vector3 towerPos, out Vector3 spawnPos)
    {
        previewTurret.gameObject.SetActive(false);
        turretInfo.gameObject.SetActive(false);
        this.LevelData = levelData;
        foreach (var turret in turrets)
            turretSpawner.RecycleTurret(turret);
        turrets.Clear();

        mapControllerConnector.Controller = this;
        inputTap.PointerClick.AddListener(OnClickReceived);
        turretSpawner = turretSpawnerReference.GetComponent<ITurretSpawner>();
        gridMap = new GridMap(levelData.GridSize, levelData.CellSize, Vector3.one);
        GridMap.CreateGrid();
        gridMap.LoadBoardData(levelData.BoardData, out towerPos, out spawnPos);
        mapLoaded?.Invoke();
    }

    private void Update()
    {
        foreach (var turret in turrets)
            turret.UpdateBehaviors(hordeController.activeEnemies);
    }

    public void PreviewTurret(TurretData turretData)
    {
        if (playerData.EconomyData.EnoughCoins(turretData.Cost))
        {
            currentTurretData = turretData;
            previewTurret.Initialize(turretData, gridMap.GetCenterCell().WorldCoordinates);
            turretInfo.gameObject.SetActive(false);
            previewTurret.gameObject.SetActive(true);
        }
    }

    void OnClickReceived(Vector3 position)
    {
        if (previewTurret.isActiveAndEnabled)
        {
            previewTurret.OnPositionUpdated(position);
        }
        else
        {
            GridCell cell = gridMap.GetCell(position);
            if (cell != null && cell.CurrentBuilding != null)
            {
                TurretData turretData = cell.CurrentBuilding.TurretData;
                if (turretData != null)
                {
                    turretInfo.Initialize(turretData, cell.CurrentBuilding);
                    turretInfo.gameObject.SetActive(true);
                    previewTurret.gameObject.SetActive(false);
                }
            }
            else
                turretInfo.gameObject.SetActive(false);
        }

    }

    public void PurchaseSelectedTurret(Vector2 position)
    {
        previewTurret.gameObject.SetActive(false);
        if (playerData.EconomyData.EnoughCoins(currentTurretData.Cost))
        {
            GridCell gridCell = GridMap.GetCell(position);
            if (gridCell != null)
            {
                if (gridCell.IsEmpty && (AllowBuildOverWalkable ||!gridCell.IsWalkable))
                {
                    if (playerData.EconomyData.SpendCoins(currentTurretData.Cost))
                    {
                        Vector2 finalPosition = GridMap.GridToWorld(gridCell.Coordinates.x, gridCell.Coordinates.y);
                        Turret turret = turretSpawner.SpawnTurret(currentTurretData, finalPosition);
                        turrets.Add(turret);
                        gridCell.AssignBuildable(turret);
                        if (playerData.EconomyData.EnoughCoins(currentTurretData.Cost))
                            PreviewTurret(currentTurretData);
                    }
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
    }

    public void SellTurret(Turret turret)
    {
        GridCell cell = gridMap.GetCell(turret.transform.position);
        if (cell != null)
        {
            turretSpawner.RecycleTurret(turret);
            turrets.Remove(turret);
            cell.RemoveBuildable();
            playerData.EconomyData.AddCoins(turret.TurretData.SellingCost);
        }
    }

    

    private void OnDrawGizmos()
    {
        if(GridMap!=null)
        GridMap.OnDrawGizmosSelected();
    }
}
