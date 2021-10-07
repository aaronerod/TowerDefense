using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    single, laser, splash
}
[System.Flags] public enum UnitType
{
    ground = 1,
    air =2
}