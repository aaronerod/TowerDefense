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

    public AttackData AttackData { get => attackData; set => attackData = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public int Health { get => health; set => health = value; }
}
