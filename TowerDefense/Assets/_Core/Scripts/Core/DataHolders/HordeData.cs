using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Horde Data", menuName = "TowerDefense/Data Holders/Horde Data")]
public class HordeData : ScriptableObject
{
    [SerializeField]
    float delayBetweenHordes;
    [SerializeField]
    private List<Horde> hordes = new List<Horde>();

    /// <summary>
    /// Hordes definition
    /// </summary>
    public List<Horde> Hordes { get => hordes; set => hordes = value; }
    /// <summary>
    /// How long will take  for the next horde to begin
    /// </summary>
    public float DelayBetweenHordes { get => delayBetweenHordes; set => delayBetweenHordes = value; }
}

/// <summary>
/// Horde definition
/// </summary>
[System.Serializable]
public class Horde
{
    [SerializeField]
    List<HordeGroup> groups = new List<HordeGroup>();

    /// <summary>
    /// Group of enemies
    /// </summary>
    public List<HordeGroup> Groups { get => groups; set => groups = value; }
}

/// <summary>
/// Group of enemies
/// </summary>
[System.Serializable]
public class HordeGroup
{
    [SerializeField]
    private List<EnemyData> enemies = new List<EnemyData>();
    [SerializeField]
    private int spawnAmount;
    [SerializeField]
    private float spawnRate;

    /// <summary>
    /// List of enemies in the group
    /// </summary>
    public List<EnemyData> Enemies { get => enemies; set => enemies = value; }
    /// <summary>
    /// How fast spawn new enemies
    /// </summary>
    public float SpawnRate { get => spawnRate; set => spawnRate = value; }
    /// <summary>
    /// How much enemies should be spawned for this group
    /// </summary>
    public int SpawnAmount { get => spawnAmount; set => spawnAmount = value; }
}