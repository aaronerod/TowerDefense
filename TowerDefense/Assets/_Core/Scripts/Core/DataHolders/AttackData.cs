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
    private int attackRate;
    [SerializeField]
    private float range;
    [SerializeField]
    private ProjectileType projectileType;

    /// <summary>
    /// The amount of damage that the attacker can do
    /// </summary>
    public int DamageAmount { get => damageAmount; set => damageAmount = value; }
    /// <summary>
    /// How fast the attacker can perform an attack
    /// </summary>
    public int AttackRate { get => attackRate; set => attackRate = value; }
    /// <summary>
    /// How far con attack
    /// </summary>
    public float Range { get => range; set => range = value; }
    /// <summary>
    /// The type of projectile the attacker is going to use
    /// </summary>
    public ProjectileType ProjectileType { get => projectileType; set => projectileType = value; }
}
