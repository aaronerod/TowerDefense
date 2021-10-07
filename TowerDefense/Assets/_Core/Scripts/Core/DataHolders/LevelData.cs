using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "TowerDefense/Data Holders/Level data")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private List<TurretData> availableTurrets = new List<TurretData>();
    [SerializeField]
    private HordeData hordeData;
    [SerializeField, NaughtyAttributes.ResizableTextArea]
    private string boardData;    
    [SerializeField]
    private int towerLife=100;
    [System.NonSerialized]
    private TurretData tower;

    [SerializeField]
    private int initialCoins=150;

    [SerializeField]
    private Vector2Int gridSize = new Vector2Int(10,10);
    [SerializeField]
    private float cellSize=1;
    [SerializeField]
    private GameObject walkableCell;
    [SerializeField]
    private GameObject unwalkableCell;

    /// <summary>
    /// Turrets available to purchase during this level
    /// </summary>
    public List<TurretData> AvailableTurrets { get => availableTurrets; set => availableTurrets = value; }
    /// <summary>
    /// Size of the grid
    /// </summary>
    public Vector2Int GridSize { get => gridSize; set => gridSize = value; }
    /// <summary>
    /// Prefab for unwalkable cells
    /// </summary>
    public GameObject UnwalkableCell { get => unwalkableCell; set => unwalkableCell = value; }
    /// <summary>
    /// Prefab for walkable cells
    /// </summary>
    public GameObject WalkableCell { get => walkableCell; set => walkableCell = value; }
    /// <summary>
    /// Cell size
    /// </summary>
    public float CellSize { get => cellSize; set => cellSize = value; }
    /// <summary>
    /// Horde sequence
    /// </summary>
    public HordeData HordeData { get => hordeData; set => hordeData = value; }
    /// <summary>
    /// Initial coins for this level
    /// </summary>
    public int InitialCoins { get => initialCoins; set => initialCoins = value; }
    /// <summary>
    /// Life of the tower in this level
    /// </summary>
    public int TowerLife { get => towerLife; set => towerLife = value; }
    public string BoardData { get => boardData; set => boardData = value; }
}
