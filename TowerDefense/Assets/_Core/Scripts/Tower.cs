using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base tower, the target of all enemies. 
/// </summary>
public class Tower : DamageReceiver, IBuildable
{
    public TurretData TurretData => null;

    private void Start()
    {
        Destroyed += OnTowerDestroyed;
    }
    void OnTowerDestroyed(IDamageReceiver damageReceiver)
    {
    }

    public void Build(GridCell gridCell)
    {
        throw new System.NotImplementedException();
    }
}
