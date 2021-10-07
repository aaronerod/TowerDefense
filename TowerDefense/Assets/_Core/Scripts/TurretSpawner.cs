using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour, ITurretSpawner
{
    [SerializeField]
    private Transform spawnParent;
    private Dictionary<TurretData, Stack<Turret>> recycledTurrets = new Dictionary<TurretData, Stack<Turret>>();
    
    public Turret SpawnTurret(TurretData turretData, Vector2 worldPosition)
    {
        Turret turret = GetTurretInstance(turretData, worldPosition);
        turret.Initialize(turretData);
        return turret;
    }

    public void RecycleTurret(Turret turret)
    {
        Stack<Turret> turrets;
        if (recycledTurrets.TryGetValue(turret.TurretData, out turrets))
        {
            turrets.Push(turret);
        }
        else
        {
            turrets = new Stack<Turret>();
            turrets.Push(turret);
            recycledTurrets.Add(turret.TurretData, turrets);
        }
        turret.gameObject.SetActive(false);
    }

    public Turret GetTurretInstance(TurretData turretData, Vector2 worldPosition)
    {
        Turret newTurret = null;
        Stack<Turret> turrets;
        if (recycledTurrets.TryGetValue(turretData, out turrets))
        {
            if (turrets.Count > 0)
            {
                newTurret = turrets.Pop();
                newTurret.gameObject.SetActive(true);
                newTurret.transform.position = worldPosition;
                newTurret.Initialize(turretData);
            }
            else
                newTurret = CreateNewTurretInstance(turretData, worldPosition);
        }
        else
        {
            newTurret = CreateNewTurretInstance(turretData, worldPosition);
        }

        return newTurret;
    }

    private Turret CreateNewTurretInstance(TurretData turretData,Vector2 worldPosition)
    {
        GameObject turretInstance = Instantiate(turretData.TurretPrefab, worldPosition, Quaternion.identity, spawnParent);
        Turret newTurret = turretInstance.GetComponent<Turret>();
        newTurret.Initialize(turretData);
        return newTurret;
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
