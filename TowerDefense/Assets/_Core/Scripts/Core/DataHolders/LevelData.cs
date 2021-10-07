using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "TowerDefense/Data Holders/Level data")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private List<TurretData> availableTurrets = new List<TurretData>();


    public List<TurretData> AvailableTurrets { get => availableTurrets; set => availableTurrets = value; }
}
