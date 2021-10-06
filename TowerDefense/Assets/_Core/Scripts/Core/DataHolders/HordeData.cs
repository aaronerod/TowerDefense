using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Horde Data", menuName = "TowerDefense/Data Holders/Horde Data")]
public class HordeData : ScriptableObject
{
    [SerializeField]
    private List<Horde> hordes = new List<Horde>();

    public List<Horde> Hordes { get => hordes; set => hordes = value; }
}

[System.Serializable]
public class Horde
{
    [SerializeField]
    List<HordeGroup> groups = new List<HordeGroup>();

    public List<HordeGroup> Groups { get => groups; set => groups = value; }
}

[System.Serializable]
public class HordeGroup
{
    [SerializeField]
    private List<EnemyData> enemies = new List<EnemyData>();
    [SerializeField]
    private int spawnAmount;
    [SerializeField]
    private float spawnRate;

    public List<EnemyData> Enemies { get => enemies; set => enemies = value; }
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
    public int SpawnAmount { get => spawnAmount; set => spawnAmount = value; }
}