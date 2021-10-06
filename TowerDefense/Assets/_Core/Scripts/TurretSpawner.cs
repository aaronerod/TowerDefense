using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour, ITurretSpawner
{

    public Turret SpawnTurret(TurretData turretData, Vector2 worldPosition)
    {
        GameObject turretInstance = Instantiate(turretData.TurretPrefab, worldPosition, Quaternion.identity);
        return turretInstance.GetComponent<Turret>();
    }


#if UNITY_EDITOR
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private TurretData turretData;
    [NaughtyAttributes.Button("Test Spawn")]
    public void TestSpawn()
    {
        SpawnTurret(turretData, spawnPosition.position);
    }

#endif
}
