using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable:IGameObject
{
    public TurretData TurretData { get; }
    public void Build(GridCell gridCell);
    
}
