using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Interface for objects that can attack
/// </summary>
public interface IAttacker:IGameObject
{
    public void Attack();
}
