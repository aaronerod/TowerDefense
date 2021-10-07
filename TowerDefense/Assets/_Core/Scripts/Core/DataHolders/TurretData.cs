using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Data", menuName = "TowerDefense/Data Holders/Turret Data")]
public class TurretData : ScriptableObject
{
    [SerializeField]
    private AttackData attackData;
    [SerializeField]
    private int cost;
    [SerializeField]
    private GameObject turretPrefab;
    [SerializeField]
    private GameObject uiTurretPrefab;

    public AttackData AttackData { get => attackData; set => attackData = value; }
    public int Cost { get => cost; set => cost = value; }
    public GameObject TurretPrefab { get => turretPrefab; set => turretPrefab = value; }
    public GameObject UiTurretPrefab { get => uiTurretPrefab; set => uiTurretPrefab = value; }
}
