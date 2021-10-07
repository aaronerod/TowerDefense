using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Defines the basic data of an attacker
/// </summary>

[System.Serializable]
public class AttackData
{
    [SerializeField]
    private int damageAmount;
    [SerializeField]
    private float attackRate;
    [SerializeField]
    private float range;
    [SerializeField]
    private ProjectileType projectileType;
    [SerializeField]
    private UnitType targetType;
    [SerializeField]
    private UnitType unitType;

    /// <summary>
    /// The amount of damage that the attacker can do
    /// </summary>
    public int DamageAmount { get => damageAmount; set => damageAmount = value; }
    /// <summary>
    /// How fast the attacker can perform an attack
    /// </summary>
    public float AttackRate { get => attackRate; set => attackRate = value; }
    /// <summary>
    /// How far con attack
    /// </summary>
    public float Range { get => range; set => range = value; }
    /// <summary>
    /// The type of projectile the attacker is going to use
    /// </summary>
    public ProjectileType ProjectileType { get => projectileType; set => projectileType = value; }
    /// <summary>
    /// Type of target to attack
    /// </summary>
    public UnitType TargetType { get => targetType; set => targetType = value; }
    /// <summary>
    /// Type of unit
    /// </summary>
    public UnitType UnitType { get => unitType; set => unitType = value; }
}
