using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Interface for objects that can attack
/// </summary>
public interface IAttacker:IGameObject
{
    /// <summary>
    /// Updates tbe status of the attack
    /// </summary>
    /// <param name="damageReceiver">Target to attack</param>
    /// <param name="attackData">Attack data of the owner</param>
    public void UpdateAttack(IDamageReceiver damageReceiver, AttackData attackData);
    /// <summary>
    /// Execute the shoot action
    /// </summary>
    /// <param name="target">Target to shoot</param>
    public void Shoot(IDamageReceiver target);
}
