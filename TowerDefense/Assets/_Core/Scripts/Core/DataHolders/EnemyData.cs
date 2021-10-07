using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName ="TowerDefense/Data Holders/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private AttackData attackData;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private int health;
    [SerializeField]
    private int coins;

    /// <summary>
    /// Attack data
    /// </summary>
    public AttackData AttackData { get => attackData; set => attackData = value; }
    /// <summary>
    /// Movement speed
    /// </summary>
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    /// <summary>
    /// Prefab to instantiate when the enemy is spawn
    /// </summary>
    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    /// <summary>
    /// Initial health
    /// </summary>
    public int Health { get => health; set => health = value; }
   /// <summary>
   /// How much coins will give when he dies
   /// </summary>
    public int Coins { get => coins; set => coins = value; }
}
