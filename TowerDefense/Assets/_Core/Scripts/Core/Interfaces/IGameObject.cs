using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Interface for objects that are game objects in the world
/// </summary>
public interface IGameObject
{
    /// <summary>
    /// The gameobject owner
    /// </summary>
    public GameObject GameObject { get; }
}
