using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurretSpawner
{
    public Turret SpawnTurret(TurretData turretData, Vector2 worldPosition);
}
